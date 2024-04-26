using Task_5.Models;

namespace Task5_PersonsFaker.Helpers
{
    public class AdapterPerson
    {
        public static List<List<string>> ConvertPersonData(List<Person> persons)
        {
            List<List<string>> result = new List<List<string>>();

            result.Add(new List<string>());

            result[0].Add("Index");
            result[0].Add("Name");
            result[0].Add("Lastname");
            result[0].Add("Adress");
            result[0].Add("Phone");
            result[0].Add("Zipcode");

            for (int i = 1; i <= persons.Count; i++)
            {
                result.Add(new List<string>());
                result[i].Add(persons[i - 1].IndexNumber.ToString());
                result[i].Add(persons[i - 1].FirstName);
                result[i].Add(persons[i - 1].LastName);
                result[i].Add(persons[i - 1].Adress);
                result[i].Add(persons[i - 1].PhoneNumber);
                result[i].Add(persons[i - 1].ZipCode);
            }

            return result;
        }
    }
}
