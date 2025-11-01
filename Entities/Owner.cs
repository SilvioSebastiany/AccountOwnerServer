using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Owner
{
    [Key]
    [Column("OwnerId")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Name is a required field.")]
    [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Date of birth is a required field.")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Address is a required field.")]
    [MaxLength(100, ErrorMessage = "Maximum length for the Address is 100 characters.")]
    public string? Address { get; set; }

    // Navigation property - Um Owner pode ter múltiplas Accounts
    public ICollection<Account>? Accounts { get; set; }
}