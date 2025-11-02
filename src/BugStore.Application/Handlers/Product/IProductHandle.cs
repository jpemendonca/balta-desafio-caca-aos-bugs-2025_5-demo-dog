using BugStore.Application.Requests.Products;
using Microsoft.AspNetCore.Http;

namespace BugStore.Application.Handlers.Product;

public interface IProductHandle
{
    Task<IResult> GetAllAsync();
    Task<IResult> GetByIdAsync(Guid id);
    Task<IResult> CreateAsync(Create request, CancellationToken cancellationToken); 
    Task<IResult> UpdateAsync(Guid id, Update request, CancellationToken cancellationToken);
    Task<IResult> DeleteAsync(Guid id, CancellationToken cancellationToken);
}