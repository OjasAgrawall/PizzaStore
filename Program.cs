using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Application.Interfaces;
using PizzaStore.Application.Services;
using PizzaStore.Infrastructure.Data;
using PizzaStore.Infrastructure.Interfaces;
using PizzaStore.Infrastructure.ModelBusinessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Get connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DBcontext
builder.Services.AddDbContext<PizzaContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();
builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.Configure<RazorViewEngineOptions>(o =>
{
    o.ViewLocationFormats.Clear();
    o.ViewLocationFormats.Add("/Presentation/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    o.ViewLocationFormats.Add("/Presentation/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

  

app.Run();
