using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class Account
{
    [Key]
    [Column("AccountId")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Date created is a required field.")]
    public DateTime DateCreated { get; set; }

    [Required(ErrorMessage = "Account type is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the AccountType is 30 characters.")]
    public string? AccountType { get; set; }

    // Foreign key para Owner
    [ForeignKey(nameof(Owner))]
    public Guid OwnerId { get; set; }

    // Navigation property - Uma Account pertence a um Owner
    public Owner? Owner { get; set; }
}