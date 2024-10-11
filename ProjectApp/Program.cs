using Microsoft.EntityFrameworkCore;
using ProjectApp.Core;
using ProjectApp.Core.Interfaces;
using ProjectApp.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency injection
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectPersistence, MySqlProjectPersistence>();

// Add database context
builder.Services.AddDbContext<ProjectDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString(
            "ProjectDbConnection")));

// Auto mapper
builder.Services.AddAutoMapper(typeof(Program));

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