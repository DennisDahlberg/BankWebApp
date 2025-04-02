using BankWebApp.Infrastructure.Paging;
using DataAccessLayer.Models;
using Mapster;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Infrastructure
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<PagedResult<Customer>, List<CustomerViewModel>>.NewConfig()
                .MapWith(src => src.Results.Adapt<List<CustomerViewModel>>());
        }
    }
}
