using System.Data.Entity;

namespace DSmartQB.CORE.Models
{

    public class DSmartQBContext : DbContext
    {
        public DSmartQBContext() : base("name=DSmartQBContext")
        {

        }

    }
}
