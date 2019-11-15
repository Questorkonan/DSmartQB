using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Models;
using System.Collections.Generic;
using System.Linq;

namespace DSmartQB.CORE.Services
{
    public class AccountService
    {
        DSmartQBContext _db = new DSmartQBContext();

        public UserTokenDTO CheckUser(string username, string password)
        {
            string query = $"EXECUTE dbo.SP_CheckUser '{username}','{password}'";
            var user = _db.Database.SqlQuery<UserTokenDTO>(query).FirstOrDefault();
            return user;
        }

        public string[] GetRolesForUser(string username)
        {
            string query = $"EXECUTE dbo.SP_GetRolesForUser '{username}'";
            var roles = _db.Database.SqlQuery<string>(query).ToArray();
            return roles;
        }


        public UserDto GetById(string Id)
        {
            string query = $"EXECUTE dbo.SP_GetUserById '{Id}'";
            var user = _db.Database.SqlQuery<UserDto>(query).FirstOrDefault();
            return user;
        }

        public string GetRoleId(string name)
        {
            string query = $"Select Id From [Role] Where Name = '{name}'";
            var result = _db.Database.SqlQuery<string>(query).FirstOrDefault();
            return result;
        }

        public ReturnMessage AddUser(UserDto model)
        {
            string query = $"EXECUTE dbo.CB_AddUser N'{model.Firstname}',N'{model.Lastname}','{model.Email}','{model.Password}','{model.Phone}','{model.Username}','{model.RoleId}'";
            var result = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return result;
        }

        public ReturnMessage Update(UserDto model)
        {
            string query = $"EXECUTE dbo.CB_UpdateUser '{model.Id}','{model.Firstname}','{model.Lastname}','{model.Email}','{model.Username}','{model.Phone}'";
            var result = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return result;
        }

        public string UserProfile(UserProfile user)
        {
            string message = "";
            string query = $"EXECUTE dbo.SP_UserProfile '{user.Id}','{user.Username}','{user.Password}'";
            message = _db.Database.SqlQuery<string>(query).FirstOrDefault();
            return message;
        }

        public ReturnMessage ChangePassword(UserDto model)
        {
            string query = $"EXECUTE dbo.SP_ChangePassword '{model.Id}','{model.Password}'";
            var result = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return result;
        }

        public string Delete(string id)
        {
            string query = $"EXECUTE dbo.SP_DeleteUser '{id}'";
            var user = _db.Database.SqlQuery<string>(query).FirstOrDefault();
            return user;
        }

        public string DeleteAll(List<string> remove)
        {
            string user = "";
            foreach (var id in remove)
            {
                string query = $"EXECUTE dbo.SP_DeleteUser '{id}'";
                user = _db.Database.SqlQuery<string>(query).FirstOrDefault();
            }
            return user;
        }

        public List<UserBinder> ListTeachers()
        {
            var pagination = new List<UserBinder>();

            string rowsQuery = $"EXECUTE dbo.SP_TypeHeadTeachers";
            pagination = _db.Database.SqlQuery<UserBinder>(rowsQuery).ToList();

            return pagination;
        }

        public UserPagination ListUsers(int page)
        {
            var pagination = new UserPagination();

            #region Users

            string rowsQuery = $"EXECUTE dbo.SP_ListTeachers {page}";
            pagination.Users = _db.Database.SqlQuery<UserBinder>(rowsQuery).ToList();

            #endregion

            #region TotalRows

            string totalQuery = $"EXECUTE dbo.SP_TeacherTotalRows";
            pagination.TotalRows = _db.Database.SqlQuery<int>(totalQuery).FirstOrDefault();


            #endregion

            return pagination;
        }

        public UserPagination ListStudents(int page)
        {
            var pagination = new UserPagination();

            #region Users

            string rowsQuery = $"EXECUTE dbo.SP_ListStudents {page}";
            pagination.Users = _db.Database.SqlQuery<UserBinder>(rowsQuery).ToList();

            #endregion

            #region TotalRows

            string totalQuery = $"EXECUTE dbo.SP_StudentTotalRows";
            pagination.TotalRows = _db.Database.SqlQuery<int>(totalQuery).FirstOrDefault();


            #endregion

            return pagination;
        }

        public List<string> StudentsForGroup(string id)
        {
            string rowsQuery = $"EXECUTE dbo.SP_ListStudentsForGroup '{id}'";
            var students = _db.Database.SqlQuery<string>(rowsQuery).ToList();

            return students;
        }


    }
}
