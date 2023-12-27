using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gara.Services
{
    public class GaraRestService : IRestService
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions jsonSerializationOptions;

        readonly Uri uri = new("http://localhost:5298/api/vehicle");
        public GaraRestService()
        {
            client = new HttpClient();
            jsonSerializationOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<string> TestApiConnection()
        {
            var response = await client.GetAsync(uri);
            Debug.Assert(response.IsSuccessStatusCode);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
