using Bogus;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Task_5.Models.RequestsData;
using Task_5.Services;
using Task_5.Models;
using Person = Task_5.Models.Person;
using Task5_PersonsFaker.Helpers;

namespace Task5_PersonsFaker.Controllers
{
    public class CustomPostData
    {
        [FromForm(Name = "Region")]
        public string Region { get; set; }

        [FromForm(Name = "ErrorsValue")]
        public double ErrorsValue { get; set; }

        [FromForm(Name = "ErrorsSeed")]
        public int ErrorsSeed { get; set; }

        [FromForm(Name = "GenerationSeed")]
        public int GenerationSeed { get; set; }

        [FromForm(Name = "More")]
        public int More { get; set; }
    }

    public class ApiController : Controller
    {
        private FakePersonsGeneratorService _fakePersonsGeneratorService;

        public ApiController([FromServices] FakePersonsGeneratorService fakePersonGenerator)
        {
            _fakePersonsGeneratorService = fakePersonGenerator;
        }

        [HttpPost]
        public IActionResult Index([FromBody] CustomPostData data)
        {
            List<Person> persons = _fakePersonsGeneratorService
                .GenerateFakePersons(region : data.Region,
                                     generateSeed : data.GenerationSeed,
                                     errorSeed : data.ErrorsSeed,
                                     errorsValue : data.ErrorsValue,
                                     count : data.More + 20,
                                     startNumber : 1);

            var personsData = AdapterPerson.ConvertPersonData(persons, withHeaders : true);

            string json = JsonSerializer.Serialize(personsData);

            JsonDocument jsonDocument = JsonDocument.Parse(json);

            return Json(jsonDocument);
        }
    }
}
