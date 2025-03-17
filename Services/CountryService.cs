using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.Intefaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountryService : ICountryService
    {
        private readonly BankAppDataContext _dbContext;

        public CountryService(BankAppDataContext bankAppDataContext)
        {
            _dbContext = bankAppDataContext;
        }        

        public CountryStatsViewModel GetACountriesStats(string countryName)
        {
            var customers = _dbContext.Customers
                .Where(x => x.Country == countryName)
                .Count();

            var accounts = _dbContext.Accounts
                .Where(x => x.Dispositions
                .Any(x => x.Customer.Country == countryName))
                .Count();

            var money = _dbContext.Accounts
                .Where(x => x.Dispositions
                .Any(x => x.Customer.Country == countryName))
                .Sum(x => x.Balance);

            return new CountryStatsViewModel
            {
                AmountOfCustomers = customers,
                AmountOfAccounts = accounts,
                AmountOfMoney = money
            };
        }

        public List<CountryStatsViewModel> GetCountriesStats()
        {
            var sweden = GetACountriesStats("Sweden");
            sweden.ImageUrl = "assets/img/Flags/se.png";

            var denmark = GetACountriesStats("Denmark");
            denmark.ImageUrl = "assets/img/Flags/dk.png";

            var finland = GetACountriesStats("Finland");
            finland.ImageUrl = "assets/img/Flags/fi.png";

            var norway = GetACountriesStats("Norway");
            norway.ImageUrl = "assets/img/Flags/no.png";

            var stats = new List<CountryStatsViewModel>();
            stats.Add(sweden);
            stats.Add(denmark);
            stats.Add(finland);
            stats.Add(norway);

            return stats;
        }
    }
}
