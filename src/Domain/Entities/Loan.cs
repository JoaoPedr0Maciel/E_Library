using System.ComponentModel.DataAnnotations.Schema;
using E_Library.Domain.Enums;

namespace E_Library.Domain.Entities;

[Table("Loan")]
public class Loan
{
    public int Id { get; set; }
    
    [ForeignKey("BookId")]
    public int BookId { get; set; }

    [ForeignKey("UserId")]
    public int UserId { get; set; }

    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public LoanStatus Status { get; set; }
}