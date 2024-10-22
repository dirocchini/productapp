using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Domain.Entity.Database;
using System.Text.Json;

namespace IntegrationTests.Products;


public class ProductApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateProduct_ShouldReturnOk()
    {
        // Arrange
        var newProduct = new
        {
            Name = "Integration Tests Product " + new Random().Next(0, 1500),
            Value = 29.99m
        };
        var content = new StringContent(JsonConvert.SerializeObject(newProduct), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/products", content);

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        var responseString = await response.Content.ReadAsStringAsync();
    }
    [Fact]
    public async Task GetAllProducts_ShouldReturnAllProducts()
    {
        // Arrange


        // Act
        var response = await _client.GetAsync("/api/products");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

    }
    [Fact]
    public async Task DeleteProduct_ShouldDeleteAProduct()
    {
        //Arrange
        var response = await _client.GetAsync("/api/products");

        JsonDocument jsonDoc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        JsonElement data = jsonDoc.RootElement.GetProperty("data");

        var products = JsonConvert.DeserializeObject<List<ProductEntity>>(data.ToString());

        //Act + Arrange
        foreach (var product in products)
        {
            if (product.Name.Contains("Integration Tests Product"))
            {
                var deleteResponse = await _client.DeleteAsync("/api/products/" + product.Id);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
