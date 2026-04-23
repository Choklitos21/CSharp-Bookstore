namespace Bookstore.Models;

public class BookViewModel
{
    public Book Book { get; set; } = new Book();

    public IEnumerable<Book> BookList { get; set; } = new List<Book>();
}