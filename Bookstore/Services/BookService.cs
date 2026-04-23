using Bookstore.Data;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using Bookstore.Response;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Services;

public class BookService
{
    private readonly AppDbContext _context;

    public BookService(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    public async Task<ResponseService<List<Book>>> GetBooks()
    {
        var books = await _context.Book.ToListAsync();
        return new ResponseService<List<Book>>(
            books,
            books.Count > 0 ? "Users Loaded" : "No users on db yet",
            books.Count > 0 ? true : false);
    }
    
    public async Task<ResponseService<Book>> CreateBook(Book newBook)
    {
        var book = await _context.Book.AnyAsync(u => u.Title == newBook.Title);

        if (book)
        {
            return new ResponseService<Book>(
                newBook,
                "Book already registered",
                false);
        }

        await _context.AddAsync(newBook);
        await _context.SaveChangesAsync();
        return new ResponseService<Book>(
            newBook,
            "Book registered",
            true);
    }
    
    public async Task<ResponseService<Book>> UpdateBook(Book newBook)
    {
        var oldBook = await _context.Book.FirstOrDefaultAsync(x => x.Id == newBook.Id);
        
        if (oldBook != null)
        {
            _context.Entry(oldBook).CurrentValues.SetValues(newBook);
            await _context.SaveChangesAsync();
            return new ResponseService<Book>(
                newBook,
                "Book updated correctly",
                true);
        }
        
        return new ResponseService<Book>(
            newBook,
            "Book not found",
            false); 
    }
    
    public async Task<ResponseService<Book>> DeleteBook(int id)
    {
        var oldBook = await _context.Book.FirstOrDefaultAsync(x => x.Id == id);
        
        if (oldBook != null)
        {
            _context.Remove(oldBook);
            await _context.SaveChangesAsync();
            return new ResponseService<Book>(
                oldBook,
                "Book removed",
                true);
        }
        
        return new ResponseService<Book>(
            oldBook,
            "Book not found",
            false);
    }
    
}