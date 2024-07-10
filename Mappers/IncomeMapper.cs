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
                Amount = incomeDto.Amount,
                Source = incomeDto.Source,
                UserId = userId
            };
        }
    }
}
