using Domain.Entities;

namespace Domain.Specifications.Products;

public class ProductByProfit : BaseSpecification<Product>
{
    public ProductByProfit()
    {
        AddOrderByDescending(x => x.SellingPrice - x.BuyingPrice);
    }
}