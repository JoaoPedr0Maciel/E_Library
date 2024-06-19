using E_Library.Domain.Entities;
using E_Library.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Infrastructure.Repositories;

public class BookRepository(AppDbContext appDbContext) : IBookRepository
{
    
    
    public async Task<Book> CreateBookAsync(Book book)
    {
        await appDbContext.Books.AddAsync(book);
        await appDbContext.SaveChangesAsync();
        return book;
    }

    public Task GetAllBooksAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteBookAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task GetBookById(int id)
    {
        throw new NotImplementedException();
    }
}