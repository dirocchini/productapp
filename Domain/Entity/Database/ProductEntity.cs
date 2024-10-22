using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entity.Database;

public class ProductEntity : BaseEntity
{
    protected ProductEntity() { }

    public ProductEntity(string name, decimal value)
    {
        Name = name;
        Value = value;
    }
    public string Name { get; set; }
    public decimal Value { get; set; }

    public List<ProductCategory> ProductCategories { get; set; }

    public ValidationResult Validate()
    {
        var validator = new ProductEntityValidator();
        return validator.Validate(this);
    }

    public void AddCategories(List<CategoryEntity> categories)
    {
        ProductCategories ??= [];

        foreach (var category in categories)
            AddCategory(category);
    }

    public void AddCategory(CategoryEntity category)
    {
        ProductCategories ??= [];
        var categoryExists = ProductCategories.FirstOrDefault(c => c.CategoryId == category.Id);

        if (categoryExists is null)
            ProductCategories.Add(new ProductCategory() { CategoryId = category.Id });
    }

    public void RemoveCategory(int categoryId)
    {
        ProductCategories ??= [];
        ProductCategories = ProductCategories.Where(c => c.CategoryId != categoryId).ToList();
    }
}

public class ProductEntityValidator : AbstractValidator<ProductEntity>
{
    public ProductEntityValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Product name should not be empty");

        RuleFor(c => c.Value)
            .GreaterThan(0).WithMessage("Product value should be greater than 0");
    }
}