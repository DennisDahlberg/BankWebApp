using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public string Operation { get; set; } = null!;
        public decimal Balance { get; set; }
    }
}
