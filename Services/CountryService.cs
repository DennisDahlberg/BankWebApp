using DataAccessLayer.DTOs;
using DataAccessLayer.Enums;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
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

        public CountryStatsDTO GetACountriesStats(Country countryName)
        {
            var customers = _dbContext.Customers
                .Where(x => x.Country == countryName.ToString())
                .Count();

            var accounts = _dbContext.Accounts
                .Where(x => x.Dispositions
                .Any(x => x.Customer.Country == countryName.ToString()))
                .Count();

            var money = _dbContext.Accounts
                .Where(x => x.Dispositions
                .Any(x => x.Customer.Country == countryName.ToString()))
                .Sum(x => x.Balance);

            return new CountryStatsDTO
            {
                AmountOfCustomers = customers,
                AmountOfAccounts = accounts,
                AmountOfMoney = money,
                Country = countryName
            };
        }

        public List<CountryStatsDTO> GetCountriesStats()
        {
            var sweden = GetACountriesStats(Country.Sweden);
            sweden.ImageUrl = "assets/img/Flags/se.png";

            var denmark = GetACountriesStats(Country.Denmark);
            denmark.ImageUrl = "assets/img/Flags/dk.png";

            var finland = GetACountriesStats(Country.Finland);
            finland.ImageUrl = "assets/img/Flags/fi.png";

            var norway = GetACountriesStats(Country.Norway);
            norway.ImageUrl = "assets/img/Flags/no.png";

            var stats = new List<CountryStatsDTO>();
            stats.Add(sweden);
            stats.Add(denmark);
            stats.Add(finland);
            stats.Add(norway);

            return stats;
        }

        public Country GetEnumFromString(string country)
        {
            if (country.ToLower() == "sweden")
                return Country.Sweden;

            else if (country.ToLower() == "norway")
                return Country.Norway;

            else if (country.ToLower() == "denmark")
                return Country.Denmark;

            else 
                return Country.Finland;
        }

        public List<SelectListItem> GetCountryEnums()
        {
            var genders = Enum.GetValues<Country>()
                .Select(c => new SelectListItem()
                {
                    Value = c.ToString(),
                    Text = c.ToString()
                }).ToList();

            

            return genders;
        }


    }
}
