using CoreCashApi.Data;
using CoreCashApi.Services;
using CoreCashApi.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    // use Microsoft Sql Server
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#region AddScoped Utility
builder.Services.AddScoped<ImageUtility>();
#endregion

#region AddScoped Services
builder.Services.AddScoped<AuthService>();
// builder.Services.Add
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
