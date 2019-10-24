using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSmartQB.CORE.DTOs
{
    public class GroupUser
    {
        public string GroupId { get; set; }
        public string[] UserId { get; set; }
    }

    public class GroupListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public string CreatedBy { get; set; }
    }

    public class GroupAddDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }

    }

    public class GroupPagination
    {
        public List<GroupListDto> Groups { get; set; }
        public int TotalRows { get; set; }
    }

}
