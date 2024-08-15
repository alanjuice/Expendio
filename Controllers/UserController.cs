using Expendio.Data;
using Expendio.DTO;
using Expendio.Mappers;
using Expendio.Models;
using Expendio.Services;
using Expendio.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expendio.Controllers
{
    public class UserController : Controller
    {
        private readonly ExpendioDbContext _context;
        private readonly IExpenseRepository _expenseRepository;
        public UserController(ExpendioDbContext context,IExpenseRepository expenseRepository)
        {
            _context = context;
            _expenseRepository = expenseRepository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            var dayexpense=await _expenseRepository.GetExpenseByDate(today);
            var dayincome = await _expenseRepository.GetIncomeByDate(today);

            var monthexpense = await _expenseRepository.GetExpenseByMonth(today.Year,today.Month);
            var monthincome = await _expenseRepository.GetIncomeByMonth(today.Year, today.Month);

            var yearexpense = await _expenseRepository.GetExpenseByYear(today.Year);
            var yearincome = await _expenseRepository.GetIncomeByYear(today.Year);

            var viewModel = new IndexViewModel()
            {
                DayExpenses = dayexpense,
                DayIncomes = dayincome,
                MonthExpenses = monthexpense,
                MonthIncomes = monthincome,
                YearIncomes = yearincome,
                YearExpenses = yearexpense,
            };

            return View(viewModel);

            //Current day expenses/incomes
            //Current week/incomes
            //Current month
            //Current year
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
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddIncome(int id, IncomeDto incomeDto)
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
        public async Task<IActionResult> AddExpense(ExpenseDto expenseDto)
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


            var expenses = _context.Expenses.Where(expense => expense.UserId == Id).ToList<Expense>();
            var incomes = _context.Incomes.Where(income => income.UserId == Id).ToList<Income>();

            var history = new HistoryViewModel { Expenses = expenses, Incomes = incomes };
            return View(history);
        }
    }
}
