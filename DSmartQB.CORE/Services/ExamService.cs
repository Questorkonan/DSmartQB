using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Models;
using System.Collections.Generic;
using System.Linq;

namespace DSmartQB.CORE.Services
{
    public class ExamService
    {
        DSmartQBContext _db = new DSmartQBContext();

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

        public ReturnMessage AddSetting(ExamPost model)
        {
            ReturnMessage message = new ReturnMessage();

            string query = $"EXECUTE dbo.SP_AddExam '{model.Name}',{model.Mark},'{model.Type}','{model.CourseId}','{model.GroupId}',{model.Duration},'{model.Supervisor}','{model.StartDate}'";
            message = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();

            return message;
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


    }
}
