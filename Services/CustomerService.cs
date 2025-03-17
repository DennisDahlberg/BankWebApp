using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankWebApp.Infrastructure.Paging;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Intefaces;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _dbContext;

        public CustomerService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        public PagedResult<Customer> GetAllCustomers(string sortBy, string sortOrder, int page)
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

            return query.GetPaged(page, 5);
            
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
    }
}
