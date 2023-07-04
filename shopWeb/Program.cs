using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using shopWeb.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


var testValue = builder.Configuration.GetValue<string>("TestValue");



var test2= builder.Configuration["RedisCacheOptions:Configuration"];

var test3 = builder.Configuration.GetSection("Settings").Get<Settings>();

var connectionString = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddIdentity<User, Role>(identityOptions =>{}).AddRoles<Role>()
           .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString); // for example if you're holding the connection string in the appsettings.json
});


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();







// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

builder.Services.AddKendo();

var app = builder.Build();

//app.UseKendo();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




//var connectionString = builder.Configuration.GetConnectionString("SqlServer");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//{
//    options.UseSqlServer(connectionString);
//});

//https://www.freecodespot.com/blog/asp-net-core-identity/
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
    name: "all",
    pattern: "{area}/{controller}/{action=Index}/{id?}"
  );

    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

   

 

});



app.Run();