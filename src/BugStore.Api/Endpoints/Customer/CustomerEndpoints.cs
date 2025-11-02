using BugStore.Application.Handlers.Customers;
using BugStore.Application.Requests.Customers;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Api.Endpoints.Customer;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/v1/customers");
        group.WithTags("Customers");
        
        group.MapGet("/", async (
                [FromServices] ICustomerHandler handler) => 
            await handler.GetAllAsync());

        group.MapGet("/{id}", async (
                [FromRoute] Guid id, 
                [FromServices] ICustomerHandler handler) => 
            await handler.GetByIdAsync(id));
        
        group.MapPost("/", async (
                [FromBody] Create request, 
                [FromServices] ICustomerHandler handler,
                CancellationToken cancellationToken) => 
            await handler.CreateAsync(request, cancellationToken));
        
        group.MapPut("/{id}", async (
                [FromRoute] Guid id, 
                [FromBody] Update request, 
                [FromServices] ICustomerHandler handler,
                CancellationToken cancellationToken) => 
            await handler.UpdateAsync(id, request, cancellationToken));
        
        group.MapDelete("/{id}", async (
                [FromRoute] Guid id, 
                [FromServices] ICustomerHandler handler,
                CancellationToken cancellationToken) => 
            await handler.DeleteAsync(id, cancellationToken));
    }
}