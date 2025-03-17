using DataAccessLayer.DTOs;
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
    }
}
