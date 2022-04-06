using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain;

namespace Application.Services.ProductServices.ProductDTOs;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class ProductCreationDto
{
    [Required]
    public string? Name { get; init; }

    [Required]
    [Range(0.0, double.MaxValue, ErrorMessage = DomainConstants.RangeValidation)]
    public decimal? BuyingPrice { get; init; }

    [Required]
    [Range(0.0, double.MaxValue, ErrorMessage = DomainConstants.RangeValidation)]
    public decimal? SellingPrice { get; init; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = DomainConstants.RangeValidation)]
    public int? Quantity { get; init; }
}