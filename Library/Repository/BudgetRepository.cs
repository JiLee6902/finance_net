using Application.IRepository;
using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using Library.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
    {
        private readonly IMapper _mapper;
        private readonly PRN221Context _context;

        public BudgetRepository(PRN221Context context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddNewBudget(BudgetDTO budget)
        {

            Budget bud = _mapper.Map<Budget>(budget);
            await base.Add(bud);
            int i = await _context.SaveChangesAsync();
            if (i > 0)
            {
                return true;
            }

            return false;

        }

        public async Task<bool> DeleteBudget(int id)
        {
            var vc = await GetByID(id);
            if (vc != null)
            {
                vc.IsDelete = true;
                base.Update(vc);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<BudgetDTO>> GetAllBudget()
        {
            List<Budget> budgets = await base.GetAll();
            List<BudgetDTO> result = _mapper.Map<List<BudgetDTO>>(budgets);
            return result;
        }

        public async Task<IEnumerable<BudgetDTO>> GetAllBudgetsCategory(int CateID, int userID)
        {
            List<Budget> list = await _context.Budgets.Where(id => (id.CategoryId == CateID && id.UserId == userID)).ToListAsync();
            List<BudgetDTO> result = _mapper.Map<List<BudgetDTO>>(list);
            return result;
        }

        public async Task<IEnumerable<BudgetDTO>> GetAllBudgetsUserID(int userID)
        {
            List<Budget> list = await _context.Budgets.Where(id => id.UserId == userID).ToListAsync();
            List<BudgetDTO> result = _mapper.Map<List<BudgetDTO>>(list);
            return result;
        }

        public async Task<BudgetDTO?> GetBudgetByID(int id)
        {
            Budget? budget = await base.GetByID(id);
            BudgetDTO bud = _mapper.Map<BudgetDTO>(budget);
            return bud;

        }

        public async Task<bool> UpdateBudget(BudgetDTO budget)
        {
            Budget? vc = await base.GetByID(budget.BudgetId);
            if (vc != null)
            {
                _mapper.Map(budget, vc);
                base.Update(vc);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
