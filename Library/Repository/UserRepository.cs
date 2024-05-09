using Application.IRepository;
using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using Library.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly PRN221Context _context;
        private readonly IMapper _mapper;
        public UserRepository(PRN221Context context, IMapper mapper): base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddNewUser(AddUserDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            await base.Add(user);
            int i = await _context.SaveChangesAsync();
            if (i > 0)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            User? user = await base.GetByID(id);
            if (user != null)
            {
                user.IsDelete = true;
                base.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUser()
        {
            List<User> get = await _context.Users.Include(i => i.Account).ToListAsync();
            List<UserDTO> list = _mapper.Map<List<UserDTO>>(get);
            return list;
        }

        public async Task<UserDTO> GetUserByAccountID(int aid)
        {
            User? user = await _context.Users.Include(i => i.Account).SingleOrDefaultAsync(i => i.AccountId == aid);
            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO> GetUserByID(int id)
        {
            User? uid = await _context.Users.Include(i => i.Account).SingleOrDefaultAsync(i => i.UserId == id);
            UserDTO idDTO = _mapper.Map<UserDTO>(uid);
            return idDTO;
        }

        public async Task<bool> UpdateUser(AddUserDTO userDTO)
        {
            User? use = await GetByID(userDTO.UserId);
            _mapper.Map(userDTO, use);
            if (use != null)
            {
                base.Update(use);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
