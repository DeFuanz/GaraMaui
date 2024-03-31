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
    public class RestService : IRestService
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions jsonSerializationOptions;

        private readonly string port;

        private readonly Uri uri;

        private static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public RestService()
        {
            if (Debugger.IsAttached)
            {
                port = "5298";
            }
            else
            {
                port = "5000";
            }
            uri = new($"garaapi-production.up.railway.app");


            client = new HttpClient();
            jsonSerializationOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        //Get all vehicles for all users (useful in needing to create a new vehicle)
        public async Task<List<Vehicle>> GetVehicles()
        {
            Uri allVehicleUri = new(uri, $"api/Vehicle");

            var response = await client.GetAsync(allVehicleUri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Debug.Assert(json != null);
                var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json, jsonSerializationOptions);
                return vehicles!;
            }
            else
            {
                // Handle the error or throw an exception
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }
        }

        //Get vehicles that belong to a specific user
        public async Task<List<UserVehicle>> GetUserVehicles(string userid)
        {
            Uri userVehicleUri = new(uri, $"api/Vehicle/{userid}");

            var response = await client.GetAsync(userVehicleUri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(json))
                {
                    try
                    {
                        var vehicles = JsonSerializer.Deserialize<List<UserVehicle>>(json, jsonSerializationOptions);
                        return vehicles!;
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
                    return [];
                }
            }
            else
            {
                // Handle the error or throw an exception
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }
        }

        public async Task AddVehicle(UserVehicle userVehicle)
        {
            Uri userVehicleUri = new(uri, "api/Vehicle");
            Debug.WriteLine($"POST request to {userVehicleUri}");
            var content = JsonContent.Create(userVehicle);
            try
            {
                HttpResponseMessage? response = await client.PostAsync(userVehicleUri, content);
                if (response.IsSuccessStatusCode)
                {
                    // Handle success
                    Debug.WriteLine("Vehicle added successfully.");
                }
                else
                {
                    // Read the response content for detailed error information
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Request failed with status code {response.StatusCode} and content: {responseContent}");

                    // Optionally, throw an exception with the detailed error
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode} and content: {responseContent}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Handle exceptions that occur during the request
                Debug.WriteLine($"HttpRequestException: {httpEx.Message}");
                throw; // Re-throw the exception if you want to handle it further up the call stack
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Debug.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception if you want to handle it further up the call stack
            }
        }

        public async Task AddGassFillUp(GasFillUp gasFillUp)
        {
            Uri gasUri = new(uri, "/api/Gas");
            Debug.WriteLine($"POST request to {gasUri}");
            var content = JsonContent.Create(gasFillUp);
            try
            {
                HttpResponseMessage? response = await client.PostAsync(gasUri, content);
                if (response.IsSuccessStatusCode)
                {
                    // Handle success
                    Debug.WriteLine("Gas fill up added successfully.");
                }
                else
                {
                    // Read the response content for detailed error information
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Request failed with status code {response.StatusCode} and content: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<List<GasFillUp>> GetGasFillUps(int userVehicleId)
        {
            Uri gasUri = new(uri, $"/api/Gas/{userVehicleId}");
            Debug.WriteLine($"GET request to {gasUri}");
            var response = await client.GetAsync(gasUri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var gasFillUps = JsonSerializer.Deserialize<List<GasFillUp>>(json, jsonSerializationOptions);
                return gasFillUps!;
            }
            else
            {
                // Handle the error or throw an exception
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }
        }
    }
}
