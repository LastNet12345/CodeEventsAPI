using CodeEvents.Api.Core.DTOs;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CodeEvents.Client.Clients
{
    public class BaseClient
    {
        public  HttpClient HttpClient { get; }

        public BaseClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        } 
        
        public BaseClient(HttpClient httpClient, Uri uri) : this(httpClient)
        {
            HttpClient.BaseAddress = uri;
        }

        public async Task<T?> GetAsync<T>(string path, string contentType = "application/json")
        {
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

            var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();

            return JsonSerializer.Deserialize<T>(stream, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })!;
        }


    }
}
