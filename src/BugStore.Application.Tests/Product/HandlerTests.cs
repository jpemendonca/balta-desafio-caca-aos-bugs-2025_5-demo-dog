using BugStore.Application.Handlers.Product;
using BugStore.Application.Requests.Products;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BugStore.Application.Tests.Product;

public class HandlerTests : BaseHandlerTest
{
    private readonly IProductHandle _handler;

    public HandlerTests()
    {
        _handler = new BugStore.Application.Handlers.Product.Handler(_context);
    }
    
    [Fact]
    public async Task CreateAsync_WhenRequestIsValid_ShouldReturnCreatedAndSaveProduct()
    {
        // Arrange
        var request = new Create()
        {
            Title = "Novo Produto",
            Description = "Descricao",
            Price = 19.99m
        };
    
        // Act
        var result = await _handler.CreateAsync(request, CancellationToken.None);
    
        // Assert
        var createdResult = Assert.IsType<Created<BugStore.Domain.Entities.Product>>(result);
        
        Assert.NotNull(createdResult);
        Assert.Equal(request.Title, createdResult.Value.Title);
        Assert.Equal("novo-produto", createdResult.Value.Slug);
    
        // 2. Verifica o banco de dados
        var productInDb = await _context.Products.FindAsync(createdResult.Value.Id);
        Assert.NotNull(productInDb);
        Assert.Equal(request.Title, productInDb.Title);
    }

    [Fact]
    public async Task GetByIdAsync_WhenProductDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await _handler.GetByIdAsync(nonExistentId);

        // Assert
        // Verifica se o IResult é do tipo "NotFound"
        Assert.IsType<NotFound>(result);
    }
    
}