using Domain.Entity.Database;

namespace UnitTests.Domain;

public class ProductUnitTests
{
    public ProductUnitTests()
    {

    }

    [Fact]
    public void CreateProduct_ShouldBe_Valid()
    {

        //Arrange
        var validName = "some product";
        var valueGreatedThanZero = 123m;

        var product = new ProductEntity(validName, valueGreatedThanZero);

        //Act
        var validation = product.Validate();

        //Assert
        Assert.True(validation.IsValid);
    }

    [Fact]
    public void CreateProduct_ValueZero_ShouldBeInvalid()
    {

        //Arrange
        var validName = "some product";
        var valueZero = 0m;

        var product = new ProductEntity(validName, valueZero);

        //Act
        var validation = product.Validate();

        //Assert
        Assert.False(validation.IsValid);
    }
    [Fact]
    public void CreateProduct_EmptyName_ShouldBeInvalid()
    {

        //Arrange
        var emptyName = "";
        var valueGreatedThanZero = 123m;

        var product = new ProductEntity(emptyName, valueGreatedThanZero);

        //Act
        var validation = product.Validate();

        //Assert
        Assert.False(validation.IsValid);
    }
    [Fact]
    public void Product_AddCategories_ShouldAdd()
    {
        //Arrange
        var categories = new List<CategoryEntity>
        {
            new CategoryEntity() { Id = 1, Name = "Category 1" },
            new CategoryEntity() { Id = 2, Name = "Category 2" }
        };

        var product = new ProductEntity("Valid Product Name", 123.4m);

        //Act
        product.AddCategories(categories);

        //Assert
        Assert.Equal(2m, product.ProductCategories.Count(), 0);
    }
    [Fact]
    public void Product_AddCategory_ShouldAdd()
    {
        //Arrange
        var category = new CategoryEntity() { Id = 1, Name = "Category 1" };
        var product = new ProductEntity("Valid Product Name", 123.4m);

        //Act
        product.AddCategory(category);

        //Assert
        Assert.Equal(1m, product.ProductCategories.Count(), 0);
    }
    [Fact]
    public void Product_AddCategoryWithItemsAdded_ShouldAdd()
    {
        //Arrange
        var product = new ProductEntity("Valid Product Name", 123.4m);
    
        var category = new CategoryEntity() { Id = 1, Name = "Category 1" };
        var categories = new List<CategoryEntity>
        {
            new CategoryEntity() { Id = 1, Name = "Category 1" },
            new CategoryEntity() { Id = 2, Name = "Category 2" }
        };


        //Act
        product.AddCategories(categories);
        product.AddCategory(category);

        //Assert
        Assert.Equal(2m, product.ProductCategories.Count(), 0);
    }
    [Fact]
    public void Product_AddCategoriesAlreadyAdded_ShouldAdd()
    {
        //Arrange
        var product = new ProductEntity("Valid Product Name", 123.4m);

        var categories = new List<CategoryEntity>
        {
            new CategoryEntity() { Id = 1, Name = "Category 1" },
            new CategoryEntity() { Id = 2, Name = "Category 2" }
        };
        var categories2 = new List<CategoryEntity>
        {
            new CategoryEntity() { Id = 1, Name = "Category 1" },
            new CategoryEntity() { Id = 3, Name = "Category 3" }
        };

        //Act
        product.AddCategories(categories);
        product.AddCategories(categories2);

        //Assert
        Assert.Equal(3m, product.ProductCategories.Count(), 0);
    }
}
