using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Entities.Models;

[Table("Account")]
public class Account
{
    public Guid AccountId { get; set; }

    [Required(ErrorMessage = "Date Created is required.")]
    public DateTime DateCreated { get; set; }

    [Required(ErrorMessage = "Account Type is required.")]
    public string AccountType { get; set; }

    [ForeignKey(nameof(Owner))]
    public Guid OwnerId { get; set; }

    public Models.Owner? Owner { get; set; }
}
