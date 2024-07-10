namespace Expendio.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public int Amount { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
    }
}
