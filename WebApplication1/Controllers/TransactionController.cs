using API_SYSTEM.Commons;
using Application.IRepository;
using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SYSTEM.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transactionRepository )
        {
            _transactionRepository = transactionRepository;
        }
        [HttpGet("get-all-transaction")]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<TransactionDTO> trans = await _transactionRepository.GetAllTransaction();
            if (trans.Any())
            {
                return Ok(new APIReponse<IEnumerable<TransactionDTO>>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Get transaction all successfully",
                    Data = trans
                });
            }
            return NotFound(new APIReponse<IEnumerable<TransactionDTO>>
            {
                StatusCode = 404,
                Result = false,
                Message = "Not found",
            });
        }
        
        [HttpGet("get-all-transaction-by-user-id")] 
        public async Task<ActionResult> GetAllByUserID(int uid)
        {
            IEnumerable<TransactionDTO> trans = await _transactionRepository.GetAllTransactionByUserID(uid);
            if (trans.Any())
            {
                return Ok(new APIReponse<IEnumerable<TransactionDTO>>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Get transaction all successfully",
                    Data = trans
                });
            }
            return NotFound(new APIReponse<IEnumerable<TransactionDTO>>
            {
                StatusCode = 404,
                Result = false,
                Message = "Not found",
            });
        }
        
        [HttpGet("get-all-transaction-by-category-id")]
        public async Task<ActionResult> GetAllByCateID(int cid, int userID)
        {
            IEnumerable<TransactionDTO> trans = await _transactionRepository.GetAllTransactionWithCate(cid, userID);
            if (trans.Any())
            {
                return Ok(new APIReponse<IEnumerable<TransactionDTO>>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Get transaction all successfully",
                    Data = trans
                });
            }
            return NotFound(new APIReponse<IEnumerable<TransactionDTO>>
            {
                StatusCode = 404,
                Result = false,
                Message = "Not found",
            });
        }
        
        [HttpGet("get-transaction-by-id")]
        public async Task<ActionResult> GetByID(int id)
        {
            TransactionDTO trans = await _transactionRepository.GetTransactionID(id);
            if (trans != null)
            {
                return Ok(new APIReponse<TransactionDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Get transaction successfully",
                    Data = trans
                });
            }
            return NotFound(new APIReponse<TransactionDTO>
            {
                StatusCode = 404,
                Result = false,
                Message = "Not found",
            });
        }
        [HttpPost("add-transaction")]
        public async Task<ActionResult> AddTransaction(AddTransactionDTO transactionDTO)
        {
            var trans = await _transactionRepository.AddNewTransaction(transactionDTO);
            if (trans)
            {
                return Ok(new APIReponse<AddTransactionDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Add successfully",
                    Data = transactionDTO
                });
            }
            return BadRequest(new APIReponse<AddTransactionDTO>
            {
                StatusCode = 400,
                Result = false,
                Message = "Add failed!",
            });

        }
        
        [HttpPut("update-transaction")]
        public async Task<ActionResult> UpdateTransaction(TransactionDTO transactionDTO)
        {
            var trans = await _transactionRepository.UpdateTransaction(transactionDTO);
            if (trans)
            {
                return Ok(new APIReponse<TransactionDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Update successfully",
                    Data = transactionDTO
                });
            }
            return BadRequest(new
            {
                StatusCode = 400,
                Result = false,
                Message = "Update failed!",
            });

        }
        
        [HttpPatch("delete-transaction")]
        public async Task<ActionResult> DeleteTransaction(TransactionDTO transactionDTO)
        {
            var trans = await _transactionRepository.DeleteTransaction(transactionDTO.UserId);
            if (trans)
            {
                return Ok(new APIReponse<TransactionDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Deleted successfully",
                });
            }
            return BadRequest(new
            {
                StatusCode = 400,
                Result = false,
                Message = "Deleted failed!",
            });

        }

    }
}
