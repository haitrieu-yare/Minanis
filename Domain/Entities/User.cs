namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = Constants.NewUser;
    public string Email { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Status { get; set; } = Constants.InUse;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}