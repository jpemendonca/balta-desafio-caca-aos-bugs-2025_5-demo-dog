using BugStore.Application.Handlers.Order;
using BugStore.Application.Requests.Orders;
using Microsoft.AspNetCore.Mvc;

namespace BugStore.Api.Endpoints.Order;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/v1/orders");

        group.WithTags("Orders");

        group.MapGet("/", async (
                [FromServices] IOrderHandle handler) =>
            await handler.GetAllAsync());

        group.MapGet("/{id}", async (
                [FromRoute] Guid id,
                [FromServices] IOrderHandle handler) =>
            await handler.GetByIdAsync(id));

        group.MapPost("/", async (
                [FromBody] Create request,
                [FromServices] IOrderHandle handler, 
                CancellationToken cancellationToken) =>
            await handler.CreateAsync(request, cancellationToken));

        group.MapDelete("/{id}", async (
                [FromRoute] Guid id,
                [FromServices] IOrderHandle handler,
                CancellationToken cancellationToken) =>
            await handler.DeleteAsync(id, cancellationToken));
    }
}