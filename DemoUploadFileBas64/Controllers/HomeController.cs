using DemoUploadFileBas64.Models;
using DemoUploadFileBas64.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoUploadFileBas64.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileUploadService _fileUploadService;

        public HomeController(ILogger<HomeController> logger, IFileUploadService fileUploadService)
        {
            _logger = logger;
            _fileUploadService = fileUploadService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "Please select a file to upload." });
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    var fileData = memoryStream.ToArray();

                    var fileId = await _fileUploadService.UploadFileAsync(file.FileName, file.ContentType, (int)file.Length, fileData);
                    return Json(new { success = true, message = "File uploaded successfully with ID: " + fileId });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error uploading file: " + ex.Message });
            }
        }

        public async Task<IActionResult> List()
        {
            var files = await _fileUploadService.GetUploadedFilesAsync();
            return View(files);
        }
    }
}
