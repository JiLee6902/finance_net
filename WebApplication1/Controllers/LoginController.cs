using API_SYSTEM.Middleware;
using Application.IRepository;
using Application.IService;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using Library.Service;
using API_SYSTEM.Commons;
using BusinessObject.Models;

namespace API_SYSTEM.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAuthService _authService;
        public LoginController(IAuthRepository authRepository, IAuthService authService)
        {
            _authRepository = authRepository;
            _authService = authService;
        }

        [HttpPost("")]
        public async Task<ActionResult> CheckLogin([FromBody] LoginDTO loginDTO)
        {
            var acc = await _authRepository.CheckLogin(loginDTO);
            if (acc == null)
            {
                return Unauthorized(new
                {
                    StatusCode = 401,
                    Result = false,
                    Message = "Wrong username or password"
                });
            }

            if (acc.IsBan == true)
            {
                return Unauthorized(new
                {
                    StatusCode = 401,
                    Result = false,
                    Message = "Unauthorized"
                });
            }

            //Generate token
            string role = "";
            if (acc.Type == 1)
            {
                role = "Admin";
            }
            else
            {
                role = "User";
            }
            var token = _authService.GenerateToken(acc.UserName, role);
            return Ok(new 
            {
                StatusCode = 200,
                Result = true,
                Message = "Login sucessfully",
                Data = new
                {
                    acc,
                    token
                }
            });
        }
      
    }
}
