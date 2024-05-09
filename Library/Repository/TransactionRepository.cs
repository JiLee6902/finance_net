using Application.IRepository;
using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Library.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Library.Repository
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly PRN221Context _context;
        private readonly IMapper _mapper;

        public TransactionRepository(PRN221Context context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        private void UpdateBalanceSpending(int id, decimal amount)
        {
            User? user = _context.Users.FirstOrDefault(i => i.UserId == id);
            if (user != null)
            {
                user.Balance = user.Balance - amount;
                user.UpdateDate = DateTime.Now;
                _context.Users.Update(user);
                _context.SaveChanges();
            }

        }
        private void UpdateBalanceIncome(int id, decimal amount)
        {
            User? user = _context.Users.FirstOrDefault(i => i.UserId == id);
            if (user != null)
            {
                user.Balance = user.Balance + amount;
                user.UpdateDate = DateTime.Now;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }


        public async Task<bool> AddNewTransaction(AddTransactionDTO transaction)
        {
            Transaction tt = _mapper.Map<Transaction>(transaction);
            await base.Add(tt);
            if (tt.CategoryId == 1)
            {
                UpdateBalanceSpending(tt.UserId, tt.Amount);
            }
            else if (tt.CategoryId == 2)
            {
                UpdateBalanceIncome(tt.UserId, tt.Amount);
            }
            int vc = await _context.SaveChangesAsync();
            if (vc > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTransaction(int transactionID)
        {
            Transaction? trans = await base.GetByID(transactionID);
            if (trans != null)
            {
                trans.IsDelete = true;
                base.Update(trans);
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactionByUserID(int userID)
        {
            List<Transaction> transactions = await _context.Transactions
                    .Where(i => i.User.UserId == userID)
                    .ToListAsync();
            List<TransactionDTO> list = _mapper.Map<List<TransactionDTO>>(transactions);
            return list;
        }

        public async Task<TransactionDTO> GetTransactionID(int ID)
        {
            Transaction? trans = await base.GetByID(ID);
            TransactionDTO tran = _mapper.Map<TransactionDTO>(trans);
            return tran;
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactionWithDate(int cateID, int userID, DateTime startDate, DateTime endDate)
        {
            List<Transaction> trans = await _context.Transactions
                .Where(i => i.Category.CategoryId == cateID && i.UserId == userID && i.TransactionsDate == startDate && i.TransactionsDate == endDate)
                .ToListAsync();
            List<TransactionDTO> list = _mapper.Map<List<TransactionDTO>>(trans);
            return list;
        }

        public async Task<bool> UpdateTransaction(TransactionDTO transaction)
        {
            Transaction? tran = await GetByID(transaction.TransId);
            if (tran != null)
            {
                _mapper.Map(tran, transaction);
                base.Update(tran);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactionWithCate(int cateID, int userID)
        {
            List<Transaction> trans = await _context.Transactions.Where(i => i.Category.CategoryId == cateID && i.UserId == userID).ToListAsync();
            List<TransactionDTO> list = _mapper.Map<List<TransactionDTO>>(trans);
            return list;
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransaction()
        {
            List<Transaction> trans = await _context.Transactions.ToListAsync();
            List<TransactionDTO> list = _mapper.Map<List<TransactionDTO>>(trans);
            return list;
        }

        public async Task<IEnumerable<StatisticTransactionDTO>> GetTransactionByStatistic(int userID, int cateID, DateTime startDate, DateTime endDate)
        {
            var statisticList = new List<StatisticTransactionDTO>();
            if (cateID == 0)
            {
                statisticList = await _context.Transactions.Include(t => t.Category)
                   .Where(t => (t.TransactionsDate >= startDate && t.TransactionsDate <= endDate) && t.UserId == userID && t.IsDelete == false)
                   .Select(obj => new StatisticTransactionDTO
                   {
                       TransId = obj.TransId,
                       CategoryType = obj.Category.CategoryType,
                       Merchant = obj.Merchant,
                       Amount = obj.Amount,
                       Notes = obj.Notes,
                       TransactionsDate = obj.TransactionsDate
                   })
                   .OrderByDescending(t => t.TransactionsDate).ToListAsync();
            }
            else
            {
                statisticList = await _context.Transactions.Include(t => t.Category)
                    .Where(t => (t.CategoryId == cateID && t.UserId == userID && t.IsDelete == false && (t.TransactionsDate >= startDate && t.TransactionsDate <= endDate)))
                    .Select(obj => new StatisticTransactionDTO
                    {
                        TransId = obj.TransId,
                        CategoryType = obj.Category.CategoryType,
                        Merchant = obj.Merchant,
                        Amount = obj.Amount,
                        Notes = obj.Notes,
                        TransactionsDate = obj.TransactionsDate
                    })
                    .OrderByDescending(t => t.TransactionsDate).ToListAsync();
            }
            return statisticList;
        }
    }
}
