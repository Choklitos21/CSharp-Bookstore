using Bookstore.Enums;
using Bookstore.Models;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers;

public class LoanController: Controller
{
    private readonly LoanService _loanService;
    private readonly UserService _userService;
    private readonly BookService _bookService;

    public LoanController(LoanService loanService, UserService userService, BookService bookService)
    {
        _loanService = loanService;
        _userService = userService;
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var loans = await _loanService.GetLoans();
        var users = await _userService.GetUsers();
        var books = await _bookService.GetBooks();
        
        var viewModel = new LoanViewModel()
        {
            Loan = new Loan(),
            Loans = loans.Data,
            Users = users.Data,
            Books = books.Data
        };
        return View(viewModel);
    }

    public async Task<IActionResult> Store(int userId, List<int> bookIds, DateTime date)
    {
        var response = await _loanService.CreateLoan(userId, bookIds, date, LoanStatus.OnLoan);
        
        if (response == null)
        {
            TempData["Message"] = "holi";
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> EditLoan(int loanId, DateTime date, List<int> newBooksIds)
    {
        var response = await _loanService.UpdateLoan(loanId, date, newBooksIds);
        
        if (response == null)
        {
            TempData["Message"] = "holi";
        }

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        await _loanService.DeleteLoan(id);

        return RedirectToAction("Index");
    }
}