
using System.Diagnostics.Metrics;
using System.Net;
using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NuGet.Common;
using NuGet.Protocol.Plugins;
using Store.Entities.Models;
using Swashbuckle.AspNetCore.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt => {
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, 
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:5228",
        ValidAudience = "http://localhost:5228",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#@"))
    };
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    }); 

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


if (builder.Environment.IsProduction())
{
    var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL");
    var keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientId");
    var keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret");
    var keyVaultDirectoryID = builder.Configuration.GetSection("KeyVault:DirectoryID");

    // allows me to authenticate against the AzureAD
    var credential = new ClientSecretCredential(keyVaultDirectoryID.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString());

    builder.Configuration.AddAzureKeyVault(keyVaultURL.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString(), new DefaultKeyVaultSecretManager());

    var client = new SecretClient(new Uri(keyVaultURL.Value!.ToString()), credential);

    builder.Services.AddDbContext<StoreContext>(options =>
    {
        options.UseSqlServer(client.GetSecret("AzureConnSecret12").Value.Value.ToString());
    });

}


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<StoreContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}


var app = builder.Build();

// Configure Swagger UI and enable middleware
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreForWeb v1");
});


app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(
    endpoints => { endpoints.MapControllers();
    });


app.Run();







