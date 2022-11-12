using NiloPharmacy.Data;
using Microsoft.EntityFrameworkCore;
using NiloPharmacy.Data.Services;
using NiloPharmacy.Data.Cart;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
;
builder.Services.AddDbContext<AppDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("Con")));
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

builder.Services.AddSession();
builder.Services.AddControllersWithViews();


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
app.UseSession();
app.UseAuthentication();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppDbInitializer.Seed(app);
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();

app.Run();
