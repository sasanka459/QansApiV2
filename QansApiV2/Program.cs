using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
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

#region Keyvault

var keyVaultname = builder.Configuration["KeyVault:Name"];

var keyVaultUri = new Uri($"https://{keyVaultname}.vault.azure.net/");
var client = new SecretClient(vaultUri: keyVaultUri, credential: new DefaultAzureCredential());

//Read User Name and password from Keyvault
var sqlUserName = client.GetSecret("qansSqlUserName").Value;
var sqlPassword = client.GetSecret("qansSqlPassword").Value;
#endregion



var sqlConnection=String.Format(builder.Configuration.GetConnectionString("connectionsString"), sqlUserName.Value, sqlPassword.Value );

builder.Services.AddDbContext<QansDbContext>(Option =>
 Option.UseSqlServer(sqlConnection));
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
