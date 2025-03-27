using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class SuspectTransactionDTO
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        
    }
}
