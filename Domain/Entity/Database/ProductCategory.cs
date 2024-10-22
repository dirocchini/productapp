namespace Domain.Entity.Database;

public class ProductCategory
{
    public int ProductId { get; set; }   
    public ProductEntity Product { get; set; }


    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
}
