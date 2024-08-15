namespace Expendio.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public IEnumerable<Expense>? Expenses { get; set; }
        public IEnumerable<Income>? Incomes { get; set; }
    }
}
