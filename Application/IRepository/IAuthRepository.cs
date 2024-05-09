using BusinessObject.DTOs;
using BusinessObject.Models;

namespace Application.IRepository
{
    public interface IAuthRepository:IGenericRepository<Account>
    {
        Task<Account> RegisterAccount(AccountDTO accountDTO);
        Task<User> RegisterUser(UserDTO userDTO);
        Task<string> ConfirmMail(string username);
        Task<Account> CheckLogin(LoginDTO loginDTO);
        Task<string> ChangePassword(string nameOrMail, string password);
        Task<Account> GetAccountByNameOrEmail(string nameOrMail);
    }
}
