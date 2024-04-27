using Microsoft.AspNetCore.Mvc;

namespace Task5_PersonsFaker.Models.RequestsData
{
    public class PostData
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
}
