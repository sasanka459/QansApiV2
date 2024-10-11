using Microsoft.EntityFrameworkCore;
using QansBAL.Abstraction;
using QansBAL.Services;
using QansDAL.Abstraction;
using QansDAL.Entities;
using QansDAL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IUserRepo,UserRepo>();
builder.Services.AddDbContext<QansDbContext>(Option =>
 Option.UseSqlServer(builder.Configuration.GetConnectionString("connectionsString")));
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
