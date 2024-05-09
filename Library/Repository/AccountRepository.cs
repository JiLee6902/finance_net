using Application.IRepository;
using Application.IService;
using BusinessObject.DTOs;
using BusinessObject.Models;
using Library.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly PRN221Context _context;
        private readonly IPasswordHashingService _passwordHashingService;
        public AccountRepository(PRN221Context context,
            IPasswordHashingService passwordHashingService) : base(context)
        {
            _context = context;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<Account> GetAccountByNameOrEmail(string nameOrMail)
        {
            var account = await _context.Accounts
                .SingleOrDefaultAsync(a => (a.UserName.Equals(nameOrMail) || a.Email.Equals(nameOrMail)));
            return account;
        }

        public async Task<string> AddAccount(AccountDTO accountDTO)
        {
            if (await GetAccountByNameOrEmail(accountDTO.UserName) != null || await GetAccountByNameOrEmail(accountDTO.Email) != null)
            {
                return "Name or Email has been existed";
            }
            byte[] salt = _passwordHashingService.GenerateSalt();
            string hashedPassword = _passwordHashingService.HashPassword(accountDTO.Password, salt);
            var newAcc = new Account
            {
                UserName = accountDTO.UserName,
                Password = hashedPassword,
                Email = accountDTO.Email,
                Type = 2,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsBan = false
            };
            await Add(newAcc);
            int isSuccess = await _context.SaveChangesAsync();
            if (isSuccess >= 0)
            {
                return "Add success";
            }
            return "Add failed";
        }

        public async Task<string> BanAccount(int id)
        {
            var acc = await GetByID(id);
            if (acc == null)
            {
                return "Account not found";
            }
            acc.IsBan = true;
            Update(acc);
            int isSuccess = await _context.SaveChangesAsync();
            if (isSuccess > 0)
            {
                return "Ban success";
            }
            return "Ban failed";
        }

        public async Task<string> UnbanAccount(int id)
        {
            var acc = await GetByID(id);
            if (acc == null)
            {
                return "Account not found";
            }
            acc.IsBan = false;
            Update(acc);
            int isSuccess = await _context.SaveChangesAsync();
            if (isSuccess > 0)
            {
                return "Unban success";
            }
            return "Unban failed";
        }

        public async Task<string> UpdateAccount(UpdateAccountDTO updateAccountDTO)
        {
            var acc = await GetByID(updateAccountDTO.AccountId);
            if (acc == null)
            {
                return "Account not found";
            }
            var acc01 = await GetAccountByNameOrEmail(updateAccountDTO.UserName);
            var acc02 = await GetAccountByNameOrEmail(updateAccountDTO.Email);
            if (acc01 != null)
            {
                if (acc01.AccountId != updateAccountDTO.AccountId)
                {
                    return "Name or Email has been existed";
                }
            }
            if (acc02 != null)
            {
                if (acc02.AccountId != updateAccountDTO.AccountId)
                {
                    return "Name or Email has been existed";
                }
            }
            byte[] salt = _passwordHashingService.GenerateSalt();
            string hashedPassword = _passwordHashingService.HashPassword(updateAccountDTO.Password, salt);
            acc.UserName = updateAccountDTO.UserName;
            acc.Password = updateAccountDTO.Password;
            acc.Email = updateAccountDTO.Email;
            Update(acc);
            int isSuccess = await _context.SaveChangesAsync();
            if (isSuccess > 0)
            {
                return "Update success";
            }
            return "Update failed";
        }
    }
}
