using CodeEvents.Api.Core.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CodeEvents.Client.Clients
{
    public class CodeEventClient : ICodeEventClient
    {
        private readonly HttpClient httpClient;

        public CodeEventClient(HttpClient httpClient) 
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:7181");
            this.httpClient.Timeout = new TimeSpan(0, 0, 30);
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T?> GetAsync<T>(string path, string contentType = "application/json")
        {
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();

            return JsonSerializer.Deserialize<T>(stream, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })!;
        }
    }
}
