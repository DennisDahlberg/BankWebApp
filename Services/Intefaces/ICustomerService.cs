using DataAccessLayer.Models;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Intefaces
{
    public interface ICustomerService
    {
        Task<List<CustomerViewModel>> GetAllCustomersAsync(string sortBy, string order);
        Task<CustomerViewModel> GetCustomerByIdAsync(int id);
    }
}
