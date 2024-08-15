using Expendio.Models;
using System.Collections;

namespace Expendio.ViewModel
{
    public class IndexViewModel
    {
        public IList<Expense>? DayExpenses;
        public IList<Expense>? MonthExpenses;
        public IList<Expense>? YearExpenses;

        public IList<Income>? DayIncomes;
        public IList<Income>? MonthIncomes;
        public IList<Income>? YearIncomes;
    }
}
