﻿using System.Globalization;
using System.IO.Compression;
using Expendio.Data;
using Expendio.DTO;
using Expendio.Mappers;
using Expendio.Repository;
using Expendio.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;

namespace Expendio.Controllers
{
    [Authorize]
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
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value;
            var Id = Convert.ToInt32(idClaim);

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            var dayexpense=await _expenseRepository.GetExpenseByDate(today,Id);
            var dayincome = await _expenseRepository.GetIncomeByDate(today, Id);

            var monthexpense = await _expenseRepository.GetExpenseByMonth(today.Year,today.Month, Id);
            var monthincome = await _expenseRepository.GetIncomeByMonth(today.Year, today.Month, Id);

            var yearexpense = await _expenseRepository.GetExpenseByYear(today.Year, Id);
            var yearincome = await _expenseRepository.GetIncomeByYear(today.Year, Id);

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
        }

        [Authorize]
        public IActionResult Analytics()
        {
            return View();
        }

        public JsonResult ChartDataYear()
        {
            int year = DateTime.Now.Year;

            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value;
            var Id = Convert.ToInt32(idClaim);

            var totalExpensesByMonth = _context.Expenses
                .Where(p=> p.UserId == Id)
                .Where(p => p.Date.Year == year)
                .GroupBy(p => p.Date.Month)
            .Select(g => new {
                 Month = g.Key,
                  Total = g.Sum(p => p.Amount) 
                 })
                .ToList();

            var totalIncomesByMonth = _context.Incomes
                .Where(p => p.Date.Year == year).
                Where(p => p.UserId == Id)
                .GroupBy(p => p.Date.Month) 
            .Select(g => new {
                Month = g.Key,
                Total = g.Sum(p => p.Amount) 
            })
                .ToList();

            var TotalData = new
            {
                Expenses = totalExpensesByMonth,
                Incomes = totalIncomesByMonth
            };

            return Json(TotalData);
        }

        public JsonResult ChartDataYearCategory()
        {
            int year = DateTime.Now.Year;

            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value;
            var Id = Convert.ToInt32(idClaim);

            var totalExpensesByCategory = _context.Expenses
                .Where(p => p.UserId == Id)
                .Where(p => p.Date.Year == year)
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(p => p.Amount)
                })
                .ToList();

            var totalIncomesBySource = _context.Incomes
                .Where(p => p.UserId == Id)
                .Where(p => p.Date.Year == year)
                .GroupBy(p => p.Source) 
                .Select(g => new
                {
                    Source = g.Key,
                    Total = g.Sum(p => p.Amount)
                })
                .ToList();


            var TotalData = new
            {
                Expenses = totalExpensesByCategory,
                Incomes = totalIncomesBySource
            };

            return Json(TotalData);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        public async Task<FileResult> ExportData()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var incomesFile = archive.CreateEntry("incomes.csv");
                    using (var streamWriter = new StreamWriter(incomesFile.Open()))
                    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        var incomes = await _expenseRepository.GetIncomes();
                        csvWriter.WriteRecords(incomes);
                    }
                    
                    var expensesFile = archive.CreateEntry("expenses.csv");
                    using (var streamWriter = new StreamWriter(expensesFile.Open()))
                    using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        var expenses = await _expenseRepository.GetExpenses();
                        csvWriter.WriteRecords(expenses); 
                    }
                }
                
                
                return File(memoryStream.ToArray(), "application/zip", "Data.zip");
            }
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
                    await _expenseRepository.AddIncome(incomeEntity);
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
                    await _expenseRepository.AddExpense(expenseEntity);
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


            var expenses = _context.Expenses
                .Where(expense => expense.UserId == Id)
                .OrderBy(expense => expense.Date)
                .ToList();

            var incomes = _context.Incomes
                .Where(income => income.UserId == Id)
                .OrderBy(income => income.Date)
                .ToList();


            var history = new HistoryViewModel { Expenses = expenses, Incomes = incomes };
            return View(history);
        }
    }
}
