using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.APIs
{
    public class RandomUserService
    {
        public readonly string _baseUrl = "https://randomuser.me/api/?gender=";
        public readonly HttpClient _httpClient;

        public RandomUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> FetchFromApi(string gender)
        {
            if (string.IsNullOrEmpty(gender))
                gender = "female";
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}{gender}");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonString = await response.Content.ReadAsStringAsync();

            var jsonDoc = JsonDocument.Parse(jsonString);

            return jsonDoc.RootElement.GetProperty("results")[0].GetProperty("picture").GetProperty("large").GetString();
        }
    }
}
