using System.Text.Json.Serialization;
using API.Data;
using API.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opt => 
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FoodieContext>(opt =>
{
    opt.UseSqlite(connectionString);
});
// builder.Services.AddDefaultIdentity<IdentityUser>();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt => 
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});

app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<FoodieContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    context.Database.Migrate();
    DbInitializer.Initialize(context);
} 
catch (Exception ex)
{
    logger.LogError(ex, "A problem occured");
}

app.Run();
