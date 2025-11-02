using BugStore.Api.Endpoints.Customer;
using BugStore.Api.Endpoints.Order;
using BugStore.Api.Endpoints.Product;
using BugStore.Application.Handlers.Customers;
using BugStore.Application.Handlers.Order;
using BugStore.Application.Handlers.Product;
using BugStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite(connectionString));


builder.Services.AddScoped<BugStore.Application.Interfaces.IAppDbContext, 
    BugStore.Infrastructure.Data.AppDbContext>();

builder.Services.AddScoped<ICustomerHandler, BugStore.Application.Handlers.Customers.Handler>();
builder.Services.AddScoped<IOrderHandle, BugStore.Application.Handlers.Order.Handler>();
builder.Services.AddScoped<IProductHandle, BugStore.Application.Handlers.Product.Handler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapCustomerEndpoints();
app.MapOrderEndpoints();
app.MapProductEndpoints();

app.Run();
