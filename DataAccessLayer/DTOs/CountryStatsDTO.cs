using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class CountryStatsDTO
    {
        public int AmountOfCustomers { get; set; }
        public int AmountOfAccounts { get; set; }
        public decimal AmountOfMoney { get; set; }
        public string ImageUrl { get; set; }
    }
}
