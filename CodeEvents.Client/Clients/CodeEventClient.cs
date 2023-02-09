using CodeEvents.Api.Core.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CodeEvents.Client.Clients
{
    public class CodeEventClient : BaseClient, ICodeEventClient
    {

        public CodeEventClient(HttpClient httpClient) : base(httpClient)
        {
            HttpClient.BaseAddress = new Uri("https://localhost:7181");
            HttpClient.Timeout = new TimeSpan(0, 0, 30);
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CodeEventDto?> GetCodeEventAsync(string name)
        {
            return await GetAsync<CodeEventDto>($"api/events/{name}");
        }

        public async Task<IEnumerable<CodeEventDto>?> GetCodeEventsAsync()
        {
            return await GetAsync<IEnumerable<CodeEventDto>>($"api/events");
        }

        public async Task<LectureDto?> GetLectureAsync(string name, int id)
        {
            return await GetAsync<LectureDto>($"api/events/{name}/lectures/{id}");
        }



    }
}
