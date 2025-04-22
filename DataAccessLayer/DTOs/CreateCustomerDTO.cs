using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class CreateCustomerDTO
    {
        public string Gender { get; set; }

        public string Givenname { get; set; }

        public string Surname { get; set; }

        public string Streetaddress { get; set; }

        public string City { get; set; }

        public Country Country { get; set; }

        public string Zipcode { get; set; }

        public string? NationalId { get; set; }

        public string Emailaddress { get; set; }

        public string Telephonenumber { get; set; }
    }
}
