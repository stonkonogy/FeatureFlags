using System;
using System.Collections.Generic;
using System.Text;
using FFModels;
using Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace Repositories
{
    public class AzureFFRepository : IRepository
    {
        public async System.Threading.Tasks.Task<FeatureFlagNameCollection> GetActiveFeatureFlagNames()
        {
            var client = new HttpClient();
            var route = "http://localhost:35151/api/FeatureFlags/GetEnabledFlags";
            var result = await client.GetAsync(route);
            var keyResponse = await System.Text.Json.JsonSerializer.DeserializeAsync<FeatureFlagNameCollection>(await result.Content.ReadAsStreamAsync());

            var flags = new FeatureFlagNameCollection();
            flags.FeatureFlagNames = new List<FFReturn>();

            var flag1 = new FFReturn();
            flag1.FeatureFlagName = "Flag1";
            var flag2 = new FFReturn();
            flag2.FeatureFlagName = "Flag2";
            var flag3 = new FFReturn();
            flag3.FeatureFlagName = "Flag3";

            flags.FeatureFlagNames.Add(flag1);
            flags.FeatureFlagNames.Add(flag2);
            flags.FeatureFlagNames.Add(flag3);

            return flags;
        }

        public KeyResponseModelCollection GetAllFeatureFlags()
        {
            throw new NotImplementedException();
        }

        Task<KeyResponseModelCollection> IRepository.GetAllFeatureFlags()
        {
            throw new NotImplementedException();
        }
    }
}
