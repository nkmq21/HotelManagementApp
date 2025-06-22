using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        public Account GetAccountById(int id)
        {
            return AccountDAO.GetAccountById(id);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return AccountDAO.GetAllAccount();
        }

        public Customer GetCustomerByAccountId(int accountId)
        {
            return AccountDAO.GetCustomerByAccountId(accountId);
        }
    }
}
