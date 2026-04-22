using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    
    public DbSet<User> User { get; set; }
    public DbSet<Book> Book { get; set; }
    public DbSet<Loan> Loan { get; set; }
    public DbSet<UserLoans> UserLoans { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserLoans>(entity =>
        {
            entity.HasKey(u => new { u.UserId, u.LoanId });

            entity.HasOne(u => u.User)
                .WithMany(u => u.UserLoans)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(u => u.Loan)
                .WithMany(l => l.UserLoans)
                .HasForeignKey(u => u.LoanId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasOne(b => b.Loan)
                .WithMany(l => l.Books)
                .HasForeignKey(b => b.LoanId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}