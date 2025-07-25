
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace MyCvApp.Controllers
{
    public class GalleryController : Controller
    {
        private readonly string _imagePath = "wwwroot/images";

        // This allows navigation to /Gallery to work
        public IActionResult Index()
        {
            return RedirectToAction("ViewGallery");
        }

        // Page to upload an image
        public IActionResult Upload()
        {
            return View();
        }

        // Handles image upload
        [HttpPost]
        public IActionResult Upload(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), _imagePath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
            }
            return RedirectToAction("ViewGallery");
        }  
        public IActionResult ViewGallery()
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), _imagePath);

            // Make sure the folder exists
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath); // Create it if it doesn't exist
            }

            var imageFiles = Directory.GetFiles(fullPath)
                                      .Select(Path.GetFileName)
                                      .ToList();

            ViewBag.Images = imageFiles;
            return View();
        }
    }
}
