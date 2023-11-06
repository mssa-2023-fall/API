using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace GlizzyGlobal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IPInfoController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetIPInfo()
        {
            string response;
            using (var client = new HttpClient())
            {
                response = await client.GetStringAsync("https://ipinfo.io");
            }

            var json = JsonDocument.Parse(response);

            var ip = json.RootElement.GetProperty("ip").GetString();
            var city = json.RootElement.GetProperty("city").GetString();
            var region = json.RootElement.GetProperty("region").GetString();

            return Ok($"I know where you live..{city}, {region} \n{ip}");
        }
    }
}
