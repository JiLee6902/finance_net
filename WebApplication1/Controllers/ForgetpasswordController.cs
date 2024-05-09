using Application.IRepository;
using Application.IService;
using BusinessObject.DTOs;
using BusinessObject.Models;
using BusinessObject.ServiceClass;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_SYSTEM.Controllers
{
    [Route("api/forget-password")]
    [ApiController]
    public class ForgetPasswordController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAuthRepository _authRepository;
        private readonly IMailService _mailService;
        private readonly IMD5HashingService _mD5HashingService;
        public ForgetPasswordController(IAuthService authService, IMailService mailService, IAuthRepository authRepository, IMD5HashingService mD5HashingService)
        {
            _authService = authService;
            _mailService = mailService;
            _authRepository = authRepository;
            _mD5HashingService = mD5HashingService;
        }

        [HttpGet("send-otp-code")]
        public async Task<ActionResult> SendOTPCode([FromQuery] string nameOrMail)
        {
            string sMessage = string.Empty;
            if (string.IsNullOrEmpty(nameOrMail))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Result = false,
                    Message = "Username or Email is required"
                });
            }

            Account acc = await _authRepository.GetAccountByNameOrEmail(nameOrMail);
            if (acc == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Result = false,
                    Message = "Account is not found"
                });
            }
            if (acc.IsBan)
            {
                return Unauthorized(new
                {
                    StatusCode = 401,
                    Result = false,
                    Message = "Account is ban"
                });
            }

            //Genarate OTP Code
            Random random = new Random();
            int otpCode = random.Next(100000, 999999);

            MailClass mailClass = new MailClass()
            {
                Subject = "Password changing OTP Code",
                Body = _mailService.GetMailBodyToChangePassword(otpCode),
                ToMailIds = new List<string>()
                    {
                        acc.Email
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

            var hashedOtpCode = _mD5HashingService.getMD5Hash(otpCode.ToString());
            //Append OTP Hashed Code into cookie
            var cookiesOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(5)
            };
            Response.Cookies.Append("otpCode" + acc.AccountId, hashedOtpCode, cookiesOptions);

            return Ok(new
            {
                StatusCode = 200,
                Result = true,
                Message = "Email sent successfully",
                Data = hashedOtpCode + ":" + acc.AccountId
            });

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            if (Request.Cookies.ContainsKey("otpCode" + resetPasswordDTO.AccountId))
            {
                string hashedOtpCode = Request.Cookies["otpCode" + resetPasswordDTO.AccountId].ToString();
                if (_mD5HashingService.getMD5Hash(resetPasswordDTO.OTPCode.ToString()).Equals(hashedOtpCode))
                {
                    string sMessage = "";
                    sMessage = await _authRepository.ChangePassword(resetPasswordDTO.UserNameOrEmail, resetPasswordDTO.Password);
                    if (!sMessage.Equals("Change password successfully"))
                    {
                        return BadRequest(new
                        {
                            StatusCode = 400,
                            Result = false,
                            Message = sMessage
                        });
                    }
                    Response.Cookies.Delete("otpCode" + resetPasswordDTO.AccountId);
                    return Ok(new
                    {
                        StatusCode = 200,
                        Result = true,
                        Message = sMessage
                    });
                }
            }
            return Unauthorized();
        }

    }
}
