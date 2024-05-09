using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessObject.Models;
using Application.IService;
using Application.IRepository;
using BusinessObject.DTOs;
using BusinessObject.ServiceClass;

namespace API_SYSTEM.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMailService _mailService;
        public RegisterController(IAuthRepository authRepository, IMailService mailService)
        {
            _authRepository = authRepository;
            _mailService = mailService;
        }

        [HttpPost("register-account")]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] AccountDTO accountDTO, string callConfirmMailURL)
        {
            string sMessage = string.Empty;
            //Add new account whose email is not verified
            var newAcc = await _authRepository.RegisterAccount(accountDTO);
            if (newAcc == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Result = true,
                    Message = "Username or Email has been existed"
                });
            }

            //Send verify mail
            MailClass mailClass = new MailClass()
            {
                Subject = "Mail Confirmation",
                Body = _mailService.GetMailBodyToConfirmMail(callConfirmMailURL, accountDTO),
                ToMailIds = new List<string>()
                {
                    accountDTO.Email
                }
            };
            sMessage = await _mailService.SendMail(mailClass);
            if (!sMessage.Equals("Mail is sent successfully"))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Result = true,
                    Message = sMessage
                });
            }
            return Ok(new
            {
                StatusCode = 200,
                Result = true,
                Message = "Email sent successfully",
                Data = newAcc
            });
        }

        [HttpPost("confirm-mail")]
        public async Task<ActionResult> ConfirmMail([FromBody] string username)
        {
            string sMessage = await _authRepository.ConfirmMail(username);
            return Ok(new
            {
                StatusCode = 200,
                Result = true,
                Message = sMessage
            });
        }

        [HttpPost("register-user")]
        public async Task<ActionResult<UserDTO>> CreateCandidate([FromBody] UserDTO userDTO)
        {
            var can = await _authRepository.RegisterUser(userDTO);
            return Ok(new
            {
                StatusCode = 200,
                Result = true,
                Message = $"Create new user successfully",
                Data = can
            });

        }
    }
}
