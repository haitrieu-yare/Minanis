using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }
    
    [Column(TypeName = "decimal(18,4)")]
    [Range(0.0, double.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public decimal TotalBuyingPrice { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    [Range(0.0, double.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public decimal TotalSellingPrice { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}