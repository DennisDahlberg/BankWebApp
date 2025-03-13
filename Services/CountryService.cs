using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountryService
    {
        private readonly BankAppDataContext _dbContext;

        public CountryService(BankAppDataContext bankAppDataContext)
        {
            _dbContext = bankAppDataContext;
        }

        public CountryStatsViewModel GetSwedenStats()
        {
            var customers = _dbContext.Customers
                .Where(x => x.Country == "Sweden")
                .Count();

            var accounts = _dbContext.Accounts
                .Where(x => x.Dispositions
                .Any(x => x.Customer.Country == "Sweden"))
                .Count();

            var money = _dbContext.Accounts
                .Where(x => x.Dispositions
                .Any(x => x.Customer.Country == "Sweden"))
                .Sum(x => x.Balance);

            return new CountryStatsViewModel
            {
                AmountOfCustomers = customers,
                AmountOfAccounts = accounts,
                AmountOfMoney = money
            };
        }
        public void GetFinlandStats()
        {

        }
        public void GetDenmarkStats()
        {

        }
        public void GetNorwayStats()
        {

        }
    }
}
