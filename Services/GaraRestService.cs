using Gara.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
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

        private readonly string port;

        private readonly Uri uri;

        private static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public GaraRestService()
        {
            if (Debugger.IsAttached)
            {
                port = "5298";
            }
            else
            {
                port = "5000";
            }
            uri = new($"http://localhost:{port}");

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

        public async Task<List<Vehicle>> GetVehicles()
        {
            Uri allVehicleUri = new Uri(uri, $"api/Vehicle");

            var response = await client.GetAsync(allVehicleUri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Debug.Assert(json != null);
                var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json, jsonSerializationOptions);
                return vehicles;
            }
            else
            {
                // Handle the error or throw an exception
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }
        }

        public async Task<List<Vehicle>> GetUserVehicles(string userid)
        {
            Uri userVehicleUri = new Uri(uri, $"api/Vehicle/{userid}");

            var response = await client.GetAsync(userVehicleUri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(json))
                {
                    try
                    {
                        var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json, jsonSerializationOptions);
                        return vehicles;
                    }
                    catch (JsonException ex)
                    {
                        // Handle deserialization errors
                        Debug.WriteLine($"JSON deserialization error: {ex.Message}");
                        throw;
                    }
                }
                else
                {
                    // Handle the case where JSON content is null or empty
                    return new List<Vehicle>();
                }
            }
            else
            {
                // Handle the error or throw an exception
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }
        }
    }
}
