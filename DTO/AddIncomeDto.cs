using Expendio.Models;

namespace Expendio.DTO
{
    public class AddIncomeDto
    {
        public string Source { get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateOnly Date { get; set; } = new DateOnly();
    }
}
