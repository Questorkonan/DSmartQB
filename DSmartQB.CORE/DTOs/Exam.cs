using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSmartQB.CORE.DTOs
{
    public class ExamBinder
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Mark { get; set; }
        public string Type { get; set; }
        public int NoOfQuestions { get; set; }
        public string Group { get; set; }
    }
    public class ExamPost
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Mark { get; set; }
        public string Type { get; set; }
        public string CourseId { get; set; }
        public string GroupId { get; set; }
        public int Duration { get; set; }
        public string  Supervisor { get; set; }
        public DateTime StartDate { get; set; }
    }

    public class ExamPagination
    {
        public List<ExamBinder> Exams { get; set; }
        public int TotalRows { get; set; }
    }
}
