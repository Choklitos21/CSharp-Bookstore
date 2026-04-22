namespace Bookstore.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    private string _document { get; set; }
    public int Age { get; set; }

    public ICollection<UserLoans> UserLoans { get; set; } = new List<UserLoans>();
}