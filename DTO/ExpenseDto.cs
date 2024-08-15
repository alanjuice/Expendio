using Expendio.Models;

namespace Expendio.DTO
{
    public class ExpenseDto
    {
        public string Category { get; set; } = string.Empty;
        public int Amount { get; set; }

        public DateOnly Date { get; set; } = new DateOnly();
    }
}
