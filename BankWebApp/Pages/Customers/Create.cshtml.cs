using BankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankWebApp.Pages.Customers
{
    public class CreateModel : PageModel
    {

        [BindProperty]
        public CreateCustomerViewModel Customer { get; set; }

        public void OnGet()
        {

        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            return Page();
        }
    }
}
