namespace Bookstore.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    
    public int LoanId { get; set; }
    public Loan Loan { get; set; }
}
