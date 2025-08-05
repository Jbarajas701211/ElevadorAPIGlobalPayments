using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApplicationElevador.Interfaces;

namespace WebApplicationElevador.Utility
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration configuration;

        public HttpClientHelper(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            this.configuration = configuration;
        }

        public async Task<T> GetTAsync<T>(string url, string token = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration.GetValue<string>("UrlApi")}{url}");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<T> PostAsync<T>(string url, object data, string token = null)
        {
            var jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, $"{configuration.GetValue<string>("UrlApi")}{url}")
            {
                Content = content
            };

            if(!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
        }
    }
}
