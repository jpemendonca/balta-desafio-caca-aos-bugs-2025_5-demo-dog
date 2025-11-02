using BugStore.Application.Requests.Customers;
using Microsoft.AspNetCore.Http;

namespace BugStore.Application.Handlers.Customers;

public interface ICustomerHandler
{
    Task<IResult> GetAllAsync();
    Task<IResult> GetByIdAsync(Guid id);
    Task<IResult> CreateAsync(Create request, CancellationToken cancellationToken); 
    Task<IResult> UpdateAsync(Guid id, Update request, CancellationToken cancellationToken);
    Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken);
}