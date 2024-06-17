using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Domain.Entities;

[Table("Book")]
public class Book
{
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public string? Synopsis { get; set; }
    public string? CoverUrl { get; set; }
    public float Value { get; set; }
    public int AvailableQuantity { get; set; }
    
    [ForeignKey("AuthorId")]
    public int AuthorId { get; set; }
}