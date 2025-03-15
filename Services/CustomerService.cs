using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Intefaces;
using Services.ViewModels;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _dbContext;

        public CustomerService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        public async Task<List<CustomerViewModel>> GetAllCustomersAsync(string sortBy, string sortOrder)
        {
            var query = _dbContext.Customers.AsQueryable();

            if (sortBy == "Name")
                query = sortOrder == "asc" ? query.OrderBy(c => c.Surname) : query.OrderByDescending(c => c.Surname);

            else if (sortBy == "Address")
                query = sortOrder == "asc" ? query.OrderBy(c => c.Streetaddress) : query.OrderByDescending(c => c.Streetaddress);

            else if (sortBy == "City")
                query = sortOrder == "asc" ? query.OrderBy(c => c.City) : query.OrderByDescending(c => c.City);

            else if (sortBy == "Country")
                query = sortOrder == "asc" ? query.OrderBy(c => c.Country) : query.OrderByDescending(c => c.Country);

            return await query.Take(25)
                .Select(c => new CustomerViewModel
                {
                    CustomerId = c.CustomerId,
                    GivenName = c.Givenname,
                    SurName = c.Surname,
                    Address = c.Streetaddress,
                    City = c.City,
                    Country = c.Country
                })
                .ToListAsync();
        }

        public async Task<CustomerViewModel> GetCustomerByIdAsync(int id)
        {
           var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            return new CustomerViewModel
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
    }
}
