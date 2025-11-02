using BugStore.Domain.Entities;

namespace BugStore.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void ShouldGenerateSlug_WhenTitleIsSet()
    {
        // Arrange
        var product = new Product();
        
        // Act
        product.Title = "Meu Novo Produto de Teste";

        // Assert
        Assert.Equal("meu-novo-produto-de-teste", product.Slug);
    }

    [Theory]
    [InlineData("Produto com Acentos é Çomplicado", "produto-com-acentos-e-complicado")]
    [InlineData("Remove !@#$ Caracteres Especiais", "remove-caracteres-especiais")]
    [InlineData("  Espaços   Extras  ", "espacos-extras")]
    public void ShouldGenerateSlugCorrectly(string title, string expectedSlug)
    {
        // Arrange
        var product = new Product();

        // Act
        product.Title = title;
        
        // Assert
        Assert.Equal(expectedSlug, product.Slug);
    }
}