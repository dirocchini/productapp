namespace Domain.Entity.Database;

public class CategoryEntity : BaseEntity
{
    public string Name { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
}
