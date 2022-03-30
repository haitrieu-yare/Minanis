using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ProductBatch
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("ProductId")]
    public int ProductId { get; set; }
    public decimal BuyingPrice { get; set; } 
    public decimal SellingPrice { get; set; } 
    public int Quantity { get; set; }
    public string Status { get; set; } = Constants.New;
    public DateTime? ExpiredDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}