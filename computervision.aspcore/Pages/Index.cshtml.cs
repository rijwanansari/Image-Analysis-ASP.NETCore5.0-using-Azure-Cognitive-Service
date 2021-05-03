using computervision.aspcore.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace computervision.aspcore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICompuerVisionService _compuerVisionService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _compuerVisionService = new CompterVisionService();
            this._hostEnvironment = hostEnvironment;

        }
        [BindProperty]
        public FileUpload fileUpload { get; set; }
        public void OnGet()
        {
            ViewData["SuccessMessage"] = "";
        }
        public async Task<IActionResult> OnPostUpload(FileUpload fileUpload)
        {
            string fullPath = _hostEnvironment.WebRootPath + "/UploadImages/";
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            var formFile = fileUpload.FormFile;
            if (formFile.Length > 0)
            {
                var filePath = Path.Combine(fullPath, formFile.FileName);
                ViewData["ImageUrl"] = formFile.FileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                var imageAnalysis =await _compuerVisionService.AnalyzeImageUrl(filePath);
                if (imageAnalysis.imageAnalysisResult != null)
                    ViewData["ImageAnalysisViewModel"] = imageAnalysis;
            }
            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            ViewData["SuccessMessage"] = fileUpload.FormFile.FileName.ToString() + " file uploaded!!";


            return Page();
        }
        public class FileUpload
        {
            [Required]
            [Display(Name = "File")]
            public IFormFile FormFile { get; set; }
            public string SuccessMessage { get; set; }
        }


    }
}
