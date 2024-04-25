using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations;

namespace Task_5.Models.RequestsData
{
    public class PostData
    {
        [Required]
        [HtmlAttributeName("Region")]
        public string Region { get; set; }

        [Required]
        [HtmlAttributeName("ErrorsValue")]
        public double ErrorsValue { get; set; }

        [Required]
        [HtmlAttributeName("ErrorsSeed")]
        public int ErrorsSeed { get; set; }

        [Required]
        [HtmlAttributeName("GenerationSeed")]
        public int GenerationSeed { get; set; }

        [HtmlAttributeName("IsRandom")]
        public string? IsRandom { get; set; }

        [HtmlAttributeName("IsRandom")]
        public string? IsSubmit { get; set; }

        [HtmlAttributeName("IsRandom")]
        public int? More { get; set; }

        [HtmlAttributeName("IsRandom")]
        public string? IsExport { get; set; }
    }
}
