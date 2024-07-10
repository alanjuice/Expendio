namespace Expendio.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }
        public IEnumerable<Expense>? Expenses { get; set; }
        public IEnumerable<Income>? Incomes { get; set; }
    }
}
