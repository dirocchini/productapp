using Application.Common.Models;
using Application.Events;
using Application.Products.Commands.CreateProduct;
using Domain.Entity.Database;
using Domain.Entity.Repositories;
using Domain.Interfaces.Listeners;
using Moq;

namespace UnitTests.Application;

public class CreateProductCommandTests
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly Mock<ICategoryRepository> _categoryRepository;
    private readonly Mock<IProductEventListener> _productEventListener;

    public CreateProductCommandTests()
    {
        _productRepository = new Mock<IProductRepository>();
        _productEventListener = new Mock<IProductEventListener>();
        _categoryRepository = new Mock<ICategoryRepository>();
    }
    [Fact]

    public async Task Handle_GivenValidProduct_ShouldCreateProduct()
    {
        //Arrange
        var command = new CreateProductCommand("product name", 123, new List<string>());

        _productRepository.Setup(x => x.AddAsync(It.IsAny<ProductEntity>())).Returns(Task.CompletedTask);
        _productRepository.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new CreateProductCommandHandler(_productRepository.Object, _categoryRepository.Object, new ProductEventPublisher(_productEventListener.Object));

        //Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<ServiceResult<String>>(result);
        Assert.True(result.Succeeded);
    }
}
