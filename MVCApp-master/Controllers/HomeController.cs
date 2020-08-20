using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCApp.Models;
using Newtonsoft.Json;
using System.Text.Json;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Home/GetFeatureFlags")]
        public async Task<KeyResponseModel> GetFeatureFlag(string keyName)
        {
     
            HttpClient client = new HttpClient();
            HelperFunctions helper = new HelperFunctions();
            string token = await helper.getTokenAsync();
            //POST https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.AppConfiguration/configurationStores/{configStoreName}/listKeyValue?api-version=2019-10-01
            string subscriptionId = "{Subscription ID}";
            string resourceGroupName = "{ResourceGroupName}";
            string configStoreName = "{AppConfigName}";
            string route = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.AppConfiguration/configurationStores/{configStoreName}/listKeyValue?api-version=2019-10-01";
            KeyModel keyModel = new KeyModel();
            keyModel.key = keyName;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage message = await client.PostAsync(route, new StringContent(JsonConvert.SerializeObject(keyModel),Encoding.UTF8, "application/json"));
            var keyResponse = await System.Text.Json.JsonSerializer.DeserializeAsync<KeyResponseModel>(await message.Content.ReadAsStreamAsync());

            return keyResponse;  

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
