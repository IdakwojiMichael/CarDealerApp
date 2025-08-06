using CarDealerApp.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

//  Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  Register CSV repositories
builder.Services.AddSingleton<ICarRepository, CsvCarRepository>();
builder.Services.AddSingleton<IInquiryRepository, CsvInquiryRepository>();
builder.Services.AddSingleton<CsvCarRepository>();
builder.Services.AddSingleton<CsvInquiryRepository>();

//  Add session support
builder.Services.AddDistributedMemoryCache(); // Required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

//  Enable Swagger in ALL environments (Azure needs it too)
app.UseSwagger();
app.UseSwaggerUI();

//  Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//  Use session
app.UseSession();

//  Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();














//using CarDealerApp.Data.Repositories;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();
//builder.Services.AddHttpContextAccessor();

////// Register CSV repositories
//builder.Services.AddSingleton<ICarRepository, CsvCarRepository>();
//builder.Services.AddSingleton<IInquiryRepository, CsvInquiryRepository>();


//builder.Services.AddSingleton<CsvCarRepository>();
//builder.Services.AddSingleton<CsvInquiryRepository>();



////  Add session support
//builder.Services.AddDistributedMemoryCache(); // Required for session
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//// ? Use session in middleware
//app.UseSession();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
