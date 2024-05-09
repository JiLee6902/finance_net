using API_SYSTEM.Commons;
using Application.IRepository;
using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SYSTEM.Controllers
{
    [Route("api/budget")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetRepository _budgetRepository;
        public BudgetController(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        [HttpGet("get-all-budget")]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<BudgetDTO> vc = await _budgetRepository.GetAllBudget();
            if (vc.Any())
            {
                return Ok(new APIReponse<IEnumerable<BudgetDTO>>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "get all budget successfully",
                    Data = vc
                });
            } 
            return NotFound(new APIReponse<IEnumerable<BudgetDTO>>
            {
                StatusCode = 404,
                Result = false,
                Message = "Budget not found",
            });
        }

        [HttpGet("get-bugdet-by-id")]
        public async Task<ActionResult> GetBudgetByID(int id)
        {
            var vc = await _budgetRepository.GetBudgetByID(id);
            if (vc != null)
            {
                return Ok(new APIReponse<BudgetDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "get budget successfully",
                    Data = vc
                });
            }
            return NotFound(new APIReponse<BudgetDTO>
            {
                StatusCode = 404,
                Result = false,
                Message = "Not found",
            });
        }

        [HttpGet("get-all-budget-by-user-id")]
        public async Task<ActionResult> GetBudgetByUserID(int uid)
        {
            IEnumerable<BudgetDTO> vc = await _budgetRepository.GetAllBudgetsUserID(uid);
            if (vc.Any())
            {
                return Ok(new APIReponse<IEnumerable<BudgetDTO>>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "get all budget successfully",
                    Data = vc
                });
            }
            return NotFound(new APIReponse<BudgetDTO>
            {
                StatusCode = 404,
                Result = false,
                Message = "Not found",
            });
        }

        [HttpGet("get-all-budget-by-category-id-and-user-id")]
        public async Task<ActionResult> GetBudgetByCateIDAndUserID(int cid, int uid)
        {
            IEnumerable<BudgetDTO> vc = await _budgetRepository.GetAllBudgetsCategory(cid, uid);
            if (vc.Any())
            {
                return Ok(new APIReponse<IEnumerable<BudgetDTO>>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "get all budget successfully",
                    Data = vc
                });
            }
            return NotFound(new APIReponse<IEnumerable<BudgetDTO>>
            {
                StatusCode = 404,
                Result = false,
                Message = "Not found",
            });
        }

        [HttpPost("add-budget")]
        public async Task<ActionResult<Budget>> AddNewBudget(BudgetDTO budget)
        {
            var vc = await _budgetRepository.AddNewBudget(budget);
            if (vc)
            {
                return Ok(new APIReponse<BudgetDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "add budget successfully",
                    Data = budget
                });
            }
            return NotFound(new APIReponse<BudgetDTO>
            {
                StatusCode = 404,
                Result = false,
                Message = "Not found",
            });
        }

        [HttpPut("update-budget")]
        public async Task<ActionResult> UpdateBudget(BudgetDTO budget)
        {
            var vc = await _budgetRepository.UpdateBudget(budget);
            if (vc)
            {
                return Ok(new APIReponse<BudgetDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Update budget successfully",
                    Data = budget
                });
            }
            return NotFound(new APIReponse<BudgetDTO>
            {
                StatusCode = 404,
                Result = false,
                Message = "Not found",
            });
        }

        [HttpPatch("delete-budget")]
        public async Task<ActionResult<Budget>> DeleteBudget(int id)
        {
            var vc = await _budgetRepository.DeleteBudget(id);
            if (vc)
            {
                return Ok(new APIReponse<BudgetDTO>
                {
                    StatusCode = 200,
                    Result = true,
                    Message = "Deleted successfully",
                });
            }
            return NotFound(new APIReponse<BudgetDTO>
            {
                StatusCode = 404,
                Result = false,
                Message = "Not found",
            });
        }
    }
}
