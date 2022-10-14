using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;


namespace BlazorFile.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PdfsController : ControllerBase {

        private readonly IHostEnvironment _env;

        public PdfsController(IHostEnvironment env) {
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] IFormFile pdf) {
            if (pdf == null || pdf.Length == 0)
                return BadRequest("Upload a file");

            string fileName = pdf.FileName;
            string extension = Path.GetExtension(fileName);

            string[] allowedExtensions = { ".pdf" };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("File is not a pdf");

            string newFileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Pdfs", newFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write)) {
                await pdf.CopyToAsync(fileStream);
            }


            return Ok($"Pdfs/{newFileName}");
        }


        [HttpPost("pdf2jpg")]
        public async Task<IActionResult> Pdf2Jpg([FromForm] IFormFile pdf, [FromForm] int width, [FromForm] int height) {
            if (pdf == null || pdf.Length == 0)
                return BadRequest("Upload a file");

            string fileName = pdf.FileName;
            string extension = Path.GetExtension(fileName);

            string[] allowedExtensions = { ".pdf" };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("File is not a pdf");

            string newFileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Pdfs", newFileName);

            string jpgPath = "";
            using (var ms = new MemoryStream()) {
                pdf.CopyTo(ms);
                var fileBytes = ms.ToArray();
                jpgPath = PdfUtils.Pdf2Jpg(fileBytes, width, height);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write)) {
                await pdf.CopyToAsync(fileStream);
            }


            return Ok($"{jpgPath}");
        }
    }
}
