using Application.Products.Commands.DeleteProduct;
using Domain.Entity.Database;
using Domain.Entity.Repositories;
using Moq;
using System.Net;

namespace UnitTests.Application.Product;

public class DeleteProductCommandTests
{
    private readonly Mock<IProductRepository> _productRepository;

    public DeleteProductCommandTests()
    {
        _productRepository = new Mock<IProductRepository>();
    }

    [Fact]
    public async Task DeleteProduct_ExistingProduct_ShouldDelete()
    {
        //Arrange
        var deleteCommand = new DeleteProductCommand(1);

        var product = new ProductEntity("new product", 123.4m);
        _productRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(product);
        _productRepository.Setup(x => x.RemoveAsync(It.IsAny<ProductEntity>())).Returns(Task.CompletedTask);
        _productRepository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new DeleteProductCommandHandler(_productRepository.Object);

        //Act
        var result = await handler.Handle(deleteCommand, CancellationToken.None);

        //Assert
        Assert.True(result.Succeeded);
    }
    [Fact]
    public async Task DeleteProduct_ProductNotFound_ShouldReturnNotFound()
    {
        //Arrange
        var deleteCommand = new DeleteProductCommand(1);

        ProductEntity product = null;
        _productRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(product);

        var handler = new DeleteProductCommandHandler(_productRepository.Object);

        //Act
        var result = await handler.Handle(deleteCommand, CancellationToken.None);

        //Assert
        Assert.True(result.Succeeded == false);
        Assert.True(result.Error.Code == (int)HttpStatusCode.NotFound);
    }
}
