using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankAppDataContext _dbContext;

        public TransactionService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void GetSuspectTransactionsFromCountry(List<MoneyLaunderingCustomerDTO> customers, DateOnly date)
        {
            List<SuspectTransactionDTO> suspectTransactionDTOs = new List<SuspectTransactionDTO>();

            foreach (var customer in customers)
            {

            }
            //TODO
        }


        
    }
}
