using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLaunderingApp
{
    public class App
    {
        private readonly ITransactionService _transactionService;

        public App(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task Run()
        {
            var sketchyTransactions = await _transactionService.GetTransactionsFromDate(DateOnly.MinValue);
            Console.WriteLine("Success");
            Console.ReadLine();
        }
    }
}
