using E_Library.Domain.Entities;

namespace E_Library.Domain.Repositories;

public interface IBookRepository
{
    Task<Book> CreateBookAsync(Book book);
    Task GetAllBooksAsync();
    Task DeleteBookAsync(int id);
    Task GetBookById(int id);
}