using Expendio.Data;
using Expendio.DTO;
using Expendio.Mappers;
using Expendio.Models;
using Expendio.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Expendio.Controllers
{
    public class UserController : Controller
    {
        private readonly ExpendioDbContext _context;
        public UserController(ExpendioDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Analytics()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> AddIncome(IncomeDto incomeDto, int userId)
        {
            var income = incomeDto.ToIncome(userId);
            await _context.Incomes.AddAsync(income);
            return NoContent();
        }

        public IActionResult History() 
        {
            var expenses = _context.Expenses.ToList<Expense>();
            var incomes = _context.Incomes.ToList<Income>();
            Console.WriteLine("Ss");

            var history = new HistoryViewModel { Expenses = expenses, Incomes = incomes };
            return View(history);
        }
    }
}
