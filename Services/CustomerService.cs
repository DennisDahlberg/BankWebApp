using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankWebApp.Infrastructure.Paging;
using DataAccessLayer.DTOs;
using DataAccessLayer.Enums;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _dbContext;
        private readonly ICountryService _countryService;

        public CustomerService(BankAppDataContext dbContext, ICountryService countryService)
        {
            _dbContext = dbContext;
            _countryService = countryService;
        }

        

        public PagedResult<Customer> GetAllCustomers(string sortBy, string sortOrder, int page, string q)
        {
            var query = _dbContext.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query
                    .Where(c => c.Givenname.Contains(q) ||
                    c.Surname.Contains(q) ||
                    c.Streetaddress.Contains(q) ||
                    c.City.Contains(q) ||
                    c.Country.Contains(q));
            }

            if (sortBy == "Name")
                query = sortOrder == "asc" ? query.OrderBy(c => c.Surname) : query.OrderByDescending(c => c.Surname);

            else if (sortBy == "Address")
                query = sortOrder == "asc" ? query.OrderBy(c => c.Streetaddress) : query.OrderByDescending(c => c.Streetaddress);

            else if (sortBy == "City")
                query = sortOrder == "asc" ? query.OrderBy(c => c.City) : query.OrderByDescending(c => c.City);

            else if (sortBy == "Country")
                query = sortOrder == "asc" ? query.OrderBy(c => c.Country) : query.OrderByDescending(c => c.Country);

            return query.GetPaged(page, 50);
            
        }

        public List<MoneyLaunderingCustomerDTO> GetCustomersByCountry(string country)
        {
            var customers = _dbContext.Customers.Where(c => c.Country == country);

            return customers.Select(c => new MoneyLaunderingCustomerDTO()
            {
                CustomerId = c.CustomerId,
                Firstname = c.Givenname,
                Lastname = c.Surname,
            }).ToList();
        }

        public CustomerDTO GetCustomerByIdAsync(int id)
        {
           var customer =  _dbContext.Customers.FirstOrDefault(c => c.CustomerId == id);

            return new CustomerDTO
            {
                GivenName = customer.Givenname,
                SurName= customer.Surname,
                Address = customer.Streetaddress,
                Country = customer.Country,
                City = customer.City,
                CustomerId = customer.CustomerId,
                Gender = customer.Gender,
            };
        }

        public CreateCustomerDTO GetCreateCustomer(int customerId)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.CustomerId == customerId);

            return new CreateCustomerDTO()
            {
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                Gender = customer.Gender,
                City = customer.City,
                Country = _countryService.GetEnumFromString(customer.Country),
                Streetaddress = customer.Streetaddress,
                Emailaddress = customer.Emailaddress,
                Phonenumber = customer.Telephonenumber,
                Zipcode = customer.Zipcode,
            };
        }


        public async Task CreateCustomerWithAccount(CreateCustomerDTO customer)
        {
            var newCustomer = new Customer()
            {
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                Gender = customer.Gender,
                Streetaddress = customer.Streetaddress,
                Country = customer.Country.ToString(),
                City = customer.City,
                Telephonenumber = customer.Phonenumber,
                Emailaddress = customer.Emailaddress,
                Zipcode = customer.Zipcode,
            };
            if (customer.Country == Country.Sweden)
                newCustomer.CountryCode = "SE";

            else if (customer.Country == Country.Norway)
                newCustomer.CountryCode = "NO";

            else if (customer.Country == Country.Denmark)
                newCustomer.CountryCode = "DK";

            else if (customer.Country == Country.Finland)
                newCustomer.CountryCode = "FI";

            var account = new Account()
            {
                Frequency = "Monthly",
                Created = DateOnly.FromDateTime(DateTime.Now),
                Balance = 0,
            };

            var disposition = new Disposition()
            {
                Account = account,
                Customer = newCustomer,
                Type = "Owner"
            };

            _dbContext.Customers.Add(newCustomer);
            _dbContext.Accounts.Add(account);
            _dbContext.Dispositions.Add(disposition);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCustomer(int customerId, CreateCustomerDTO customer)
        {
            var customerToUpdate = _dbContext.Customers.First(c => c.CustomerId == customerId);

            customerToUpdate.Givenname = customer.Givenname;
            customerToUpdate.Surname = customer.Surname;
            customerToUpdate.Gender = customer.Gender;
            customerToUpdate.Streetaddress = customer.Streetaddress;
            customerToUpdate.City = customer.City;
            customerToUpdate.Country = customer.Country.ToString();
            customerToUpdate.Zipcode = customer.Zipcode;
            customerToUpdate.Emailaddress = customer.Emailaddress;
            customerToUpdate.Telephonenumber = customer.Phonenumber;
            
            _dbContext.Customers.Update(customerToUpdate);
            await _dbContext.SaveChangesAsync();
        }
    }
}
