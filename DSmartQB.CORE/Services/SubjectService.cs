using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Models;
using System.Collections.Generic;
using System.Linq;

namespace DSmartQB.CORE.Services
{
    public class SubjectService
    {
        DSmartQBContext _db = new DSmartQBContext();

        public CoursePagination ListSubjects(int page)
        {
            var pagination = new CoursePagination();

            #region Groups

            string query1 = $"EXECUTE dbo.SP_ListCourses {page}";
            pagination.Courses = _db.Database.SqlQuery<CourseDto>(query1).ToList();

            #endregion

            #region TotalRows

            string query2 = $"EXECUTE dbo.SP_CourseTotalRows";
            pagination.TotalRows = _db.Database.SqlQuery<int>(query2).FirstOrDefault();


            #endregion

            return pagination;
        }

        public List<PlannerBind> LoadCourses()
        {
            string query1 = $"EXECUTE dbo.SP_LoadCourses";
            var Courses = _db.Database.SqlQuery<PlannerBind>(query1).ToList();
            return Courses;
        }

        public CourseDto BasicInformations(string Id)
        {
            string query1 = $"EXECUTE dbo.SP_GetSubjectById '{Id}'";
            var subject = _db.Database.SqlQuery<CourseDto>(query1).FirstOrDefault();

            return subject;
        }
        public ReturnMessage AddCourse(CourseDto model)
        {
            string query = $"EXECUTE dbo.SP_AddCourse '{model.Name}' , '{model.Code}' , {model.Marks} , {model.Hourse}";
            var user = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return user;
        }

        public ReturnMessage EditCourse(CourseDto model)
        {
            string query = $"EXECUTE dbo.SP_UpdateCourse '{model.Id}','{model.Name}' , '{model.Code}' , {model.Marks} , {model.Hourse}";
            var user = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return user;
        }


        public List<CongitiveDto> ListCongitives()
        {
            #region Groups

            string query1 = $"EXECUTE dbo.SP_ListCongitives";
            var Congitives = _db.Database.SqlQuery<CongitiveDto>(query1).ToList();

            #endregion

            return Congitives;
        }

        public List<PlannerBind> ListPlanner(string id)
        {
            #region Groups

            string query1 = $"EXECUTE dbo.SP_ListPlanners '{id}'";
            var pagination = _db.Database.SqlQuery<PlannerBind>(query1).ToList();

            #endregion

            return pagination;
        }

        public List<ILODto> ListIlos(string id)
        {
            #region Groups

            string query1 = $"EXECUTE dbo.SP_GetIlosById '{id}'";
            var pagination = _db.Database.SqlQuery<ILODto>(query1).ToList();

            #endregion

            return pagination;
        }

        public List<ILODto> LoadIlos(string id)
        {
            #region Groups

            string query1 = $"EXECUTE dbo.SP_GetIlosByPlannerId '{id}'";
            var pagination = _db.Database.SqlQuery<ILODto>(query1).ToList();

            #endregion

            return pagination;
        }

        public ReturnMessage AddCongitiveLevel(string name)
        {
            string query = $"EXECUTE dbo.SP_AddCongitiveLevel '{name}'";
            var user = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return user;
        }

        public string Update(CongitiveDto model)
        {
            string query = $"EXECUTE dbo.SP_UpdateCongitive '{model.Id}' , '{model.Name}'";
            var user = _db.Database.SqlQuery<string>(query).FirstOrDefault();
            return user;
        }

        public ReturnMessage AddPlanner(PlannerDto model)
        {
            string query = $"EXECUTE dbo.SP_AddPlanner '{model.Name}','{model.CourseId}'";
            var user = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return user;
        }

        public ReturnMessage UpdatePlanner(PlannerBind model)
        {
            string query = $"EXECUTE dbo.SP_UpdatePlanner '{model.Id}','{model.Name}'";
            var user = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return user;
        }

        public ReturnMessage UpdateIlo(ILODto model)
        {
            string query = $"EXECUTE dbo.SP_UpdateIlo '{model.Id}','{model.Text}','{model.CongitiveId}','{model.PlannerId}'";
            var user = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return user;
        }

        public ReturnMessage DeletePlanner(string Id)
        {
            string query = $"EXECUTE dbo.SP_DeletePlanner '{Id}'";
            var user = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return user;
        }

        public ReturnMessage AddILO(ILODto model)
        {
            string query = $"EXECUTE dbo.SP_AddILO '{model.Text}','{model.CongitiveId}','{model.PlannerId}'";
            var user = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return user;
        }

        public ReturnMessage DeleteCongitive(string id)
        {
            string query = $"EXECUTE dbo.SP_DeleteCongitive '{id}'";
            var user = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return user;
        }

        public ReturnMessage DeleteSubject(string id)
        {
            string query = $"EXECUTE dbo.SP_DeleteSubject '{id}'";
            var user = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return user;
        }

        public ReturnMessage DeleteIlo(string id)
        {
            string query = $"EXECUTE dbo.SP_DeleteIlos '{id}'";
            var user = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return user;
        }

    }
}
