using EmployeeAdminPortal.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("blazorApp", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:5000");
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowCredentials();
    });

    options.AddPolicy("AngularApp", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200");
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowCredentials();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200") // Angular origin
                     .AllowAnyHeader()
                     .AllowAnyMethod();
                     
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();


app.UseCors("AngularApp");

app.MapControllers();

app.Run();
