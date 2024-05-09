using BusinessObject.DTOs;
using BusinessObject.Models;


namespace Application.IRepository
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<IEnumerable<TransactionDTO>> GetAllTransactionWithDate(int cateID, int userID, DateTime startDate, DateTime endDate);
        Task<IEnumerable<TransactionDTO>> GetAllTransactionWithCate(int cateID, int userID);
        Task<IEnumerable<TransactionDTO>> GetAllTransactionByUserID(int userID);
        Task<IEnumerable<TransactionDTO>> GetAllTransaction();
        Task<TransactionDTO> GetTransactionID(int ID);
        Task<bool> AddNewTransaction(AddTransactionDTO transaction);
        Task<bool> UpdateTransaction(TransactionDTO transaction);
        Task<bool> DeleteTransaction(int transactionID);
        Task<IEnumerable<StatisticTransactionDTO>> GetTransactionByStatistic(int userID, int cateID, DateTime startDate, DateTime endDate);
    }
}
