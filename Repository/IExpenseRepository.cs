using Expendio.Models;

namespace Expendio.Services
{
    public interface IExpenseRepository
    {
        Task<IList<Expense>> GetExpenseByDate(DateOnly date);
        Task<IList<Income>> GetIncomeByDate(DateOnly date);
        Task<IList<Income>> GetIncomeByMonth(int year, int month);
        Task<IList<Expense>> GetExpenseByMonth(int year, int month);
        Task<IList<Expense>> GetExpenseByYear(int year);
        Task<IList<Income>> GetIncomeByYear(int year);
    }
}
