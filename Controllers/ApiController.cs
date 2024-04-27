using Bogus;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Task_5.Services;
using Task_5.Models;
using Person = Task_5.Models.Person;
using Task5_PersonsFaker.Helpers;
using Task5_PersonsFaker.Models.RequestsData;
using Microsoft.Extensions.Configuration;

namespace Task5_PersonsFaker.RequestData
{
    public class ApiController : Controller
    {
        private FakePersonsGeneratorService _fakePersonsGeneratorService;
        private readonly IConfiguration _configuration;

        public ApiController
            (
                [FromServices] 
                FakePersonsGeneratorService fakePersonGenerator,
                IConfiguration configuration
            )
        {
            _fakePersonsGeneratorService = fakePersonGenerator;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult GetData([FromBody] PostData data)
        {
            List<List<string>> persons = _fakePersonsGeneratorService
                .GenerateFakePersons(region : data.Region,
                                     generateSeed : data.GenerationSeed,
                                     errorSeed : data.ErrorsSeed,
                                     errorsValue : data.ErrorsValue,
                                     count : data.More + 20,
                                     startNumber : 1);


            string json = JsonSerializer.Serialize(persons);

            JsonDocument jsonDocument = JsonDocument.Parse(json);

            return Json(jsonDocument);
        }

        [HttpGet]
        public IActionResult GetData()
        {
            List<List<string>> persons = _fakePersonsGeneratorService
                .GenerateFakePersons(region: "United States",
                                     generateSeed: 1,
                                     errorSeed: 0,
                                     errorsValue: 0,
                                     count: 0 + 20,
                                     startNumber: 1);


            string json = JsonSerializer.Serialize(persons);

            JsonDocument jsonDocument = JsonDocument.Parse(json);

            return Json(jsonDocument);
        }

        [HttpPost]
        public IActionResult Export([FromBody] PostData data)
        {
            List<List<string>> persons = _fakePersonsGeneratorService
                .GenerateFakePersons(region: data.Region,
                                     generateSeed: data.GenerationSeed,
                                     errorSeed: data.ErrorsSeed,
                                     errorsValue: data.ErrorsValue,
                                     count: data.More + 20,
                                     startNumber: 1);

            string hashCode = persons.GetHashCode().ToString();

            string path = $"{_configuration.GetValue<string>("filePaths:exportFilesLinux")}{hashCode}.csv";

            CsvStreamer.WriteData(persons, path);

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = hashCode;

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, hashCode);
        }
    }
}
