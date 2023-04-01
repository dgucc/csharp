using BlazorFile.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorFile.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase {

        [HttpPost("pdf2jpg")]
        public async Task<IActionResult> PdfPage2Jpg(IFormFile pdf, [FromForm] int page = 1) {
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
                try {
                    image = DocnetService.PdfPage2Jpg(fileBytes, page);
                } catch (Exception ex) {
                    return BadRequest(ex.StackTrace);
                }
            }

            //return Ok(File(image, "image/jpeg", $"{Guid.NewGuid()}.jpg"));
            return new FileStreamResult(new MemoryStream(image), "image/jpeg") {
                FileDownloadName = $"{Guid.NewGuid()}.jpg"
            };
        }

        [HttpPost("page2jpg/fixedWidth")]
        public async Task<IActionResult> PdfPage2JpgFixedWidth(IFormFile pdf, [FromForm] int width, [FromForm] int page = 1) {
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
                try {
                    image = DocnetService.PdfPage2JpgFixedWidth(fileBytes, width, page);
                } catch (Exception ex) {
                    return BadRequest(ex.StackTrace);
                }
            }

            //return Ok(File(image, "image/jpeg", $"{Guid.NewGuid()}.jpg"));
            return new FileStreamResult(new MemoryStream(image), "image/jpeg") {
                FileDownloadName = $"{Guid.NewGuid()}.jpg"
            };
        }


        [HttpPost("page2jpg/fixedHeight")]
        public async Task<IActionResult> PdfPage2JpgFixedHeight(IFormFile pdf, [FromForm] int height, [FromForm] int page = 1) {
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
                try {
                    image = DocnetService.PdfPage2JpgFixedHeight(fileBytes, height, page);
                } catch (Exception ex) {
                    return BadRequest(ex.StackTrace);
                }
            }

            //return Ok(File(image, "image/jpeg", $"{Guid.NewGuid()}.jpg"));
            return new FileStreamResult(new MemoryStream(image), "image/jpeg") {
                FileDownloadName = $"{Guid.NewGuid()}.jpg"
            };
        }

        [HttpPost("Metadata/Update")] // Author, Title, Abstract
        public async Task<IActionResult> PDFMetadataUpdate(IFormFile pdf, [FromForm] string author = "test_author", [FromForm] string title = "test_title", [FromForm] string abstr = "test_abstract") {
            if (pdf == null || pdf.Length == 0)
                return BadRequest("Upload a file");

            string fileName = pdf.FileName;
            string extension = Path.GetExtension(fileName);

            string[] allowedExtensions = { ".pdf" };

            if (!allowedExtensions.Contains(extension))
                return BadRequest("File is not a pdf");

            byte[] result;
            using (var ms = new MemoryStream()) {
                pdf.CopyTo(ms);
                var fileBytes = ms.ToArray();
                try {
                    result = ITextService.UpdatePDFMetaData(fileBytes, author, title, abstr);
                } catch (Exception ex) {
                    return BadRequest(ex.StackTrace);
                }
            }

            //return Ok(File(result, "application/pdf", $"{Guid.NewGuid()}.pdf"));
            return new FileStreamResult(new MemoryStream(result), "application/pdf") {
                FileDownloadName = $"{Guid.NewGuid()}.pdf"
            };
        }


    }
}
