using Bookstore.Data;
using Bookstore.Enums;
using Bookstore.Models;
using Bookstore.Response;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Services;

public class LoanService
{
    private readonly AppDbContext _context;
    private readonly UserService _userService;
    private readonly BookService _bookService;

    public LoanService(AppDbContext appDbContext, UserService userService, BookService bookService)
    {
        _context = appDbContext;
        _userService = userService;
        _bookService = bookService;
    }
    
    public async Task<ResponseService<List<Loan>>> GetLoans()
    {
        var loans = await _context.Loan
            .Include(l => l.UserLoans)
                .ThenInclude(ul => ul.User)
            .Include(l => l.Books)
            .ToListAsync();
        
        return new ResponseService<List<Loan>>(
            loans,
            loans.Count > 0 ? "Users Loaded" : "No users on db yet",
            loans.Count > 0 ? true : false);
    }
    
    public async Task<Loan> CreateLoan(int userId, List<int> bookIds, DateTime date, LoanStatus status)
    {
        var user = await _userService.GetUserById(userId);
        if (user.Data is null) throw new Exception("User not found");

        var books = await _bookService.GetBooksById(bookIds);
        if (books.Data is null) throw new Exception("Books not found");

        var loan = new Loan
        {
            Date = DateOnly.FromDateTime(date),
            Status = status,
            Books = books.Data,
            UserLoans = new List<UserLoans>
            {
                new UserLoans { UserId = userId }
            }
        };

        _context.Loan.Add(loan);
        await _context.SaveChangesAsync();

        return loan;
    }
}