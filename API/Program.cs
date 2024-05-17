using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using API.Data;
using API.Middleware;
using API.Models;
using API.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var sqlliteConnString = builder.Configuration.GetConnectionString("DefaultConnection");
var pgConnectionString = builder.Configuration.GetConnectionString("PostgresConnection");

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Type in your Auth Bearer Token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            jwtSecurityScheme, Array.Empty<string>()
        }
    });
});

//string connString;
//if (builder.Environment.IsDevelopment()) connString = pgConnectionString;
//else connString = builder.Configuration.GetConnectionString("DefaultConnection");
string connString = pgConnectionString;

builder.Services.AddDbContext<FoodieContext>(opt =>
{
    // opt.UseSqlite(sqlliteConnString);
    opt.UseNpgsql(connString);
});
builder.Services.AddCors();
builder.Services.AddIdentityCore<User>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
    .AddRoles<Role>()
    .AddEntityFrameworkStores<FoodieContext>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.
                GetBytes(builder.Configuration["JWT:TokenKey"])),
        };
        // opt.UseSecurityTokenValidators = true;
        opt.TokenHandlers.Add(new BearerTokenHandler());
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<JwtTokenGenerator>();
// builder.Services.AddTransient<AuthMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.ConfigObject.AdditionalItems.Add("persistAuthorization", "false");
    });
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// app.MapWhen(context => context.Request.Path.StartsWithSegments("/api/v1/bookmarks"),
//     selectedRouteApp =>
//     {
//         selectedRouteApp.UseMiddleware<AuthMiddleware>();
//         selectedRouteApp.UseEndpoints(endpoints => endpoints.MapControllers());
//         // You can add more middleware or configure pipeline for selected routes here
//     }
// );

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<FoodieContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await DbInitializer.Initialize(context, userManager);
}
catch (Exception ex)
{
    logger.LogError(ex, "A problem occured");
}

app.Run();