using System.Configuration;
using Application.Catalogs.CatalogTypes;
using Application.Interfaces;
using Application.Interfaces.Contexts;
using Application.Visitors.GetTodyReport;
using Infrastructure.MappingProfile;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Context.MongoContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IGetTodayReportService, GetTodayReportService>();
builder.Services.AddTransient(typeof(IMongoDbContext<>), typeof(MongoDbContext<>));
builder.Services.AddTransient<ICatalogTypeService, CatalogTypeService>();
builder.Services.AddScoped<IDataBaseContext, DataBaseContext>();


//Mapper
builder.Services.AddAutoMapper(typeof(CatalogMappingProfile));

builder.Services.AddDbContext<DataBaseContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));



var app = builder.Build();
/*
IConfiguration configuration = app.Configuration;


#region connection
string connection = configuration["ConnectionStrings:SqlServer"];
#endregion
*/


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
