namespace BankWebApp.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string GivenName { get; set; } = null!;
        public string SurName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
