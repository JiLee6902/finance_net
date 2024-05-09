using BusinessObject.DTOs;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IAccountRepository:IGenericRepository<Account>
    {
        public Task<Account> GetAccountByNameOrEmail(string nameOrMail);
        public Task<string> AddAccount(AccountDTO accountDTO);
        public Task<string> BanAccount(int id);
        public Task<string> UnbanAccount(int id);
        public Task<string> UpdateAccount(UpdateAccountDTO updateAccountDTO);
    }
}
