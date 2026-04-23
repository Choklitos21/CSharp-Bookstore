namespace Bookstore.Models;

public class LoanViewModel
{
    public Loan? Loan { get; set; }

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<Book> Books { get; set; } = new List<Book>();
    
    public List<int> BookIds { get; set; } = new List<int>();
    
}