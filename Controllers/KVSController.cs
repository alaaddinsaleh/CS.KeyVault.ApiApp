using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CS.KeyVault.ApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KVSController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public  KVSController(IConfiguration configuration)
        {
             this.configuration = configuration;
        }

        [HttpGet("keyvault")]
        public string GetSecret()
        {
            return configuration["SecretName"];
        }
    }
}
