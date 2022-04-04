using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Application.Services.ProductServices.ProductDTOs;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class ProductCreationDto
{
    [Required]
    public string? Name { get; init; }

    [Required]
    public decimal? BuyingPrice { get; init; }

    [Required]
    public decimal? SellingPrice { get; init; }

    [Required]
    public int? Quantity { get; init; }
}