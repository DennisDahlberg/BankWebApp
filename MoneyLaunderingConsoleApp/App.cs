using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLaunderingConsoleApp
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
            await _transactionService.GetSuspiciousTransactions();
        }
    }
}
