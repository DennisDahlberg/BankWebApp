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
        public CountryStatsViewModel GetFinlandStats()
        {
            var customers = _dbContext.Customers
                .Where(x => x.Country == "Finland")
                .Count();

            var accounts = _dbContext.Accounts
                .Where(x => x.Dispositions
                .Any(x => x.Customer.Country == "Finland"))
                .Count();

            var money = _dbContext.Accounts
                .Where(x => x.Dispositions
                .Any(x => x.Customer.Country == "Finland"))
                .Sum(x => x.Balance);

            return new CountryStatsViewModel
            {
                AmountOfCustomers = customers,
                AmountOfAccounts = accounts,
                AmountOfMoney = money
            };
        }
        public CountryStatsViewModel GetDenmarkStats()
        {
            var customers = _dbContext.Customers
                .Where(x => x.Country == "Denmark")
                .Count();

            var accounts = _dbContext.Accounts
                .Where(x => x.Dispositions
                .Any(x => x.Customer.Country == "Denmark"))
                .Count();

            var money = _dbContext.Accounts
                .Where(x => x.Dispositions
                .Any(x => x.Customer.Country == "Denmark"))
                .Sum(x => x.Balance);

            return new CountryStatsViewModel
            {
                AmountOfCustomers = customers,
                AmountOfAccounts = accounts,
                AmountOfMoney = money
            };
        }
        public CountryStatsViewModel GetNorwayStats()
        {
            var customers = _dbContext.Customers
                .Where(x => x.Country == "Norway")
                .Count();

            var accounts = _dbContext.Accounts
                .Where(x => x.Dispositions
                .Any(x => x.Customer.Country == "Norway"))
                .Count();

            var money = _dbContext.Accounts
                .Where(x => x.Dispositions
                .Any(x => x.Customer.Country == "Norway"))
                .Sum(x => x.Balance);

            return new CountryStatsViewModel
            {
                AmountOfCustomers = customers,
                AmountOfAccounts = accounts,
                AmountOfMoney = money
            };
        }
    }
}
