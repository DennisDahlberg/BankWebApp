using DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
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
        [RegularExpression(@"^\d{4,5}$", ErrorMessage = "Zipcode must be 4 or 5 digits")]
        public string Zipcode { get; set; }

        [RegularExpression(@"^\d{6}-[A-Za-z0-9]{4,5}$", ErrorMessage = "Example of valid Id (980521-3824)")]
        public string? NationalId { get; set; }

        [EmailAddress]
        public string? Emailaddress { get; set; }

        
        [RegularExpression(@"^\d{8,10}", ErrorMessage = "Phone number must be between 8-10 digits")]
        public string? Telephonenumber { get; set; }
    }
}
