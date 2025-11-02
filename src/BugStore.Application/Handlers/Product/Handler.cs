using System.Text.RegularExpressions;
using BugStore.Application.Interfaces;
using BugStore.Application.Requests.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Application.Handlers.Product;

public class Handler(IAppDbContext context) : IProductHandle
{
    public async Task<IResult> GetAllAsync()
    {
        var products = await context.Products
            .Select(p => new Responses.Products.Get
            {
                Id = p.Id,
                Title = p.Title,
                Slug = p.Slug,
                Price = p.Price
            })
            .AsNoTracking()
            .ToListAsync();
        
        return Results.Ok(products);
    }

    public async Task<IResult> GetByIdAsync(Guid id)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null)
            return Results.NotFound();

        var response = new Responses.Products.GetById
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Slug = product.Slug,
            Price = product.Price
        };
        
        return Results.Ok(response);
    }

    public async Task<IResult> CreateAsync(Create request, CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product()
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            // Slug = GenerateSlug(request.Title)
        };

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync(cancellationToken);

        return Results.Created($"/v1/products/{product.Id}", product);
    }

    public async Task<IResult> UpdateAsync(Guid id, Update request, CancellationToken cancellationToken)
    {
        var product = await context.Products.FindAsync(id);
        
        if (product is null)
            return Results.NotFound();

        product.Title = request.Title;
        product.Description = request.Description;
        product.Price = request.Price;
        // product.Slug = GenerateSlug(request.Title);

        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);
        
        return Results.NoContent();
    }

    public async Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await context.Products.FindAsync(id);
        if (product is null)
            return Results.NotFound();

        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
    
    // private static string GenerateSlug(string text)
    // {
    //     var slug = text.ToLowerInvariant();
    //     slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
    //     slug = Regex.Replace(slug, @"\s+", " ").Trim();
    //     slug = Regex.Replace(slug, @"\s", "-");
    //     return slug;
    // }
}