using BugStore.Application.Handlers.Customers;
using BugStore.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Application.Tests.Customer;

public class HandlerTests : BaseHandlerTest
{
    private readonly ICustomerHandler _handler;

    public HandlerTests()
    {
        _handler = new BugStore.Application.Handlers.Customers.Handler(_context);
    }

    [Fact]
    public async Task GetByIdAsync_WhenCustomerExists_ShouldReturnOkWithCustomer()
    {
        // Arrange
        var customer = new Domain.Entities.Customer
        {
            Id = Guid.NewGuid(),
            Name = "Cliente Teste",
            Email = "teste@email.com",
            Phone = "9899999999"
        };

        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        // Act
        var result = await _handler.GetByIdAsync(customer.Id);

        // Assert
        var okResult = Assert.IsType<Ok<BugStore.Application.Responses.Customers.GetById>>(result);

        Assert.NotNull(okResult.Value);
        Assert.Equal(customer.Name, okResult.Value.Name);
        Assert.Equal(customer.Email, okResult.Value.Email);
    }

    [Fact]
    public async Task GetByIdAsync_WhenCustomerDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await _handler.GetByIdAsync(nonExistentId);

        // Assert
        Assert.IsType<NotFound>(result);
    }

    [Fact]
    public async Task CreateAsync_WhenRequestIsValid_ShouldReturnCreatedAndSaveCustomer()
    {
        // Arrange
        var request = new BugStore.Application.Requests.Customers.Create
        {
            Name = "Novo Cliente",
            Email = "novo@email.com",
            Phone = "123456789"
        };

        // Act
        var result = await _handler.CreateAsync(request, CancellationToken.None);

        // Assert
        var createdResult = Assert.IsType<Created<Domain.Entities.Customer>>(result);

        Assert.NotNull(createdResult.Value);
        Assert.Equal(request.Name, createdResult.Value.Name);

        // 2. Verifica o banco de dados
        var customerInDb = await _context.Customers.FindAsync(createdResult.Value.Id);
        Assert.NotNull(customerInDb);
        Assert.Equal(request.Name, customerInDb.Name);
    }

    [Fact]
    public async Task UpdateAsync_WhenCustomerExists_ShouldReturnNoContentAndUpdateCustomer()
    {
        // Arrange

        // cria o customer
        var customer = new Domain.Entities.Customer
            { Id = Guid.NewGuid(), Name = "Nome Antigo", Email = "antigo@email.com", Phone = "181181818" };
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        var request = new BugStore.Application.Requests.Customers.Update
        {
            Name = "Nome Novo",
            Email = "novo@email.com",
        };

        // Act
        var result = await _handler.UpdateAsync(customer.Id, request, CancellationToken.None);

        // Assert
        Assert.IsType<NoContent>(result);

        // 2. Verifica o banco de dados
        var updatedCustomer = await _context.Customers.FindAsync(customer.Id);
        Assert.NotNull(updatedCustomer);
        Assert.Equal(request.Name, updatedCustomer.Name);
    }

    [Fact]
    public async Task UpdateAsync_WhenCustomerDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var request = new BugStore.Application.Requests.Customers.Update()
        {
            Name = "Novo nome",
            Email = "Novo email"
        };

        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await _handler.UpdateAsync(nonExistentId, request, CancellationToken.None);

        // Assert
        Assert.IsType<NotFound>(result);
    }
}