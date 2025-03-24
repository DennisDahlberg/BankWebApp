using BankWebApp.Infrastructure.Paging;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Intefaces
{
    public interface ICustomerService
    {
        PagedResult<Customer> GetAllCustomers(string sortBy, string order, int page, string q);
        CustomerDTO GetCustomerByIdAsync(int id);
        CreateCustomerDTO GetCreateCustomer(int customerId);
        Task CreateCustomerWithAccount(CreateCustomerDTO customer);
    }
}
