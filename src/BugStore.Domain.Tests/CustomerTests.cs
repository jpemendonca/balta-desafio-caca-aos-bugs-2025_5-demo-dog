using BugStore.Domain.Entities;

namespace BugStore.Domain.Tests;

public class CustomerTests
{
    [Fact]
    public void ShouldCalculateAgeCorrectly_WhenBirthdayHasPassed()
    {
        // Arrange
        var customer = new Customer
        {
            BirthDate = DateTime.Today.AddYears(-20).AddDays(-1)
        };

        // Act
        var age = customer.Age;

        // Assert
        Assert.Equal(20, age);
    }
    
    [Fact]
    public void ShouldCalculateAgeCorrectly_WhenBirthdayHasNotPassed()
    {
        // Arrange
        var customer = new Customer
        {
            BirthDate = DateTime.Today.AddYears(-20).AddDays(1)
        };

        // Act
        var age = customer.Age;

        // Assert
        Assert.Equal(19, age);
    }
}