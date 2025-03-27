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
        private readonly ICustomerService _customerService;
        private readonly ICountryService _countryService;

        public App(ITransactionService transactionService, ICustomerService customerService, ICountryService countryService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
            _countryService = countryService;
        }

        public async Task Run()
        {
            var countries = _countryService.GetCountryEnums();

            foreach (var country in countries)
            {
                Console.WriteLine(country.Value);
                var customers = _customerService.GetCustomersByCountry(country.Text);


            }
        }
    }
}
