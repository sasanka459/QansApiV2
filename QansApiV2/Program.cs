using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using QansApiV2.CustomMiddleware;
using QansApiV2.Infra;
using QansBAL.Abstraction;
using QansBAL.Services;
using QansDAL.Abstraction;
using QansDAL.Entities;
using QansDAL.Services;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IUserRepo,UserRepo>();
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Replace with your React app's URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
#region Keyvault

var keyVaultname = builder.Configuration["KeyVault:Name"];

var keyVaultUri = new Uri($"https://{keyVaultname}.vault.azure.net/");
var client = new SecretClient(vaultUri: keyVaultUri, credential: new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeEnvironmentCredential = true }));

//Read User Name and password from Keyvault
var sqlUserName = client.GetSecret("qansSqlUserName").Value;
var sqlPassword = client.GetSecret("qansSqlPassword").Value;
#endregion



var sqlConnection=String.Format(builder.Configuration.GetConnectionString("connectionsString"), sqlUserName.Value, sqlPassword.Value );

builder.Services.AddDbContext<QansDbContext>(Option =>
 Option.UseSqlServer(sqlConnection));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Configure Swagger to use OAuth2
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{builder.Configuration["AzureAd:Instance"]}{builder.Configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize"),
                TokenUrl = new Uri($"{builder.Configuration["AzureAd:Instance"]}{builder.Configuration["AzureAd:TenantId"]}/oauth2/v2.0/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "api://fd32341b-7f1b-437f-8a65-3e8d609ca291/Readwrite", "Access the API" }
                }
            }
        }
    });

    //c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    //{
    //    Type = SecuritySchemeType.OAuth2,
    //    Flows = new OpenApiOAuthFlows()
    //    {
    //        Implicit = new OpenApiOAuthFlow()
    //        {
    //            AuthorizationUrl = new Uri($"https://login.microsoftonline.com/{builder.Configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize"),
    //            TokenUrl = new Uri($"https://login.microsoftonline.com/{builder.Configuration["AzureAd:TenantId"]}/oauth2/v2.0/token"),
    //            Scopes = new Dictionary<string, string> {
    //                 { "api://fd32341b-7f1b-437f-8a65-3e8d609ca291/Readwrite", "Access the API" }
    //                }
    //        }
    //    }
    //});





    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            //new List < string > ()
            new[] { "api://fd32341b-7f1b-437f-8a65-3e8d609ca291/.default" }
        }

    //      {
    //    new OpenApiSecurityScheme {
    //        Reference = new OpenApiReference {
    //                Type = ReferenceType.SecurityScheme,
    //                    Id = "oauth2"
    //            },
    //            Scheme = "oauth2",
    //            Name = "oauth2",
    //            In = ParameterLocation.Header
    //    },
    //    new List < string > ()
    //}

    });


});


var app = builder.Build();
//app.UseMiddleware<ExceptionHandlingMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.OAuthClientId(builder.Configuration["AzureAd:ClientId"]);
        //c.OAuthClientSecret(builder.Configuration["AzureAd:ClientSecret"]);
        c.OAuthUsePkce();
        c.OAuthScopeSeparator(" ");
    });

    //app.UseSwaggerUI(c => {
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AzureAD_OAuth_API v1");
    //    //c.RoutePrefix = string.Empty;    
    //    c.OAuthClientId(builder.Configuration["AzureAd:ClientId"]);
    //    c.OAuthClientSecret(builder.Configuration["AzureAd:ClientSecret"]);
    //    c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    //});
}

// Use CORS policy
app.UseCors("AllowReactApp");

app.UseMiddleware<Middleware1>();

app.UseMiddleware<Middleware2>();

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
