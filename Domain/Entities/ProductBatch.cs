using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ProductBatch
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("ProductId")]
    public int ProductId { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public decimal BuyingPrice { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public decimal SellingPrice { get; set; }

    [Range(0.0, int.MaxValue, ErrorMessage = Constants.RangeValidation)]
    public int Quantity { get; set; }

    public string Status { get; set; } = Constants.New;
    public DateTime? ExpiredDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}