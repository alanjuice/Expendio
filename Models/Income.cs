using Microsoft.VisualBasic;

namespace Expendio.Models
{
    public class Income
    {
        public int Id { get; set; }
        public string Source { get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateOnly Date { get; set; } = new DateOnly();
        public User? User { get; set; }
        public int UserId { get; set; }
     
    }
}
