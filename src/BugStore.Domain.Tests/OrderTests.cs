using BugStore.Domain.Entities;

namespace BugStore.Domain.Tests;

public class OrderTests
{
    [Fact]
    public void ShouldCalculateOrderTotalCorrectly()
    {
        // Arrange
        var order = new Order()
        {
            Lines =
            [
                new OrderLine { Total = 10.50m },
                new OrderLine { Total = 9.50m }
            ]
        };
        

        // Act
        var total = order.Total;

        // Assert
        // O total deve ser a soma
        Assert.Equal(20.00m, total);
    }

    [Fact]
    public void ShouldReturnZeroWhenOrderHasNoLines()
    {
        // Arrange
        var order = new Order();

        // Act
        var total = order.Total;

        // Assert
        Assert.Equal(0m, total);
    }
}