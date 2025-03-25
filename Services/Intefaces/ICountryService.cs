using DataAccessLayer.DTOs;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Intefaces
{
    public interface ICountryService
    {
        CountryStatsDTO GetACountriesStats(string countryName);
        List<CountryStatsDTO> GetCountriesStats();
        List<SelectListItem> GetCountryEnums();
        Country GetEnumFromString(string country);
    }
}
