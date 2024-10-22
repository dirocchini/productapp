using Application.AutoMapper;
using Domain.Entity.Database;
namespace Application.ViewModels;

public class ProductViewModel : IMapProfile<ProductEntity, ProductViewModel>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }

    public List<ProductCategoryViewModel> ProductCategories { get; set; }
}
public class ProductCategoryViewModel : IMapProfile<ProductCategory, ProductCategoryViewModel>
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
}
