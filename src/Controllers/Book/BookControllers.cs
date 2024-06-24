using E_Library.Application.Services.Books;
using E_Library.Dtos.Book;
using Microsoft.AspNetCore.Mvc;

namespace E_Library.Controllers.Book;

[ApiController]
[Route("api/v1/books")]
public class BookControllers : ControllerBase
{
    private readonly BookServices _bookServices;

    public BookControllers(BookServices bookServices)
    {
        _bookServices = bookServices;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateBookAsync([FromBody] BookDto bookDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { error = "Body inválido" });
        }
        
        var book = await _bookServices.CreateBookAsync(bookDto);
        return Created("v1/api/books", book);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllBooksAsync()
    {
        var books = await _bookServices.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookByIdAsync([FromRoute] int id)
    {
        var book = await _bookServices.GetBookById(id);
        return Ok(book);
    }
}