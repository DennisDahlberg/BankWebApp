using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            return null;
        }
    }
}
