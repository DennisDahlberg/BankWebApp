using BankWebApp.Infrastructure.Paging;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<PagedResult<UserDTO>> GetAllUsers(string sortBy, string sortOrder, int pageNo, string q);
    }
}
