﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFile.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase {

        private readonly IHostEnvironment _env;
        
        public ImagesController(IHostEnvironment env) {
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile image) {
            if (image == null || image.Length == 0)
                return BadRequest("Upload a file");

            string fileName = image.FileName;
            string extension = Path.GetExtension(fileName);

            string[] allowedExtensions = { ".jpg", ".png", ".bmp" };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("File is not a valid image");

            string newFileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Images", newFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write)) {
                await image.CopyToAsync(fileStream);
            }

            return Ok($"Images/{newFileName}");
        }
    }
}
