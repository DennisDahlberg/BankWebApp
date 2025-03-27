using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Intefaces;

namespace BankWebApp.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public List<UserViewModel> Users { get; set; }
        public string Q { get; set; }

        public async Task OnGet()
        {
            var userDTOs = await _userService.GetAllUsersAsync();

            Users = userDTOs.Select(u => new UserViewModel()
            {
                UserId = u.UserId,
                Email = u.Email,
                Role = u.Role,
            }).ToList();
        }
    }
}
