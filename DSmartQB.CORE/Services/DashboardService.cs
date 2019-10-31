using DSmartQB.CORE.DTOs;
using DSmartQB.CORE.Models;
using System.Collections.Generic;
using System.Linq;

namespace DSmartQB.CORE.Services
{
    public class DashboardService
    {
        DSmartQBContext _db = new DSmartQBContext();

        public CardBody Cards()
        {
            string query = $"EXECUTE dbo.SP_Cards";
            var cards = _db.Database.SqlQuery<CardBody>(query).FirstOrDefault();
            return cards;
        }

    }
}
