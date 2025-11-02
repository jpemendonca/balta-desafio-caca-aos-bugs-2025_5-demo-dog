using BugStore.Application.Requests.Orders;
using Microsoft.AspNetCore.Http;

namespace BugStore.Application.Handlers.Order;

public interface IOrderHandle
{
    Task<IResult> GetAllAsync();
    Task<IResult> GetByIdAsync(Guid id);
    Task<IResult> CreateAsync(Create request, CancellationToken cancellationToken);  
    Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken);
}