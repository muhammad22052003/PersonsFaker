using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using Task_5.Models;
using Task_5.Models.RequestsData;
using Task_5.Services;
using Task5_PersonsFaker.Helpers;
using Person = Task_5.Models.Person;

namespace Task_5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FakePersonsGeneratorService _personGenerator;
        private int _generationCount = 20;

        public HomeController
        (
            ILogger<HomeController> logger,
            [FromServices] FakePersonsGeneratorService fakePersonGenerator
        )
        {
            _personGenerator = fakePersonGenerator;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            PairData pair = new PairData();

            pair.Regions = _personGenerator._personGenerator.RegionsPack.Regions;
            pair.PostData.Region = _personGenerator._personGenerator.RegionsPack.Regions.FirstOrDefault().Key;

            pair.Persons = _personGenerator.GenerateFakePersons(region: pair.PostData.Region,
                                                               generateSeed: pair.PostData.GenerationSeed,
                                                               errorSeed: pair.PostData.ErrorsSeed,
                                                               errorsValue: pair.PostData.ErrorsValue,
                                                               count: 20,
                                                               startNumber: 1);

            
            return View(pair);
        }

        [HttpPost]
        public IActionResult Index(PostData data)
        {
            Random random = new Random();

            PairData pair = new PairData();
            pair.PostData = data;
            pair.Regions = _personGenerator._personGenerator.RegionsPack.Regions;

            if (data.IsRandom != null)
            {
                pair.PostData.GenerationSeed = random.Next();
                pair.PostData.ErrorsSeed = random.Next();
                pair.PostData.ErrorsValue = (int)random.Next(1000);
            }

            var persons = _personGenerator.GenerateFakePersons(region: pair.PostData.Region,
                                                               generateSeed: pair.PostData.GenerationSeed,
                                                               errorSeed: pair.PostData.ErrorsSeed,
                                                               errorsValue: pair.PostData.ErrorsValue,
                                                               count: pair.PostData.More + 20,
                                                               startNumber: 1);


            pair.Persons = persons;

            return View(pair);
        }

        [HttpPost]
        public FileResult Export(PostData data)
        {
            List<Person> persons = _personGenerator.GenerateFakePersons(region: data.Region,
                                                               generateSeed: data.GenerationSeed,
                                                               errorSeed: data.ErrorsSeed,
                                                               errorsValue: data.ErrorsValue,
                                                               count: data.DataCount + 20,
                                                               startNumber: 1);

            
            string hash = persons.GetHashCode().ToString();
            string filePath = $"wwwroot\\libraryExports\\{hash}.csv";
            var personsData = AdapterPerson.ConvertPersonData(persons);

            CsvStreamer.WriteData(personsData, filePath);

            // Прочитайте файл в массив байтов
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = "table.csv"; // Имя файла для отображения в диалоге сохранения

            // Верните файл пользователю
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
