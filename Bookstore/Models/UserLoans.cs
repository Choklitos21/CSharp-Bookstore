namespace Bookstore.Models;

public class UserLoans
{
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int LoanId { get; set; }
    public Loan Loan { get; set; }
    
}