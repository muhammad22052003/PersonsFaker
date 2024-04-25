using Task_5.Controllers;

namespace Task_5.Models.RequestsData
{
    public class PairData
    {
        public PairData()
        {
            PostData = new PostData()
            {
                ErrorsValue = 0,
                ErrorsSeed = 0,
                GenerationSeed = 0,
                IsRandom = null
            };
        }

        public PostData PostData { get; set; }

        public List<Person> Persons { get; set; }

        public Dictionary<string, string> Regions { get; set; }

    }
}
