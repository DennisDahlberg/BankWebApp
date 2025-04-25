using BankWebApp.Infrastructure.Paging;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITransactionService
    {
        PagedResult<Transaction> GetAllTransactionsFromCustomer(int accountId, int page);
        Task<List<SuspiciousTransactionDTO>> GetSuspiciousTransactions();
    }
}
