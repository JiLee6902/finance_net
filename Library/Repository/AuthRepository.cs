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
    public class AuthRepository : GenericRepository<Account>, IAuthRepository
    {
        private readonly PRN221Context _context;
        private readonly IPasswordHashingService _passwordHashingService;
        public AuthRepository(PRN221Context context,
            IPasswordHashingService passwordHashingService
           ) : base(context)
        {
            _passwordHashingService = passwordHashingService;
            _context = context;
        }

        public async Task<string> ChangePassword(string nameOrMail, string password)
        {
            try
            {
                Account acc = await GetAccountByNameOrEmail(nameOrMail);
                if (acc == null)
                {
                    return "Account is not found";
                }
                byte[] salt = _passwordHashingService.GenerateSalt();
                string hashedPassword = _passwordHashingService.HashPassword(password, salt);
                acc.Password = hashedPassword;
                acc.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return "Change password successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Account> CheckLogin(LoginDTO loginDTO)
        {
            var acc = await GetAccountByNameOrEmail(loginDTO.UserName);
            if (acc != null)
            {
                if (_passwordHashingService.VerifyPassword(loginDTO.Password, acc.Password) == true)
                {
                    return acc;
                }
            }
            return null;
        }

        public async Task<string> ConfirmMail(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return "Invalid Username";
            }
            Account acc = await GetAccountByNameOrEmail(username);
            if (acc == null)
            {
                return "Username or Email is not found";
            }
            acc.IsBan = false;
            Update(acc);
            await _context.SaveChangesAsync();
            return "Mail Confirmed";
        }

        public async Task<Account> GetAccountByNameOrEmail(string nameOrMail)
        {
            var account = await _context.Accounts
                .SingleOrDefaultAsync(a => (a.UserName.Equals(nameOrMail) || a.Email.Equals(nameOrMail)));
            return account;
        }

        public async Task<Account> RegisterAccount(AccountDTO accountDTO)
        {
            try
            {
                if (await GetAccountByNameOrEmail(accountDTO.UserName) != null || await GetAccountByNameOrEmail(accountDTO.Email) != null)
                {
                    return null;
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
                    IsBan = true
                };
                await Add(newAcc);
                int isSuccess = await _context.SaveChangesAsync();
                if (isSuccess > 0)
                {
                    return newAcc;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<User> RegisterUser(UserDTO userDTO)
        {
            var newUser = new User
            {
                AccountId = userDTO.AccountId,
                DisplayName = userDTO.Phone,
                Balance = userDTO.Balance,
                Birthdate = userDTO.Birthdate,
                UpdateDate = DateTime.Now,
                IsDelete = false
            };
            await _context.Users.AddAsync(newUser);
            int isSuccess = await _context.SaveChangesAsync();
            if (isSuccess >= 0)
            {
                return newUser;
            }
            return null;
        }
    }
}
