using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        Account GetAccountById(int id);
        Customer GetCustomerByAccountId(int id);

    }
}
