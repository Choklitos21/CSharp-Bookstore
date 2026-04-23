namespace Bookstore.Models;

public class UserViewModel
{
    public User User { get; set; } = new User();

    public ICollection<User> UserList { get; set; } = new List<User>();
}