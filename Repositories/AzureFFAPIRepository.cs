using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Interfaces;
using FFModels;
using Repositories;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using Newtonsoft.Json;
using System.Text.Json;


namespace Repositories
{
    public class AzureFFAPIRepository :IRepository
    {
        private List<string> flagKeys;
        public AzureFFAPIRepository()
        {
            //to do--get Flag Keys from config
            flagKeys = new List<string>();
            flagKeys.Add(".appconfig.featureflag/Flag1");
            flagKeys.Add(".appconfig.featureflag/Flag2");
            flagKeys.Add(".appconfig.featureflag/Flag3");
        }

        public async Task<FeatureFlagNameCollection>  GetActiveFeatureFlagNames()
        {
            var flagNames = new FeatureFlagNameCollection();
            flagNames.FeatureFlagNames = new List<FFReturn>();
            for (int i = 0; i < flagKeys.Count(); i++)
            {
                var responseModel = await GetFeatureFlag(flagKeys[i]);

                if (responseModel.value.Contains("true"))
                {
                    var item = new FFReturn();
                    item.IsEnabled = true;
                    string[] ID = flagKeys[i].Split('/');
                    item.FeatureFlagName = ID[1];
                    flagNames.FeatureFlagNames.Add(item);
                }
            }

            return flagNames;
        }

        public Task<KeyResponseModelCollection> GetAllFeatureFlags()
        {
            throw new NotImplementedException();
        }

        

        private async Task<KeyResponseModel> GetFeatureFlag(string keyName)
        {

            var client = new HttpClient();

            HelperFunctions helper = new HelperFunctions();
            string token = await helper.getTokenAsync(); //todo--get token only once in loops
            //POST https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.AppConfiguration/configurationStores/{configStoreName}/listKeyValue?api-version=2019-10-01
            string subscriptionId = "339ad297-755e-4f1e-9c41-9f445ddb85b2";
            string resourceGroupName = "TonkonFeature";
            string configStoreName = "TonkonAppConfig";
            string route = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.AppConfiguration/configurationStores/{configStoreName}/listKeyValue?api-version=2019-10-01";
            KeyModel keyModel = new KeyModel();
            keyModel.key = keyName;
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage message = await client.PostAsync(route, new StringContent(JsonConvert.SerializeObject(keyModel), Encoding.UTF8, "application/json"));
            var keyResponse = await System.Text.Json.JsonSerializer.DeserializeAsync<KeyResponseModel>(await message.Content.ReadAsStreamAsync());
            return keyResponse;
        }
    }
}
