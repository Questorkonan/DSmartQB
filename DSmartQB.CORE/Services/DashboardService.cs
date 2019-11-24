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
            string query = $"EXECUTE SP_Cards";
            var cards = _db.Database.SqlQuery<CardBody>(query).FirstOrDefault();
            return cards;
        }

        public List<Admins> Admins()
        {
            string query = $"EXECUTE SP_Admins";
            var admins = _db.Database.SqlQuery<Admins>(query).ToList();
            return admins;
        }
        public ItemsPercentage ItemsPercentage()
        {
            string query = $"EXECUTE SP_ItemsPercentage";
            var itemPercentage = _db.Database.SqlQuery<ItemsPercentage>(query).FirstOrDefault();
            return itemPercentage;
        }

        public ExamsPercentage ExamsPercentage()
        {
            string query = $"EXECUTE SP_ExamsPercentage";
            var examPercentage = _db.Database.SqlQuery<ExamsPercentage>(query).FirstOrDefault();
            return examPercentage;
        }
    }
}
