using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IAccountRepository
    {
        Account GetAccountById(int id);
        IEnumerable<Account> GetAllAccounts();
        Customer GetCustomerByAccountId(int accountId);
    }
}
