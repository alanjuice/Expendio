using Expendio.DTO;
using Expendio.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Expendio.Mappers
{
    public static class IncomeMapper
    {
        public static Income ToIncome(this IncomeDto incomeDto, int userId)
        {
            return new Income
            {
                Date = incomeDto.Date,
                Amount = incomeDto.Amount,
                Source = incomeDto.Source,
                UserId = userId
                
            };
        }

        public static IncomeDto ToIncomeDTO(this Income income)
        {
            return new IncomeDto()
            {
                Amount = income.Amount,
                Source = income.Source,
                Date = income.Date                
            };
        }
    }
}
