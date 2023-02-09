using CodeEvents.Api.Core.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CodeEvents.Client.Clients
{
    public class CodeEventClient
    {
        private readonly HttpClient httpClient;
        private const string json = "application/json";

        public CodeEventClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:7181");
            this.httpClient.Timeout = new TimeSpan(0 ,0, 30);
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(json));
        }

        public async Task<IEnumerable<CodeEventDto?>> GetWithRequestMessage()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/events");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var codeEvents = JsonSerializer.Deserialize<IEnumerable<CodeEventDto>>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return codeEvents!;
        }

    }
}
