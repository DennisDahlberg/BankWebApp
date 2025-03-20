using Services.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankWebApp.ViewModels
{
    public class CreateCustomerViewModel
    {
        [Required]
        public string Gender { get; set; }

        [Required]
        public string Givenname { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Streetaddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public Country Country {  get; set; }

        [Required]
        [StringLength(15)]
        public string Zipcode { get; set; }

        [Required]
        [EmailAddress]
        public string Emailaddress { get; set; }

        [Required]
        public string Phonenumber { get; set; }
    }
}
