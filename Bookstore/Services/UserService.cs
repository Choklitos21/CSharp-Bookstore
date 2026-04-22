using Bookstore.Data;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using Bookstore.Response;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }

    public async Task<ResponseService<List<User>>> GetUsers()
    {
        var users = await _context.User.ToListAsync();
        return new ResponseService<List<User>>(
            users,
            users.Count > 0 ? "Users Loaded" : "No users on db yet",
            users.Count > 0 ? true : false);
    }
    
    public async Task<ResponseService<User>> CreateUser(User newUser)
    {
        var user = await _context.User.AnyAsync(u => u.Email == newUser.Email);

        if (user)
        {
            return new ResponseService<User>(
                newUser,
                "User already registered",
                false);
        }

        await _context.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return new ResponseService<User>(
            newUser,
            "User registered",
            true);
    }
    
    public async Task<ResponseService<User>> UpdateUser(User newUser)
    {
        var oldEvent = await _context.User.FirstOrDefaultAsync(x => x.Id == newUser.Id);
        
        if (oldEvent != null)
        {
            _context.Entry(oldEvent).CurrentValues.SetValues(newUser);
            await _context.SaveChangesAsync();
            return new ResponseService<User>(
                newUser,
                "User updated correctly",
                true);
        }
        
        return new ResponseService<User>(
            newUser,
            "User not found",
            false); 
    }
    
    public async Task<ResponseService<User>> DeleteUser(int id)
    {
        var oldUser = await _context.User.FirstOrDefaultAsync(x => x.Id == id);
        
        if (oldUser != null)
        {
            _context.Remove(oldUser);
            await _context.SaveChangesAsync();
            return new ResponseService<User>(
                oldUser,
                "User removed",
                true);
        }
        
        return new ResponseService<User>(
            oldUser,
            "User not found",
            false);
    }
    
}