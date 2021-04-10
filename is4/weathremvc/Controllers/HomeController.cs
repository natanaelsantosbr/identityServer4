using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using weathremvc.Models;
using weathremvc.Services;

namespace weathremvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ITokenService tokenService)
        {
            this._tokenService = tokenService;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public async Task<IActionResult> Weather2()
        {
            var data = new List<WeatherData>();

            using (var client = new HttpClient())
            {
                var tokenResponse = await this._tokenService.GetToken("weatherapi.read");

                client.SetBearerToken(tokenResponse.AccessToken);

                var result = client
                    .GetAsync("https://localhost:44397/weatherforecast")
                    .Result;

                if (result.IsSuccessStatusCode)
                {
                    var model = result.Content.ReadAsStringAsync().Result;

                    data = JsonConvert.DeserializeObject<List<WeatherData>>(model);

                    return View(data);
                }
                else
                {
                    throw new Exception("Unable to get content");
                }
            }
        }

        [Authorize]
        public async Task<IActionResult> Weather()
        {
            var data = new List<WeatherData>();

            using (var client = new HttpClient())
            {
                var tokenResponse = await this._tokenService.GetToken("weatherapi.read");

                client.SetBearerToken(tokenResponse.AccessToken);

                var result = client
                    .GetAsync("https://localhost:44397/weatherforecast")
                    .Result;

                if (result.IsSuccessStatusCode)
                {
                    var model = result.Content.ReadAsStringAsync().Result;

                    data = JsonConvert.DeserializeObject<List<WeatherData>>(model);

                    return View(data);
                }
                else
                {
                    throw new Exception("Unable to get content");
                }
            }
        }
    }
}
