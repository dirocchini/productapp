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

}
