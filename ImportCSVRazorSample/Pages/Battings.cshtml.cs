using CsvHelper;
using ImportCSVRazorSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.IO;

namespace ImportCSVRazorSample.Pages
{
    public class BattingModel : PageModel
    {
        private readonly ILogger<BattingModel> _logger;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;


        [BindProperty]
        public IFormFile UploadCSV { get; set; }

        public IEnumerable<Batting> BattingStats { get; set; }

        public BattingModel(ILogger<BattingModel> logger,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            using (var stream = UploadCSV.OpenReadStream())
            { 
                using (var textStream = new StreamReader(stream))
                {
                    using (var csv = new CsvReader(textStream, CultureInfo.InvariantCulture))
                    {
                        BattingStats = csv.GetRecords<Batting>();
                    }
                }
                return Page();
            }
        }
    }
}