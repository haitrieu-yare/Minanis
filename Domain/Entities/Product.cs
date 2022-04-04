using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class Product
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,4)")]
    [Range(0.0, double.MaxValue, ErrorMessage = DomainConstants.RangeValidation)]
    public decimal BuyingPrice { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    [Range(0.0, double.MaxValue, ErrorMessage = DomainConstants.RangeValidation)]
    public decimal SellingPrice { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = DomainConstants.RangeValidation)]
    public int Quantity { get; set; }

    public string Status { get; set; } = DomainConstants.New;
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}