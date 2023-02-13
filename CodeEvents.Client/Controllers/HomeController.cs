using CodeEvents.Api.Core.DTOs;
using CodeEvents.Client.Clients;
using CodeEvents.Client.Helpers;
using CodeEvents.Client.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CodeEvents.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ICodeEventClient codeEventClient;
        private const string json = "application/json";

        public HomeController(IHttpClientFactory httpClientFactory, HttpClient client, ICodeEventClient codeEventClient)
        {
           
            httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7181");

            this.httpClientFactory = httpClientFactory;
            this.codeEventClient = codeEventClient;
            // httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(json));
        }

        public async Task<IActionResult> Index()
        {

            //var res = await SimpleGet();
            // var res = await GetWithRequestMessage();
            // var res = await CreateLecture();
            // var res = await PatchCodeEvent();

            var res = await codeEventClient.GetAsync<IEnumerable<CodeEventDto>>(UriHelper.GetEvents());
            //var res2 = await codeEventClient.GetAsync<CodeEventDto>(UriHelper.GetEvent("NewName"));
            //var res3 = await codeEventClient.GetAsync<LectureDto>(UriHelper.GetLectureForEvent("NewName", 1));


            return View();
        } 
        

        private async Task<CodeEventDto> PatchCodeEvent()
        {
            var httpClient = httpClientFactory.CreateClient("CodeEventsClient");

            var patchDokument = new JsonPatchDocument<CodeEventDto>();
            patchDokument.Replace(c => c.LocationCityTown, "Hökarängen");
            patchDokument.Remove(c => c.LocationStateProvince);

             var serializedPatch = Newtonsoft.Json.JsonConvert.SerializeObject(patchDokument);
            //var serializedPatch = JsonSerializer.Serialize(patchDokument); NotWorking needs Newtonsoft.Json

            var request = new HttpRequestMessage(HttpMethod.Patch, "api/events/NewName");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            request.Content = new StringContent(serializedPatch);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var codeEventDto = JsonSerializer.Deserialize<CodeEventDto>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return codeEventDto!;
        }

        private async Task<LectureDto> CreateLecture()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/events/NewName/lectures");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            var lecture = new CreateLectureDto
            {
                Level = 56,
                Title = "From Client"
            };

            var serializedLecture = JsonSerializer.Serialize(lecture);

            request.Content = new StringContent(serializedLecture);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(json);

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var lectureDto = JsonSerializer.Deserialize<LectureDto>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return lectureDto!;

        }

        private async Task<IEnumerable<CodeEventDto?>> GetWithRequestMessage()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/events");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(json));

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var codeEvents = JsonSerializer.Deserialize<IEnumerable<CodeEventDto>>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return codeEvents!;
        }

        private async Task<IEnumerable<CodeEventDto?>> SimpleGet()
        {
            var response = await httpClient.GetAsync("api/events/?includelectures=true");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var codeEvents = JsonSerializer.Deserialize<IEnumerable<CodeEventDto>>(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            return codeEvents!;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}