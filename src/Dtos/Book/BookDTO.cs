namespace E_Library.Dtos.Book;

public record BookDto(string Title, string Genre, string Synopsis, string CoverUrl, float Value, int AvailableQuantity, int AuthorId)
{
    
}