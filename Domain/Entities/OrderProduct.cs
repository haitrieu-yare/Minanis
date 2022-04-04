using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class OrderProduct
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("OrderId")]
    public int OrderId { get; set; }

    [ForeignKey("ProductId")]
    public int ProductId { get; set; }

    [Range(0.0, int.MaxValue, ErrorMessage = DomainConstants.RangeValidation)]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    [Range(0.0, double.MaxValue, ErrorMessage = DomainConstants.RangeValidation)]
    public decimal TotalBuyingPrice { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    [Range(0.0, double.MaxValue, ErrorMessage = DomainConstants.RangeValidation)]
    public decimal TotalSellingPrice { get; set; }
}