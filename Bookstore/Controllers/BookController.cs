using Bookstore.Models;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bookstore.Controllers;

public class BookController: Controller
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }
    
    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetBooks();
        var viewModel = new BookViewModel()
        {
            BookList = books.Data,
            Book = new Book()
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Store(Book book)
    {
        var response = await _bookService.CreateBook(book);

        if (!response.Success)
        {
            TempData["Message"] = response.Message;
        }

        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> EditBook(Book newBook)
    {
        
        if (ModelState.IsValid)
        {
            await _bookService.UpdateBook(newBook);
        }

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteBook(int id)
    {
        await _bookService.DeleteBook(id);

        return RedirectToAction("Index");
    }
}