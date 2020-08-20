
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

    public class HelperFunctions
    {
        public async Task<string> getTokenAsync()
        {
        string tenantId = "72f988bf-86f1-41af-91ab-2d7cd011db47";//"{tenantId}";
        string clientId = "c5d4f2fd-2523-4150-9e43-9eb4c8959788"; //"{clientId}";
            string grantType = "client_credentials";
        string clientSecret = "dcXDcqnPayr.oxp4C6_1Ynmk1vJc5JjXGQ";//"{clientSecret}";
            string resource = "https://management.azure.com/";


            string path = "https://login.microsoftonline.com/" + tenantId + "/oauth2/token";

            var request = new HttpRequestMessage(HttpMethod.Post, path);

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("client_id", clientId));
            keyValues.Add(new KeyValuePair<string, string>("grant_type", grantType));
            keyValues.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
            keyValues.Add(new KeyValuePair<string, string>("resource", resource));

            request.Content = new FormUrlEncodedContent(keyValues);

            HttpClient client = new HttpClient();
            var response = await client.SendAsync(request);
            var token = await JsonSerializer.DeserializeAsync<AuthResponseToken>(await response.Content.ReadAsStreamAsync());


            return token.access_token;
        }
    }
    public class AuthResponseToken
    {
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string ext_expires_in { get; set; }
        public string expires_on { get; set; }
        public string not_before { get; set; }
        public string resource { get; set; }
        public string access_token { get; set; }

    }
