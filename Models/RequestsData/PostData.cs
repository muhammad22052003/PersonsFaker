using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations;

namespace Task_5.Models.RequestsData
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

        [FromForm(Name = "IsRandom")]
        public string? IsRandom { get; set; }

        [FromForm(Name = "IsSubmit")]
        public string? IsSubmit { get; set; }

        [FromForm(Name = "More")]
        public int More { get; set; }

        [FromForm(Name = "IsExport")]
        public string? IsExport { get; set; }

        [FromForm(Name = "DataCount")]
        public int DataCount { get; set; }
    }
}
