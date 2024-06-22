using E_Library.Domain.Entities;
using E_Library.Domain.Repositories;
using E_Library.Dtos.Book;

namespace E_Library.Application.Services.Books;

public class BookServices
{
    private readonly IBookRepository _bookRepository;

    public BookServices(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }


    public async Task<Book> CreateBookAsync(BookDto bookDto)
    {
        // var bookExist = await _bookRepository.GetBookById(bookDto.Id);
        //
        // if (bookExist != null)
        // {
        //     throw new Exception("Livro ja cadastrado");
        // }

        var newBook = new Book()
        {
            Title = bookDto.Title,
            Genre = bookDto.Genre,
            Synopsis = bookDto.Synopsis,
            AuthorId = bookDto.AuthorId,
            CoverUrl = bookDto.CoverUrl,
            Value = bookDto.Value,
            AvailableQuantity = bookDto.AvailableQuantity
        };

        try
        {
            await _bookRepository.CreateBookAsync(newBook);
            return newBook;
        }
        catch (Exception exception)
        {
            throw new Exception("Erro ao cadastrar livro", exception);
        }
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        try
        {
           var books = await _bookRepository.GetAllBooksAsync();
           return books;
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao buscar livros");
        }
    }
}