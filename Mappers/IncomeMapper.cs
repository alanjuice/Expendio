using Expendio.DTO;
using Expendio.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Expendio.Mappers
{
    public static class IncomeMapper
    {
        public static Income ToIncome(this AddIncomeDto incomeDto, int userId)
        {
            return new Income
            {
                Date = incomeDto.Date,
                Amount = incomeDto.Amount,
                Source = incomeDto.Source,
                UserId = userId
                
            };
        }
    }
}
