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

    [Range(0.0, int.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public int Quantity { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public decimal TotalBuyingPrice { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public decimal TotalSellingPrice { get; set; }
}