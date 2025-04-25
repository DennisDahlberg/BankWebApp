using DataAccessLayer.DTOs;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ReportService :IReportService
    {
        public void Write(List<SuspiciousTransactionDTO> transactions)
        {
            var currentDate = DateTime.Now.ToShortDateString();
            if (transactions.Count == 0)
            {
                Console.WriteLine("Now transactions found!");
                return;
            }

            Console.WriteLine("Writing transactions to file...");

            using (var writer = new StreamWriter($"../../../Files/Report{currentDate}.txt", false))
            {
                writer.WriteLine("TransactionId - AccountId - Givenname - Surname - Amount");
                writer.WriteLine();

                foreach (var transaction in transactions)
                {
                    writer.WriteLine($"{transaction.TransactionId} - {transaction.AccountId} - {transaction.Givenname} - {transaction.Surname} - {transaction.Amount}");
                }
            }
            Console.WriteLine("Done!");
        }
    }
}
