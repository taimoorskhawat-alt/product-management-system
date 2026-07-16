using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using prodAPIPrac2.data;
using prodAPIPrac2.interfaces;
using prodAPIPrac2.repositary;
using prodAPIPrac2.Services;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using prodAPIPrac2.Models;
using prodAPIPrac2.services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Product API",
            Version = "v1"
        });

    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type = SecuritySchemeType.Http,

            Scheme = "bearer",

            BearerFormat = "JWT",

            In = ParameterLocation.Header,

            Description = "Enter JWT Token"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },

                Array.Empty<string>()
            }
        });
});

builder.Services.AddDbContext<appdbcontext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProduct, Prodrepo>();
builder.Services.AddScoped<Iprodservice, ProductService>();
builder.Services.AddScoped<IuserService, UserService>();
builder.Services.AddScoped<IOrder, OrderRepo>();
builder.Services.AddScoped<IorderService, OrderService>();
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,

        ValidateAudience = true,

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"]!
            ))
    };
});


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<appdbcontext>();

    if (!context.users.Any(x => x.Email == "admin@gmail.com"))
    {
        var hasher = new PasswordHasher<User>();

        var admin = new User
        {
            Name = "Admin",
            Email = "admin@gmail.com",
            Role = "Admin"
        };

        admin.PasswordHash =
            hasher.HashPassword(admin, "Admin@123");

        context.users.Add(admin);

        context.SaveChanges();
    }
}
app.UseCors("AllowAngularApp"); 
// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.UseStaticFiles();


app.MapControllers();

app.Run();