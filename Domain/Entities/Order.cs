using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }
    
    [Range(0.0, double.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public decimal TotalBuyingPrice { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public decimal TotalSellingPrice { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}