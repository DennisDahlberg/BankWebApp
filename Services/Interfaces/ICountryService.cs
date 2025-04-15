using DataAccessLayer.DTOs;
using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICountryService
    {
        CountryStatsDTO GetACountriesStats(Country countryName);
        List<CountryStatsDTO> GetCountriesStats();
        List<SelectListItem> GetCountryEnums();
        Country GetEnumFromString(string country);
    }
}
