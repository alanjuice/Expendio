﻿using Expendio.Models;

namespace Expendio.Services
{
    public interface IExpenseRepository
    {
        Task<IList<Expense>> GetExpenseByDate(DateOnly date,int Id);
        Task<IList<Income>> GetIncomeByDate(DateOnly date, int Id);
        Task<IList<Income>> GetIncomeByMonth(int year, int month, int Id);
        Task<IList<Expense>> GetExpenseByMonth(int year, int month, int Id);
        Task<IList<Expense>> GetExpenseByYear(int year, int Id);
        Task<IList<Income>> GetIncomeByYear(int year, int Id);
        Task AddIncome(Income income);
        Task AddExpense(Expense expense);
    }
}
