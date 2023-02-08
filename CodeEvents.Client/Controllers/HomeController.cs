using CodeEvents.Api.Core.DTOs;
using CodeEvents.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace CodeEvents.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7181");
        }

        public async Task<IActionResult> Index()
        {

            var res = await SimpleGet();


            return View();
        }

        private async Task<IEnumerable<CodeEventDto?>> SimpleGet()
        {
            var response = await httpClient.GetAsync("api/events/?includeLectures=true");
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