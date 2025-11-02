using BugStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BugStore.Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Order> Orders { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DatabaseFacade Database { get; }
}