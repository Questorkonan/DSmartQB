using System.Collections.Generic;

namespace DSmartQB.CORE.DTOs
{
    public class CourseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Hourse { get; set; }
        public int Marks { get; set; }
    }

    public class CoursePagination
    {
        public List<CourseDto> Courses { get; set; }
        public int TotalRows { get; set; }
    }
    public class CongitiveDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }

    public class CongitivePagination
    {
        public List<CongitiveDto> Congitives { get; set; }
        public int TotalRows { get; set; }
    }

    public class PlannerBind
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }


    public class PlannerDto
    {
        public string CourseId { get; set; }
        public string Name { get; set; }
    }

    public class ILODto
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string PlannerId { get; set; }
        public string CongitiveId { get; set; }
    }

    public class ILOBinder
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public string Congitive { get; set; }
    }

}
