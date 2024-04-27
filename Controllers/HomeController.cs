using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using Task_5.Models;
using Task_5.Services;
using Task5_PersonsFaker.Helpers;
using Task5_PersonsFaker.Models.RequestsData;
using Person = Task_5.Models.Person;

namespace Task_5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FakePersonsGeneratorService _fakePersonsGeneratorService;
        private readonly IConfiguration _configuration;

        public HomeController
        (
            ILogger<HomeController> logger,
            [FromServices] FakePersonsGeneratorService fakePersonGenerator,
            IConfiguration configuration
        )
        {
            _fakePersonsGeneratorService = fakePersonGenerator;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
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
