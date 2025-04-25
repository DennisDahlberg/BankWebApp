using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class SuspiciousTransactionDTO
    {
        public int TransactionId { get; set; }
        public int CustomerId { get; set; }
        public string Givenname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
