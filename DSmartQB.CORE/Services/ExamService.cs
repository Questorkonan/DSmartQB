using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Models;
using System.Collections.Generic;
using System.Linq;

namespace DSmartQB.CORE.Services
{
    public class ExamService
    {
        DSmartQBContext _db = new DSmartQBContext();


        public SpecifedExam LoadSpecifiedExam(string Id)
        {
            string query = $"EXECUTE dbo.SP_LoadSpecifiedExam '{Id}'";
            var SpecifiedExam = _db.Database.SqlQuery<SpecifedExam>(query).FirstOrDefault();
            return SpecifiedExam;
        }

        public ExamPagination ListExams(int page)
        {
            var pagination = new ExamPagination();

            #region Groups

            string query1 = $"EXECUTE dbo.SP_ListExams {page}";
            pagination.Exams = _db.Database.SqlQuery<ExamBinder>(query1).ToList();

            #endregion

            #region TotalRows

            string query2 = $"EXECUTE dbo.SP_ExamTotalRows";
            pagination.TotalRows = _db.Database.SqlQuery<int>(query2).FirstOrDefault();


            #endregion

            return pagination;
        }

        public string UpdateSetting(SpecifedExam model)
        {
            string query = $"EXECUTE dbo.SP_UpdateSetting '{model.Id}','{model.Name}','{model.GroupId}','{model.Duration}','{model.Mark}','{model.StartDate}'";
            string message = _db.Database.SqlQuery<string>(query).FirstOrDefault();

            return message;
        }
        public ReturnMessage AddSetting(ExamPost model)
        {
            ReturnMessage message = new ReturnMessage();

            string query = $"EXECUTE dbo.SP_AddExam '{model.Name}',{model.Mark},'{model.Type}','{model.CourseId}','{model.GroupId}',{model.Duration},'{model.Supervisor}','{model.StartDate}'";
            message = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();

            return message;
        }

        //

        public string ArchieveItems(List<ArchieveItems> Items)
        {
            string message = "";
            ReturnMessage QuestionValidate = new ReturnMessage();


            #region Original Items

            foreach (var item in Items)
            {

                string getOriginalItem = $"EXECUTE dbo.SP_OriginalItem '{item.Id}','{item.ExamId}',{item.Degree}";
                QuestionValidate = _db.Database.SqlQuery<ReturnMessage>(getOriginalItem).FirstOrDefault();
                if (QuestionValidate.Key == 1)
                {
                    // Add Answers

                    string applyAnswersArchieve = $"EXECUTE dbo.SP_ApplyAnswers '{item.Id}','{QuestionValidate.ReturnId}','{item.ExamId}'";
                    message = _db.Database.SqlQuery<string>(applyAnswersArchieve).FirstOrDefault();
                }

            }

            #endregion

            return message;
        }

        public string UpdatePreview(PreviewObj model)
        {
            string message = "";

            #region Update Archieve Item

            string question = $"EXECUTE dbo.SP_UpdateArchieveItem '{model.Question.Id}','{model.Question.ExamId}','{model.Question.Stem}'";
            _db.Database.SqlQuery<string>(question).FirstOrDefault();

            #endregion


            #region  Delete Old Archieve Answers


            string remove = $"EXECUTE dbo.SP_RemoveArchieveAnswer '{model.Question.Id}','{model.Question.ExamId}'";
            message = _db.Database.SqlQuery<string>(remove).FirstOrDefault();



            #endregion


            #region Add New Archieve Answers 

            foreach (var answer in model.Answers)
            {
                string add = $"EXECUTE dbo.SP_AddArchieveAnswer '{model.Question.Id}','{model.Question.ExamId}','{answer.Text}','{answer.Status}'";
                _db.Database.SqlQuery<string>(add).FirstOrDefault();
            }

            #endregion

            return message;
        }

        public string Publish(Publish model)
        {
            string Message = "";
            List<string> UserIds = new List<string>();

            foreach (var Question in model.Questions)
            {
                #region Correct Answer
                
                string correctAnswerQuery = $"EXECUTE dbo.SP_GetCorrectAnswer '{Question.Id}'";
                string CorrectAnswer = _db.Database.SqlQuery<string>(correctAnswerQuery).FirstOrDefault();

                #endregion

                string getGroupId = $"EXECUTE dbo.SP_GetSpecifedGroupId '{model.ExamId}'";
                string GroupId = _db.Database.SqlQuery<string>(getGroupId).FirstOrDefault();

                
                string userIdsQuery = $"EXECUTE dbo.SP_GetUsersByGroup '{GroupId}'";
                UserIds = _db.Database.SqlQuery<string>(userIdsQuery).ToList();

                foreach (var UserId in UserIds)
                {
                    #region Add Item To Exam

                    string insertIntoExam = $"EXECUTE dbo.SP_InserItemExam '{model.ExamId}','{UserId}','{Question.Id}','{CorrectAnswer}',{Question.Degree},'{Question.Type}'";
                    Message = _db.Database.SqlQuery<string>(insertIntoExam).FirstOrDefault();

                    #endregion



                }

            }

            foreach (var UserId in UserIds)
            {


                #region Online Students

                string onlineStudents = $"EXECUTE dbo.SP_AddOnlineStudents '{model.ExamId}','{UserId}'";
                Message = _db.Database.SqlQuery<string>(onlineStudents).FirstOrDefault();

                #endregion
            }

            string isPublished = $"EXECUTE dbo.SP_PublishExamStatus '{model.ExamId}'";
            Message = _db.Database.SqlQuery<string>(isPublished).FirstOrDefault();

            return Message;
        }

        


        public ExamItemsPagination PreviewForSelect(string examId)
        {
            ExamItemsPagination pagination = new ExamItemsPagination();

            string query1 = $"EXECUTE dbo.SP_PreviewForSelect '{examId}'";
            pagination.Items = _db.Database.SqlQuery<MannualItems>(query1).ToList();
            
            return pagination;

        }


        public ExamItemsPagination MannualItems(int page)
        {
            ExamItemsPagination pagination = new ExamItemsPagination();

            string query1 = $"EXECUTE dbo.SP_MannualItems {page}";
            pagination.Items = _db.Database.SqlQuery<MannualItems>(query1).ToList();


            string query2 = $"EXECUTE dbo.SP_ExamItemsTotalRows";
            pagination.TotalRows = _db.Database.SqlQuery<int>(query2).FirstOrDefault();


            return pagination;
        }

        public string BluePrint(BluePrintParams model)
        {
            string message = "";

            List<ArchieveItems> archieves = new List<ArchieveItems>();

            string query = $"EXECUTE dbo.SP_PreviewForSelect {model.NoQuestions},{model.Mild},{model.Normal},{model.Hard}";
            archieves = _db.Database.SqlQuery<ArchieveItems>(query).ToList();



            return message;
        }

        public string DeleteExam(string id)
        {
            string query = $"EXECUTE dbo.SP_DeleteExam '{id}'";
            string user = _db.Database.SqlQuery<string>(query).FirstOrDefault();
            return user;
        }

        public string DeleteItemArchieve(string id)
        {
            string query = $"EXECUTE dbo.SP_DeleteItemArchieve '{id}'";
            string user = _db.Database.SqlQuery<string>(query).FirstOrDefault();
            return user;
        }

        public List<OnlineStudentsGrid> OnlineStudentsGrid(string Id)
        {
            List<OnlineStudentsGrid> Students = new List<OnlineStudentsGrid>();
            string query = $"EXECUTE dbo.SP_OnlineStudents '{Id}'";
            Students = _db.Database.SqlQuery<OnlineStudentsGrid>(query).ToList();
            return Students;
        }

        public Preview Preview(string Id)
        {

            List<Question> Questions = new List<Question>();
            ExamHeader Header = new ExamHeader();
            List<ExamBody> ExamBody = new List<ExamBody>();
            List<Answer> Answers = new List<Answer>();


            string HeaderQuery = $"EXECUTE dbo.SP_GetExamHeader '{Id}'";
            Header = _db.Database.SqlQuery<ExamHeader>(HeaderQuery).FirstOrDefault();


            string query = $"EXECUTE dbo.SP_GetQuestionsByExamId '{Id}'";
            Questions = _db.Database.SqlQuery<Question>(query).ToList();

            foreach (var item in Questions)
            {

                string AnswersQuery = $"EXECUTE dbo.SP_GetQuestionsAnswersByQuestionId '{item.Id}', '{Id}'";
                Answers = _db.Database.SqlQuery<Answer>(AnswersQuery).ToList();

                ExamBody.Add(new ExamBody { Question = item, Answers = Answers });

            }


            Preview prev = new Preview()
            {
                Header = Header,
                ExamBody = ExamBody
            };

            return prev;
        }

        public string AddUniversitySettings(string name , string path)
        {
            string query = $"EXECUTE dbo.SP_SheetLogo '{name}','{path}'";
            string user = _db.Database.SqlQuery<string>(query).FirstOrDefault();
            return user;
        }

    }
}
