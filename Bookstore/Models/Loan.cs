using Bookstore.Enums;

namespace Bookstore.Models;

public class Loan
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public LoanStatus Status { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
    
    public ICollection<UserLoans> UserLoans { get; set; } = new List<UserLoans>();
}