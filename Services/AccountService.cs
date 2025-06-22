using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }
        public Account GetAccountById(int id)
        {
            return _accountRepository.GetAccountById(id);
        }

        public Customer GetCustomerByAccountId(int id)
        {
            return _accountRepository.GetCustomerByAccountId(id);
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }
    }
}
