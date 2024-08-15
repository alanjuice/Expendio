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

        public async Task<IList<Expense>> GetExpenseByDate(DateOnly date)
        {
            var data = await _context.Expenses.Where(e => e.Date == date).ToListAsync();
            return data;
        }

        public async Task<IList<Income>> GetIncomeByDate(DateOnly date)
        {
            var data = await _context.Incomes.Where(e => e.Date == date).ToListAsync();
            return data;
        }

        public async Task<IList<Income>> GetIncomeByMonth(int year, int month)
        {
            var data = await _context.Incomes.Where(e => e.Date.Year == year && e.Date.Month == month).ToListAsync();
            return data;
        }

        public async Task<IList<Expense>> GetExpenseByMonth(int year, int month)
        {
            var data = await _context.Expenses.Where(e => e.Date.Year==year && e.Date.Month==month).ToListAsync();
            return data;
        }

        public async Task<IList<Expense>> GetExpenseByYear(int year)
        {
            var data = await _context.Expenses.Where(e => e.Date.Year == year).ToListAsync();
            return data;
        }

        public async Task<IList<Income>> GetIncomeByYear(int year)
        {
            var data = await _context.Incomes.Where(e => e.Date.Year == year).ToListAsync();
            return data;
        }
    }
}
