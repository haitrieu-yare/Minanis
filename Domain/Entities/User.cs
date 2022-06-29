using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = DomainConstants.NewUser;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Status { get; set; } = DomainConstants.InUse;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}