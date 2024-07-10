using Microsoft.VisualBasic;

namespace Expendio.Models
{
    public class Income
    {
        public int Id { get; set; }
        public string? Source { get; set; }
        public int Amount { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
    }
}
