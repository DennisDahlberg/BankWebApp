using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankWebApp.Infrastructure.Paging;
using DataAccessLayer.DTOs;
using DataAccessLayer.Enums;
using DataAccessLayer.Models;
using FluentResults;
using Mapster;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _dbContext;
        private readonly ICountryService _countryService;
        private readonly IAccountService _accountService;

        public CustomerService(BankAppDataContext dbContext, 
            ICountryService countryService, IAccountService accountService)
        {
            _dbContext = dbContext;
            _countryService = countryService;
            _accountService = accountService;
        }

        

        public PagedResult<Customer> GetAllCustomers(string sortBy, string sortOrder, int page, string q)
        {
            var query = _dbContext.Customers
                .Where(c => c.IsActive == true)
                .AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query
                    .Where(c => c.Givenname.Contains(q) ||
                    c.Surname.Contains(q) ||
                    c.Streetaddress.Contains(q) ||
                    c.City.Contains(q) ||
                    c.CustomerId.ToString().Contains(q) ||
                    c.NationalId.Contains(q));
            }

            if (sortBy == "Name")
                query = sortOrder == "asc" ? query.OrderBy(c => c.Surname) : query.OrderByDescending(c => c.Surname);

            else if (sortBy == "Address")
                query = sortOrder == "asc" ? query.OrderBy(c => c.Streetaddress) : query.OrderByDescending(c => c.Streetaddress);

            else if (sortBy == "City")
                query = sortOrder == "asc" ? query.OrderBy(c => c.City) : query.OrderByDescending(c => c.City);

            else if (sortBy == "NationalId")
                query = sortOrder == "asc" ? query.OrderBy(c => c.NationalId) : query.OrderByDescending(c => c.NationalId);

            return query.GetPaged(page, 50);
            
        }

        

        public async Task<Result<CustomerDTO>> GetCustomerByIdAsync(int id)
        {
           var customer =  await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null || customer.IsActive == false)
                return Result.Fail("Customer doesn't exist");

            return customer.Adapt<CustomerDTO>();            
        }

        public async Task<Result<CreateCustomerDTO>> GetCreateCustomer(int customerId)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null || customer.IsActive == false)
                return Result.Fail("Customer doesn't exist");

            return customer.Adapt<CreateCustomerDTO>();            
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
                IsActive = true,
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

        public async Task<Result> Delete(int id)
        {
            var customer = await _dbContext.Customers
                .Include(c => c.Dispositions)
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
                return Result.Fail("Customer doesn't exist");

            customer.IsActive = false;

            var deletedAccounts = customer.Dispositions
                .Select(d => _accountService.Delete(d.AccountId));
            await Task.WhenAll(deletedAccounts);

            await _dbContext.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result<List<TopEarnerDTO>>> GetTopEarners(string country)
        {
            if (!Enum.IsDefined(typeof(Country), country))
                return Result.Fail("Not a valid country!");

            return await _dbContext.Customers
                .Where(c => c.Country == country.ToString())
                .Select(c => new TopEarnerDTO()
                {
                    CustomerId = c.CustomerId,
                    Streetaddress = c.Streetaddress,
                    Givenname = c.Givenname,
                    Surname = c.Surname,
                    TotalBalance = c.Dispositions
                        .Select(d => d.Account.Balance)
                        .Sum()
                })
                .OrderByDescending(x => x.TotalBalance)
                .Take(10)
                .ToListAsync();
        }
    }
}
