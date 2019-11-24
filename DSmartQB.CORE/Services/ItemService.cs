using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Models;
using System.Collections.Generic;
using System.Linq;

namespace DSmartQB.CORE.Services
{
    public class ItemService
    {
        DSmartQBContext _db = new DSmartQBContext();

        public List<ItemTypeDto> ItemTypes()
        {
            string query1 = $"EXECUTE SP_ListItemTypes";
            var types = _db.Database.SqlQuery<ItemTypeDto>(query1).ToList();
            return types;
        }

        public string DeleteItem(string Id)
        {
            string iquery = $"EXECUTE SP_DeleteItem '{Id}'";

            var item = _db.Database.SqlQuery<string>(iquery).FirstOrDefault();
            return item;
        }

        public ReturnMessage CheckILO(string ILO)
        {
            string itemQuery = $"EXECUTE SP_CheckILOExist '{ILO}'";
            var Item = _db.Database.SqlQuery<ReturnMessage>(itemQuery).FirstOrDefault();

            return Item;
        }

        public bool IsPublishedExam(string QuestionId)
        {
            string query = $"EXECUTE SP_GetExamPublishedStatus '{QuestionId}'";
            return _db.Database.SqlQuery<bool>(query).FirstOrDefault();
        }

        public ReturnMessage EditMWQ(MCQ model)
        {

            bool flag = IsPublishedExam(model.Item.Id);
            List<Ids> Ids = new List<Ids>();


            #region Item

            string itemQuery = "";

            if (model.Item.ILoId == null)
            {
                itemQuery = $"EXECUTE SP_EditMWQUnAssociate '{model.Item.Id}', N'{model.Item.Stem}', {model.Item.Duration}, '{model.Item.Level}'";
                _db.Database.SqlQuery<string>(itemQuery).FirstOrDefault();
            }
            else
            {
                itemQuery = $"EXECUTE SP_EditMWQAssociate '{model.Item.Id}', N'{model.Item.Stem}', {model.Item.Duration}, '{model.Item.ILoId}', '{model.Item.Level}'";
                _db.Database.SqlQuery<string>(itemQuery).FirstOrDefault();
            }

            if (!flag)
            {
                // Update ItemArchieve

                string query = $"EXECUTE SP_UndateItemArchieve '{model.Item.Id}', '{model.Item.Stem}', {model.Item.Duration}, '{model.Item.Level}'";
                Ids = _db.Database.SqlQuery<Ids>(query).ToList();
            }

            #endregion

            #region Answers

            string deleteansQuery = $"EXECUTE SP_DeleteAnssersBasedOnQuestion '{model.Item.Id}'";
            _db.Database.SqlQuery<string>(deleteansQuery).FirstOrDefault();


            ReturnMessage ans = new ReturnMessage();

            foreach (var val in model.Answers)
            {
                string aquery = $"EXECUTE SP_CreateAnswerAlternative '{model.Item.Id}','{val.Text}','{val.Status}'";
                ans = _db.Database.SqlQuery<ReturnMessage>(aquery).FirstOrDefault();
            }



            if (!flag)
            {

                foreach (var Id in Ids)
                {
                    string deleteansArchQuery = $"EXECUTE SP_DeleteAnswersArchBasedOnQuestion '{Id.QId}'";
                    _db.Database.SqlQuery<string>(deleteansArchQuery).FirstOrDefault();

                    foreach (var answer in model.Answers)
                    {

                        string archquery = $"EXECUTE SP_CreateArchieveAlternatives '{Id.QId}','{answer.Text}','{answer.Status}','{Id.ExamId}'";
                        _db.Database.SqlQuery<string>(archquery).FirstOrDefault();
                    }

                }

            }

            #endregion

            return ans;

        }


        public ReturnMessage EditTF(TF model)
        {

            bool flag = IsPublishedExam(model.Item.Id);
            List<Ids> Ids = new List<Ids>();

            #region Item

            string itemQuery = "";

            if (model.Item.ILoId == null)
            {
                itemQuery = $"EXECUTE SP_EditMWQUnAssociate '{model.Item.Id}', '{model.Item.Stem}', {model.Item.Duration}, '{model.Item.Level}'";
                _db.Database.SqlQuery<string>(itemQuery).FirstOrDefault();
            }
            else
            {
                itemQuery = $"EXECUTE SP_EditMWQAssociate '{model.Item.Id}', '{model.Item.Stem}', {model.Item.Duration}, '{model.Item.ILoId}', '{model.Item.Level}'";
                _db.Database.SqlQuery<string>(itemQuery).FirstOrDefault();
            }

            if (!flag)
            {
                // Update ItemArchieve

                string query = $"EXECUTE SP_UndateItemArchieve '{model.Item.Id}', '{model.Item.Stem}', {model.Item.Duration}, '{model.Item.Level}'";
                Ids = _db.Database.SqlQuery<Ids>(query).ToList();

            }



            #endregion

            #region Answers

            string deleteansQuery = $"EXECUTE SP_DeleteAnssersBasedOnQuestion '{model.Item.Id}'";
            _db.Database.SqlQuery<string>(deleteansQuery).FirstOrDefault();


            ReturnMessage ans = new ReturnMessage();


            string aquery = $"EXECUTE SP_CreateAnswerAlternative '{model.Item.Id}','{model.Answer.Text}','{model.Answer.Status}'";
            ans = _db.Database.SqlQuery<ReturnMessage>(aquery).FirstOrDefault();



            if (!flag)
            {

                foreach (var Id in Ids)
                {
                    string deleteansArchQuery = $"EXECUTE SP_DeleteAnswersArchBasedOnQuestion '{Id.QId}'";
                    _db.Database.SqlQuery<string>(deleteansArchQuery).FirstOrDefault();


                    string archquery = $"EXECUTE SP_CreateArchieveAlternatives '{Id.QId}','{model.Answer.Text}','{model.Answer.Status}','{Id.ExamId}'";
                    _db.Database.SqlQuery<string>(archquery).FirstOrDefault();
                }

            }

            #endregion

            return ans;

        }

        public ReturnMessage AddItem(ItemAddDto model)
        {
            string iquery = "";


            if (model.Question.Ilo != "")
            {
                iquery = $"EXECUTE SP_CreateCLassifiedItem N'{model.Question.Item}','{model.Question.Type.Id}','{model.Question.Duration}','{model.Question.UserId}','{model.Question.level}','{model.Question.Ilo}'";

            }
            else
            {
                iquery = $"EXECUTE SP_CreateUnCLassifiedItem N'{model.Question.Item}','{model.Question.Type.Id}','{model.Question.Duration}','{model.Question.UserId}','{model.Question.level}'";

            }
            var item = _db.Database.SqlQuery<string>(iquery).FirstOrDefault();


            ReturnMessage ans = new ReturnMessage();

            if (model.Alternatives.Count == 0)
            {
                string aquery = $"EXECUTE SP_CreateTFAlternative N'{item}','{model.Question.TFStatus}'";
                ans = _db.Database.SqlQuery<ReturnMessage>(aquery).FirstOrDefault();
            }
            else
            {
                foreach (var val in model.Alternatives)
                {
                    string aquery = $"EXECUTE SP_CreateAnswerAlternative N'{item}','{val.Text}','{val.Correct}'";
                    ans = _db.Database.SqlQuery<ReturnMessage>(aquery).FirstOrDefault();
                }
            }



            return ans;
        }

        public ItemPagination AssociatedItems(int page)
        {
            var pagination = new ItemPagination();

            #region Groups

            string query1 = $"EXECUTE SP_AssociatedItems {page}";
            pagination.Items = _db.Database.SqlQuery<ItemListDto>(query1).ToList();

            #endregion

            #region TotalRows

            string query2 = $"EXECUTE SP_AssociatedItemsTotalRows";
            pagination.TotalRows = _db.Database.SqlQuery<int>(query2).FirstOrDefault();


            #endregion

            return pagination;
        }


        public ItemPagination UnAssociatedItems(int page)
        {
            var pagination = new ItemPagination();

            #region Groups

            string query1 = $"EXECUTE SP_UnAssociatedItems {page}";
            pagination.Items = _db.Database.SqlQuery<ItemListDto>(query1).ToList();

            #endregion

            #region TotalRows

            string query2 = $"EXECUTE SP_UnAssociatedItemsTotalRows";
            pagination.TotalRows = _db.Database.SqlQuery<int>(query2).FirstOrDefault();


            #endregion

            return pagination;
        }


        public MCQ LoadItem(string Id)
        {
            var obj = new MCQ();

            #region Item

            string itemQuery = $"EXECUTE SP_DisplayQuestionMWQ '{Id}'";
            obj.Item = _db.Database.SqlQuery<Item>(itemQuery).FirstOrDefault();

            #endregion

            #region Answers

            string ansQuery = $"EXECUTE SP_DisplayAnswersMWQ '{Id}'";
            obj.Answers = _db.Database.SqlQuery<Answer>(ansQuery).ToList();


            #endregion

            #region Planner

            string plnQuery = $"EXECUTE SP_DisplayPlannerMWQ '{obj.Item.ILoId}'";
            obj.Planner = _db.Database.SqlQuery<Planner>(plnQuery).FirstOrDefault();


            #endregion



            return obj;
        }



        public List<string> StringToList(string stringToSplit, char splitDelimiter)
        {
            List<string> list = new List<string>();

            if (string.IsNullOrEmpty(stringToSplit))
                return list;

            foreach (var s in stringToSplit.Split(splitDelimiter))
            {
                list.Add(s.Trim());
            }
            return list;
        }

        public TF LoadItemTF(string Id)
        {
            var obj = new TF();

            #region Item

            string itemQuery = $"EXECUTE SP_DisplayQuestionMWQ '{Id}'";
            obj.Item = _db.Database.SqlQuery<Item>(itemQuery).FirstOrDefault();

            #endregion

            #region Answers

            string ansQuery = $"EXECUTE SP_DisplayAnswersMWQ '{Id}'";
            obj.Answer = _db.Database.SqlQuery<Answer>(ansQuery).First();


            #endregion

            #region Planner

            string plnQuery = $"EXECUTE SP_DisplayPlannerMWQ '{obj.Item.ILoId}'";
            obj.Planner = _db.Database.SqlQuery<Planner>(plnQuery).FirstOrDefault();


            #endregion



            return obj;
        }


    }
}
