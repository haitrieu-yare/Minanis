using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [Range(0.0, double.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public decimal BuyingPrice { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public decimal SellingPrice { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public int Quantity { get; set; }

    public string Status { get; set; } = Constants.New;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}