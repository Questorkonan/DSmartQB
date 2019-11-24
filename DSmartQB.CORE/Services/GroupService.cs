using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Models;
using System.Collections.Generic;
using System.Linq;

namespace DSmartQB.CORE.Services
{
    public class GroupService
    {
        DSmartQBContext _db = new DSmartQBContext();

        public GroupPagination ListGroups(int page)
        {
            var pagination = new GroupPagination();

            #region Groups

            string query1 = $"EXECUTE SP_ListGroups {page}";
            pagination.Groups = _db.Database.SqlQuery<GroupListDto>(query1).ToList();

            #endregion

            #region TotalRows

            string query2 = $"EXECUTE SP_GroupTotalRows";
            pagination.TotalRows = _db.Database.SqlQuery<int>(query2).FirstOrDefault();


            #endregion

            return pagination;
        }

        public List<GroupListDto> LoadGroups()
        {
            string query1 = $"EXECUTE SP_LoadGroups";
            var Groups = _db.Database.SqlQuery<GroupListDto>(query1).ToList();
            return Groups;
        }

        public ReturnMessage AddGroup(GroupAddDto model)
        {
            string query = $"EXECUTE SP_AddGroup '{model.Name}' , '{model.CreatedBy}'";
            var user = _db.Database.SqlQuery<ReturnMessage>(query).FirstOrDefault();
            return user;
        }

        public string Update(GroupAddDto model)
        {
            string query = $"EXECUTE SP_UpdateGroup '{model.Id}' , '{model.Name}'";
            var user = _db.Database.SqlQuery<string>(query).FirstOrDefault();
            return user;
        }

        public string Delete(string id)
        {
            string query = $"EXECUTE SP_DeleteGroup '{id}'";
            var user = _db.Database.SqlQuery<string>(query).FirstOrDefault();
            return user;
        }

        public string DeleteAll(List<string> remove)
        {
            string user = "";
            foreach (var item in remove)
            {
                string query = $"EXECUTE SP_DeleteGroup '{item}'";
                 user = _db.Database.SqlQuery<string>(query).FirstOrDefault();
            }
            return user;
        }

        public string AssignGroup(GroupUser model)
        {
            string user = "";

            foreach (var Id in model.UserId)
            {
                string query = $"EXECUTE SP_AssignUserToGroup '{model.GroupId}' , '{Id}'";
                user = _db.Database.SqlQuery<string>(query).FirstOrDefault();
            }
            return user;
        }

    }
}
