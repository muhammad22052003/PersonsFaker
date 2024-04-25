using Task_5.Models;

namespace Task5_PersonsFaker.Helpers
{
    public class AdapterPerson
    {
        public static List<List<string>> ConvertPersonData(List<Person> persons)
        {
            List<List<string>> result = new List<List<string>>();

            for (int i = 0; i < persons.Count; i++)
            {
                result.Add(new List<string>());
                if(i == 0)
                {
                    result[i].Add("Index");
                    result[i].Add("Name");
                    result[i].Add("Lastname");
                    result[i].Add("Adress");
                    result[i].Add("Phone");
                    result[i].Add("Zipcode");
                }
                else
                {
                    result[i].Add(persons[i].IndexNumber.ToString());
                    result[i].Add(persons[i].FirstName);
                    result[i].Add(persons[i].LastName);
                    result[i].Add(persons[i].Adress);
                    result[i].Add(persons[i].PhoneNumber);
                    result[i].Add(persons[i].ZipCode);
                }
            }

            return result;
        }
    }
}
