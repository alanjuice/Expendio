namespace Expendio.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public int Amount { get; set; }

        public DateOnly Date { get; set; } = new DateOnly();
        public User? User { get; set; }
        public int UserId { get; set; }
    }
}
