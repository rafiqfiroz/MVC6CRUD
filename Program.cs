using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVC6CRUD.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MVC6CrudConnetionString")).UseLoggerFactory(LoggerFactory.Create(builder=>builder.AddConsole())));
////var logFilePath = "E:\\Visual Studio\\Learning_Project\\MVC6CRUD\\Logs\\log.txt"; // Remove the extra slash before "log.txt"

////builder.Services.AddDbContext<ApplicationDbContext>(opt => opt
////    .UseSqlServer(builder.Configuration.GetConnectionString("MVC6CrudConnetionString"))
////    .UseLoggerFactory(LoggerFactory.Create(builder =>
////    {
////        builder.AddSerilog(new LoggerConfiguration()
////            .WriteTo.Console()
////            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
////            .CreateLogger());
////    })));

//builder.Services.AddDbContext<ApplicationDbContext>(opt => opt
//    .UseSqlServer(builder.Configuration.GetConnectionString("MVC6CrudConnetionString"))
//    .UseLoggerFactory(LoggerFactory.Create(builder =>
//    {
//        builder.AddSerilog(new LoggerConfiguration()
//            .WriteTo.Console()
//            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
//            .CreateLogger());
//    })));

//builder.Host.ConfigureLogging(logging =>
//{
//    logging.ClearProviders();
//    logging.AddSerilog(); // Add Serilog here
//});
//var logFilePath = "E:\\Visual Studio\\Learning_Project\\MVC6CRUD\\Logs\\log.txt";

//builder.Services.AddDbContext<ApplicationDbContext>(opt => opt
//    .UseSqlServer(builder.Configuration.GetConnectionString("MVC6CrudConnetionString"))
//    .UseLoggerFactory(LoggerFactory.Create(builder =>
//    {
//        builder.AddSerilog(new LoggerConfiguration()
//            .WriteTo.Console()
//            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
//            .CreateLogger());
//    })));
var logFilePath = "E:\\Visual Studio\\Learning_Project\\MVC6CRUD\\Logs\\log.txt";

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt
    .UseSqlServer(builder.Configuration.GetConnectionString("MVC6CrudConnetionString"))
    .UseLoggerFactory(LoggerFactory.Create(builder =>
    {
        builder.AddSerilog(new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
            .CreateLogger());
    })));


builder.Services.AddControllersWithViews();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
