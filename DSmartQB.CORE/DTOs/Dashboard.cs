using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSmartQB.CORE.DTOs
{
    public class CardBody
    {
        public int Groups { get; set; }
        public int Teachers { get; set; }
        public int Students { get; set; }
        public int Subjects { get; set; }
        public int Items { get; set; }
        public int Exams { get; set; }
    }

    public class Admins
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
    }

    public class ItemsPercentage
    {
        public int Classifed { get; set; }
        public int UnClassifed { get; set; }
    }

    public class ExamsPercentage
    {
        public int Mannual { get; set; }
        public int Automatic { get; set; }
    }
}
