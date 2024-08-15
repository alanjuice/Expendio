using Expendio.DTO;
using Expendio.Models;

namespace Expendio.Mappers
{
    public static class ExpenseMapper
    {
        public static Expense ToExpense(this AddExpenseDto dto,int userId)
        {
            return new Expense()
            {
                UserId = userId,
                Amount = dto.Amount,
                Category = dto.Category,
                Date = dto.Date,
            };
        }
    }
}
