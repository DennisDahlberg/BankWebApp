using BankWebApp.ViewModels;
using DataAccessLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Intefaces;

namespace BankWebApp.Pages.Customers
{
    [Authorize(Roles = "Cashier")]
    public class CreateModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CreateModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

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

            var customerDTO = new CreateCustomerDTO()
            {
                Givenname = Customer.Givenname,
                Surname = Customer.Surname,
                Gender = Customer.Gender,
                Streetaddress = Customer.Streetaddress,
                City = Customer.City,
                Country = Customer.Country,
                Emailaddress = Customer.Emailaddress,
                Phonenumber = Customer.Phonenumber,
                Zipcode = Customer.Zipcode,
            };

            _customerService.CreateCustomerWithAccount(customerDTO);
            return RedirectToPage("/Customers/Customers");
        }
    }
}
