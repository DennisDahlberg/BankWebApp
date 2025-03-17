using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Intefaces
{
    public interface ICountryService
    {
        CountryStatsViewModel GetACountriesStats(string countryName);
        List<CountryStatsViewModel> GetCountriesStats();
    }
}
