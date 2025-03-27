using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MoneyLaunderingService
    {
        private readonly BankAppDataContext _dbContext;

        public MoneyLaunderingService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }



    }
}
