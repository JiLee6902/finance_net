using BusinessObject.DTOs;
using BusinessObject.Models;

namespace Application.IRepository
{
    public interface IBudgetRepository:IGenericRepository<Budget>
    {
        Task<IEnumerable<BudgetDTO>> GetAllBudget();
        Task<IEnumerable<BudgetDTO>> GetAllBudgetsCategory(int CateID, int userID);
        Task<IEnumerable<BudgetDTO>> GetAllBudgetsUserID(int userID);
        Task<BudgetDTO?> GetBudgetByID(int id);
        Task<bool> AddNewBudget(BudgetDTO budget);
        Task<bool> UpdateBudget(BudgetDTO budget);
        Task<bool> DeleteBudget(int id);
    }
}
