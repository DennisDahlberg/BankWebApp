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
        List<CustomerDTO> GetAllCustomersAsync(string sortBy, string order);
        CustomerDTO GetCustomerByIdAsync(int id);
    }
}
