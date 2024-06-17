using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Domain.Entities;

[Table("Author")]
public class Author
{
    public int Id { get; set; }
    public string? Name { get; set; }
}