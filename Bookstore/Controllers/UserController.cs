using Bookstore.Models;
using Bookstore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bookstore.Controllers;

public class UserController: Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetUsers();
        var viewModel = new UserViewModel()
        {
            UserList = users.Data,
            User = new User()
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Store(User user)
    {
        var response = await _userService.CreateUser(user);

        if (!response.Success)
        {
            TempData["Message"] = response.Message;
        }

        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> EditUser(User newUser)
    {
        
        if (ModelState.IsValid)
        {
            await _userService.UpdateUser(newUser);
        }

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUser(id);

        return RedirectToAction("Index");
    }
}