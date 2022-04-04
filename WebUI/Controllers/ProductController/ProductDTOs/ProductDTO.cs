using System.Diagnostics.CodeAnalysis;
using Domain.Entities;

namespace WebUI.Controllers.ProductController.ProductDTOs;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class ProductDto
{
    public ProductDto(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        BuyingPrice = product.BuyingPrice;
        SellingPrice = product.SellingPrice;
        Quantity = product.Quantity;
        Status = product.Status;
        CreatedDate = product.CreatedDate;
    }

    public int? Id { get; set; }
    public string? Name { get; set; }
    public decimal? BuyingPrice { get; set; }
    public decimal? SellingPrice { get; set; }
    public int? Quantity { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedDate { get; set; }
}