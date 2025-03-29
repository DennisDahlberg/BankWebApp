using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class AccountWithCustomerNameDTO
    {
        public int AccountId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public decimal Balance { get; set; }
    }
}
