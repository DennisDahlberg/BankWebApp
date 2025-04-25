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
        private readonly IReportService _reportService;

        public App(ITransactionService transactionService, IReportService reportService)
        {
            _transactionService = transactionService;
            _reportService = reportService;
        }

        public async Task Run()
        {
            var transactions = await _transactionService.GetSuspiciousTransactions();

            _reportService.Write(transactions);
        }
    }
}
