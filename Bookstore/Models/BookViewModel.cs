namespace Bookstore.Models;

public class BookViewModel
{
    public Book Book { get; set; } = new Book();

    public ICollection<Book> BookList { get; set; } = new List<Book>();
}