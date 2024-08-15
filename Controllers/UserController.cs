using Expendio.Data;
using Expendio.DTO;
using Expendio.Mappers;
using Expendio.Models;
using Expendio.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Expendio.Controllers
{
    public class UserController : Controller
    {
        private readonly ExpendioDbContext _context;
        private readonly IHttpContextAccessor _Accessor;
        public UserController(ExpendioDbContext context, IHttpContextAccessor accessor)
        {
            _context = context;
            _Accessor = accessor;
        }

        [Authorize]
        public IActionResult Index()
        {
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value;
            var Id = Convert.ToInt32(idClaim);
            
            return View();
        }

        [Authorize]
        public IActionResult Analytics()
        {
            return View();
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddIncome(int id, AddIncomeDto incomeDto)
        {
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value;
            var Id = Convert.ToInt32(idClaim);

            if (ModelState.IsValid)
            {
                try
                {
                    var incomeEntity = incomeDto.ToIncome(Id);
                    await _context.Incomes.AddAsync(incomeEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "User");
                }
                catch (Exception ex)
                {
                    return Content(ex.ToString());
                }
            }
            return View(incomeDto);
        }

        [Authorize]
        public async Task<IActionResult> AddExpense(AddExpenseDto expenseDto)
        {
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value;
            var Id = Convert.ToInt32(idClaim);

            if (ModelState.IsValid)
            {
                try
                {
                    var expenseEntity = expenseDto.ToExpense(Id);
                    await _context.Expenses.AddAsync(expenseEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "User");
                }
                catch (Exception ex)
                {
                    return Content(ex.ToString());
                }
            }
            return View(expenseDto);
        }

        [Authorize]
        public IActionResult History() 
        {
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value;
            var Id = Convert.ToInt32(idClaim);


            var expenses = _context.Expenses.Where(expense => expense.Id == Id).ToList<Expense>();
            var incomes = _context.Incomes.Where(income => income.Id == Id).ToList<Income>();

            var history = new HistoryViewModel { Expenses = expenses, Incomes = incomes };
            return View(history);
        }
    }
}
