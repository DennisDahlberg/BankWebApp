using DataAccessLayer.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.APIs
{
    public class RandomUserService : IRandomUserService
    {
        public readonly string _baseUrl = "https://randomuser.me/api/?gender=";
        public readonly HttpClient _httpClient;
        public readonly BankAppDataContext _dbContext;

        public RandomUserService(HttpClient httpClient, BankAppDataContext dataContext)
        {
            _httpClient = httpClient;
            _dbContext = dataContext;
        }

        public async Task<string> FetchFromApi(int id)
        {
            var gender = _dbContext.Customers.FirstOrDefault(x => x.CustomerId == id).Gender.ToLower();

            gender ??= "female";

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
