using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

namespace CS.KeyVault.ApiApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Access key vault using Managed Identity

        var keyVaultUrl = new Uri($"https://{builder.Configuration["KeyVault:Name"]}.vault.azure.net/");

        builder.Configuration.AddAzureKeyVault(keyVaultUrl, new DefaultAzureCredential(),
            new AzureKeyVaultConfigurationOptions
            {
                ReloadInterval = TimeSpan.FromMinutes(5)
            });

        // Access key vault using credential

        //var Credentials = new ClientSecretCredential(builder.Configuration["KeyVault:TenantId"],
        //    builder.Configuration["KeyVault:ClientId"],
        //    builder.Configuration["KeyVault:ClientSecret"]);

        //var secretClient = new SecretClient(keyVaultUrl, Credentials);
        //KeyVaultSecret kvs = secretClient.GetSecret("SecretName");

        //var secretValue = kvs.Value;

        //builder.Configuration.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions());


        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
