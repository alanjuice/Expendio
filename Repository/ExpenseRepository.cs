using Expendio.Data;
using Expendio.Models;
using Microsoft.EntityFrameworkCore;

namespace Expendio.Services
{
    public class ExpenseRepository:IExpenseRepository
    {
        private readonly ExpendioDbContext _context;
        public ExpenseRepository(ExpendioDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Expense>> GetExpenseByDate(DateOnly date, int Id)
        {
            var data = await _context.Expenses.Where(e => e.Date == date && e.UserId==Id).ToListAsync();
            return data;
        }

        public async Task<IList<Income>> GetIncomeByDate(DateOnly date, int Id)
        {
            var data = await _context.Incomes.Where(e => e.Date == date && e.UserId == Id).ToListAsync();
            return data;
        }

        public async Task<IList<Income>> GetIncomeByMonth(int year, int month, int Id)
        {
            var data = await _context.Incomes.Where(e => e.Date.Year == year && e.Date.Month == month && e.UserId == Id).ToListAsync();
            return data;
        }

        public async Task<IList<Expense>> GetExpenseByMonth(int year, int month, int Id)
        {
            var data = await _context.Expenses.Where(e => e.Date.Year==year && e.Date.Month==month && e.UserId == Id).ToListAsync();
            return data;
        }

        public async Task<IList<Expense>> GetExpenseByYear(int year, int Id)
        {
            var data = await _context.Expenses.Where(e => e.Date.Year == year && e.UserId == Id).ToListAsync();
            return data;
        }

        public async Task<IList<Income>> GetIncomeByYear(int year, int Id)
        {
            var data = await _context.Incomes.Where(e => e.Date.Year == year && e.UserId == Id).ToListAsync();
            return data;
        }
    }
}
