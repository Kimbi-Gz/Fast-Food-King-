using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FastFoodKing.Data;
using FastFoodKing.Configuration;
using FastFoodKing.Commands;
using FastFoodKing;
using FastFoodKing.CommandHandler;
using FastFoodKing.DTOs;
using FastFoodKing.Models;
using FastFoodKing.QueryHandler;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FastFoodKingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FastFoodKingContext") ?? throw new InvalidOperationException("Connection string 'FastFoodKingContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICommandHandler<CategoryDTO>, AddCategoryCommandHandler>();
builder.Services.AddScoped<ICommandHandler<RemoveByIdCommand>, RemoveCategoryCommandHandler>();
builder.Services.AddScoped<IQueryHandler<Category, QueryByIdCommand>, CategoryQueryHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
