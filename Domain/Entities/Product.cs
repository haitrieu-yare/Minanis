using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [Range(0.0, double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
    public decimal BuyingPrice { get; set; } 
    public decimal SellingPrice { get; set; }
    public int Quantity { get; set; }
    public string Status { get; set; } = Constants.New;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}