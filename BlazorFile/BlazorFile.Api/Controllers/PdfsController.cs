using BlazorFile.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFile.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PdfsController : ControllerBase {

        [HttpPost("pdf2jpg")]
        public async Task<IActionResult> PdfPage2Jpg([FromForm] IFormFile pdf, [FromForm] int page = 1) {
            if (pdf == null || pdf.Length == 0)
                return BadRequest("Upload a file");

            string fileName = pdf.FileName;
            string extension = Path.GetExtension(fileName);

            string[] allowedExtensions = { ".pdf" };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("File is not a pdf");

            byte[] image;
            using (var ms = new MemoryStream()) {
                pdf.CopyTo(ms);
                var fileBytes = ms.ToArray();
                image = DocnetService.PdfPage2Jpg(fileBytes, page);
            }

            //return Ok(File(image, "image/jpeg", $"{Guid.NewGuid()}.jpg"));
            return new FileStreamResult(new MemoryStream(image), "image/jpeg") {
                FileDownloadName = $"{Guid.NewGuid()}.jpg"
            };
        }

        [HttpPost("page2jpg/fixedWidth")]
        public async Task<IActionResult> PdfPage2JpgFixedWidth([FromForm] IFormFile pdf, [FromForm] int width, [FromForm] int page = 1) {
            if (pdf == null || pdf.Length == 0)
                return BadRequest("Upload a file");

            string fileName = pdf.FileName;
            string extension = Path.GetExtension(fileName);

            string[] allowedExtensions = { ".pdf" };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("File is not a pdf");

            byte[] image;
            using (var ms = new MemoryStream()) {
                pdf.CopyTo(ms);
                var fileBytes = ms.ToArray();
                image = DocnetService.PdfPage2JpgFixedWidth(fileBytes, width, page);
            }

            //return Ok(File(image, "image/jpeg", $"{Guid.NewGuid()}.jpg"));
            return new FileStreamResult(new MemoryStream(image), "image/jpeg") {
                FileDownloadName = $"{Guid.NewGuid()}.jpg"
            };
        }


        [HttpPost("page2jpg/fixedHeight")]
        public async Task<IActionResult> PdfPage2JpgFixedHeight([FromForm] IFormFile pdf, [FromForm] int height, [FromForm] int page = 1) {
            if (pdf == null || pdf.Length == 0)
                return BadRequest("Upload a file");

            string fileName = pdf.FileName;
            string extension = Path.GetExtension(fileName);

            string[] allowedExtensions = { ".pdf" };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("File is not a pdf");

            byte[] image;
            using (var ms = new MemoryStream()) {
                pdf.CopyTo(ms);
                var fileBytes = ms.ToArray();
                image = DocnetService.PdfPage2JpgFixedHeight(fileBytes, height, page);
            }

            //return Ok(File(image, "image/jpeg", $"{Guid.NewGuid()}.jpg"));
            return new FileStreamResult(new MemoryStream(image), "image/jpeg") {
                FileDownloadName = $"{Guid.NewGuid()}.jpg"
            };
        }
    }
}
