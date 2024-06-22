using E_Library.Domain.Entities;
using E_Library.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Infrastructure.Repositories;

public class BookRepository: IBookRepository
{

    private readonly AppDbContext _appDbContext;

    public BookRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<Book> CreateBookAsync(Book book)
    {
        await _appDbContext.Books.AddAsync(book);
        await _appDbContext.SaveChangesAsync();
        return book;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _appDbContext.Books.ToListAsync();
    }

    public async Task DeleteBookAsync(Book book)
    {
        _appDbContext.Books.Remove(book);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Book?> GetBookById(int id)
    {
        return await _appDbContext.Books.Where(bk => bk.Id == id).FirstOrDefaultAsync();
    }
}