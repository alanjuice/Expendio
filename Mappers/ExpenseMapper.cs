using Expendio.DTO;
using Expendio.Models;

namespace Expendio.Mappers
{
    public static class ExpenseMapper
    {
        public static Expense ToExpense(this ExpenseDto dto,int userId)
        {
            return new Expense()
            {
                UserId = userId,
                Amount = dto.Amount,
                Category = dto.Category,
                Date = dto.Date,
            };
        }

        public static ExpenseDto ToExpenseDTO(this Expense expense)
        {
            return new ExpenseDto()
            {
                Amount = expense.Amount,
                Category = expense.Category,
                Date = expense.Date,
            };
        }
    }
}
