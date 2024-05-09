using BusinessObject.DTOs;
using BusinessObject.Models;

namespace Application.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<UserDTO>> GetAllUser();
        Task<UserDTO> GetUserByID(int id);
        Task<UserDTO> GetUserByAccountID(int aid);
        Task<bool> AddNewUser(AddUserDTO userDTO);
        Task<bool> UpdateUser(AddUserDTO userDTO);
        Task<bool> DeleteUser(int id);
        
    }
}
