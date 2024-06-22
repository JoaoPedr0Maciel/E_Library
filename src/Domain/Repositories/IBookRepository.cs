using E_Library.Domain.Entities;

namespace E_Library.Domain.Repositories;

public interface IBookRepository
{
    Task<Book> CreateBookAsync(Book book);
    Task<List<Book>> GetAllBooksAsync();
    Task DeleteBookAsync(Book book);
    Task<Book?> GetBookById(int id);
}