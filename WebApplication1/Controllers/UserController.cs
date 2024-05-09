using API_SYSTEM.Commons;
using Application.IRepository;
using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace API_SYSTEM.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly string ok = "successfully";
        private readonly string notFound = "Not found!";
        private readonly string badRequest = "Failed!";
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("get-all-user")]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<UserDTO> list = await _userRepository.GetAllUser();
            if (list.Any())
            {
                return Ok(new APIReponse<IEnumerable<UserDTO>>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Get user " + ok,
                    Data = list
                });
            }
            return NotFound(new APIReponse<IEnumerable<UserDTO>>
            {
                StatusCode = 404,
                Result = false,
                Message = notFound,
            });
        }
       
        [HttpGet("get-user-by-id")]
        public async Task<ActionResult> GetUserbyID(int id)
        {
            UserDTO user = await _userRepository.GetUserByID(id);
            if (user!=null)
            {
                return Ok(new APIReponse<UserDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Get user " + ok,
                    Data = user
                });
            }
            return NotFound(new APIReponse<UserDTO>
            {
                StatusCode = 404,
                Result = false,
                Message = notFound,
            });
        }
        
        [HttpGet("get-user-by-account-id")]
        public async Task<ActionResult> GetUserbyAccountID(int id)
        {
            UserDTO user = await _userRepository.GetUserByAccountID(id);
            if (user != null)
            {
                return Ok(new APIReponse<UserDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Get user " + ok,
                    Data = user
                });
            }
            return NotFound(new APIReponse<UserDTO>
            {
                StatusCode = 404,
                Result = false,
                Message = notFound,
            });
        }
        
        [HttpPost("add-user")]
        public async Task<ActionResult> AddNewUser(AddUserDTO user)
        {
            var user1 = await _userRepository.AddNewUser(user);
            if (user1)
            {
                return Ok(new APIReponse<AddUserDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Add user " + ok,
                    Data = user
                });
            }
            return BadRequest(new APIReponse<AddUserDTO>
            {
                StatusCode = 400,
                Result = false,
                Message = badRequest,
            });

        }
       
        [HttpPut("update-user")]
        public async Task<ActionResult> UpdateUser(AddUserDTO user)
        {
            var user1 = await _userRepository.UpdateUser(user);
            if (user1)
            {
                return Ok(new APIReponse<AddUserDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Add user " + ok,
                    Data = user
                });
            }
            return BadRequest(new APIReponse<AddUserDTO>
            {
                StatusCode = 400,
                Result = false,
                Message = badRequest,
            });

        }
        
        [HttpPatch("delete-user")]
        public async Task<ActionResult> DeteleUser(int id)
        {
            var user1 = await _userRepository.DeleteUser(id);
            if (user1)
            {
                return Ok(new APIReponse<AddUserDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Delete user " + ok,
                    
                });
            }
            return BadRequest(new APIReponse<AddUserDTO>
            {
                StatusCode = 400,
                Result = false,
                Message = badRequest,
            });

        }

    }
}
