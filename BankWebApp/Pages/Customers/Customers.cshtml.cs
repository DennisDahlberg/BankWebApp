using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using Services.ViewModels;

namespace BankWebApp.Pages.Customers
{
    [Authorize(Roles="Cashier")]
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public List<CustomerViewModel> Customers { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public string SortOrder { get; set; }
        public string SortBy { get; set; }
        public string Q { get; set; }


        public void OnGet(string sortBy, string sortOrder, int pageNo, string q)
        {           
            if (pageNo == 0)
                pageNo = 1;

            Q = q;
            CurrentPage = pageNo;
            SortBy = sortBy;
            SortOrder = sortOrder;

            var result = _customerService.GetAllCustomers(sortBy, sortOrder, pageNo, q);

            PageCount = result.PageCount;

            Customers = result.Results
                .Select(c => new CustomerViewModel
                {
                    CustomerId = c.CustomerId,
                    Givenname = c.Givenname,
                    Surname = c.Surname,
                    Gender = c.Gender,
                    Streetaddress = c.Streetaddress,
                    City = c.City,
                    Country = c.Country,
                }).ToList();
        }
    }
}
