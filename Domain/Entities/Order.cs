using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    public string Status { get; set; }
    public Dictionary<int, int> Products { get; set; } = new();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}