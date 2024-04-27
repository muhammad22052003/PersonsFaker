using Task_5.Generators;
using Task_5.Models;
using Task5_PersonsFaker.Helpers;

namespace Task_5.Services
{
    public class FakePersonsGeneratorService
    {
        public FakePersonsGeneratorService
        (
            PersonGenerator personGenerator,
            PersonErrorGenerator errorGenerator
        )
        {
            _errorGenerator = errorGenerator;
            _personGenerator = personGenerator;

        }

        public List<List<string>> GenerateFakePersons(string region ,int generateSeed, int errorSeed,
                                                double errorsValue, int count = 10, int startNumber = 0)
        {
            List<Person> persons = _personGenerator.GeneratePeople(region: region, 
                                                                   seed: generateSeed, 
                                                                   count: count, 
                                                                   startAutoIncrement: startNumber);

            List<List<string>> convertedPersonsData = AdapterPerson.ConvertPersonData(persons, true);

            convertedPersonsData = _errorGenerator.GenerateErrors(persons: convertedPersonsData,
                                                     errorsValue: errorsValue,
                                                     seed: errorSeed);

            return convertedPersonsData;
        }


        private PersonErrorGenerator _errorGenerator {  get; set; }
        public PersonGenerator _personGenerator {  get; set; }
    }
}
