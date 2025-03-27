using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankWebApp.Infrastructure.Paging;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly BankAppDataContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(BankAppDataContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<PagedResult<UserDTO>> GetAllUsers(string sortBy, string sortOrder, int pageNo, string q)
        {
            var query = _userManager.Users.AsQueryable();
            var userDTOs = new List<UserDTO>();

            if (!string.IsNullOrEmpty(q))
            {
                query = query
                    .Where(c => c.Id.Contains(q) ||
                    c.Email.Contains(q));
            }

            if (sortBy == "Id")
                query = sortOrder == "asc" ? query.OrderBy(c => c.Id) : query.OrderByDescending(c => c.Id);
            else if (sortBy == "Email")
                query = sortOrder == "asc" ? query.OrderBy(c => c.Email) : query.OrderByDescending(c => c.Email);

            var users = await query.ToListAsync();

            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                userDTOs.Add(new UserDTO()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Role = role.FirstOrDefault()
                });
            }

            var queryUsers = userDTOs.AsQueryable();
            return queryUsers.GetPaged(pageNo, 50);
        }
    }
}
