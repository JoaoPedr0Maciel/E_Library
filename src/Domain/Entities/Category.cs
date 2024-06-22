using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    [ForeignKey("BookId")] 
    public int BookId { get; set; }
}