using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Intefaces;

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

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);

                userDTOs.Add(new UserDTO()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Role = role.First()
                });            
            }
            return userDTOs;
        }
    }
}
