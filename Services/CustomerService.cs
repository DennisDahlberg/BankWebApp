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
            var query = await _dbContext.Customers
                .Take(20)
                .Select(c => new CustomerViewModel
            {
                CustomerId = c.CustomerId,
                GivenName = c.Givenname,
                SurName = c.Surname,
                Address = c.Streetaddress,
                City = c.City,
                Country = c.Country
            }).ToListAsync();

            return query;
        }
    }
}
