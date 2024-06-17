using E_Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<Loan> Loans { get; set; }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString = "Server=localhost;Database=e_library;User=jpdev;Password=12345678;";
        optionsBuilder
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}