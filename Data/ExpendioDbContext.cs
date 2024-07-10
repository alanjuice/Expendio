using Microsoft.EntityFrameworkCore;
using Expendio.Models;

namespace Expendio.Data
{
    public class ExpendioDbContext:DbContext
    {
        public ExpendioDbContext(DbContextOptions options):base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Income> Incomes { get; set; }
    }
}

