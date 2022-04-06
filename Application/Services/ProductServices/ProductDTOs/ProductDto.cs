using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain;
using Domain.Entities;

namespace Application.Services.ProductServices.ProductDTOs;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class ProductDto
{
    // Zero parameter constructor allow object to be created without assign all the properties
    public ProductDto()
    {
    }

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

    [Range(0.0, double.MaxValue, ErrorMessage = DomainConstants.RangeValidation)]
    public decimal? BuyingPrice { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = DomainConstants.RangeValidation)]
    public decimal? SellingPrice { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = DomainConstants.RangeValidation)]
    public int? Quantity { get; set; }

    public string? Status { get; set; }
    public DateTime? CreatedDate { get; set; }
}