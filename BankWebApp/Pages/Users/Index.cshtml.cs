using Azure;
using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Services;
using Services.Interfaces;
using System.Globalization;

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
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public string SortOrder { get; set; }
        public string SortBy { get; set; }
        public string Q { get; set; }
        public async Task OnGet(string sortBy, string sortOrder, int pageNo, string q)
        {
            if (pageNo == 0)
                pageNo = 1;

            Q = q;
            CurrentPage = pageNo;
            SortBy = sortBy;
            SortOrder = sortOrder;

            var result = await _userService.GetAllUsers(sortBy, sortOrder, pageNo, q);

            PageCount = result.PageCount;

            Users = result.Results.Select(u => new UserViewModel()
            {
                UserId = u.UserId,
                Email = u.Email,
                Role = u.Role,
            }).ToList();
        }
    }
}
