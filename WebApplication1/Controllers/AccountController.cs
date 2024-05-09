using API_SYSTEM.Commons;
using Application.IRepository;
using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SYSTEM.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("get-all-account")]
        public async Task<ActionResult> GetAll()
        {
            var accountlist = await _accountRepository.GetAll();
            if (!accountlist.Any())
            {
                return NotFound(new APIReponse<List<Account>>
                {
                    StatusCode = 200,
                    Result = false,
                    Message = $"Currently no account!",
                    Data = accountlist
                });
            }
            return Ok(new APIReponse<List<Account>>
            {
                StatusCode = 200,
                Result = true,
                Message = $"Get account list successful!",
                Data = accountlist
            });

        }
        [HttpPost("add-account")]

        public async Task<ActionResult> Add(AccountDTO addAccount)
        {
            var addMessage = await _accountRepository.AddAccount(addAccount);
            if (!addMessage.Equals("Add success"))
            {
                return BadRequest(new APIReponse<List<Account>>
                {
                    StatusCode = 400,
                    Result = true,
                    Message = addMessage
                });
            }
            return Ok(new
            {
                StatusCode = 200,
                Result = true,
                Message = addMessage
            });
        }

        [HttpPatch("ban-account")]
        public async Task<ActionResult> Ban(int id)
        {
            string banMessage = await _accountRepository.BanAccount(id);
            if (banMessage.Equals("Account not found"))
            {
                return NotFound(new APIReponse<Account>
                {
                    StatusCode = 404,
                    Result = true,
                    Message = banMessage
                });
            }
            return Ok(new APIReponse<Account>
            {
                StatusCode = 200,
                Result = true,
                Message = banMessage
            });
        }

        [HttpPatch("unban-account")]
        public async Task<ActionResult> Unban(int id)
        {
            string unbanMessage = await _accountRepository.UnbanAccount(id);
            if (unbanMessage.Equals("Account not found"))
            {
                return NotFound(new APIReponse<Account>
                {
                    StatusCode = 404,
                    Result = true,
                    Message = unbanMessage
                });
            }
            return Ok(new APIReponse<Account>
            {
                StatusCode = 200,
                Result = true,
                Message = unbanMessage
            });
        }

        [HttpPut("update-account")]
        public async Task<ActionResult> Update(UpdateAccountDTO updateAccountDTO)
        {
            string updateMessage = await _accountRepository.UpdateAccount(updateAccountDTO);
            if (updateMessage.Equals("Account not found"))
            {
                return NotFound(new APIReponse<Account>
                {
                    StatusCode = 404,
                    Result = true,
                    Message = updateMessage
                });
            }
            if (!updateMessage.Equals("Update success"))
            {
                return BadRequest(new APIReponse<Account>
                {
                    StatusCode = 400,
                    Result = true,
                    Message = updateMessage
                });
            } 
            return Ok(new APIReponse<Account>
            {
                StatusCode = 200,
                Result = true,
                Message = updateMessage
            });
        }
    }
}
