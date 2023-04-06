using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using log4net.Config;
using log4net;
using System.Reflection;
using Csla;
using CslaBlazorApp.Shared;
using DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EnumDocumentType = DataAccess.EnumDocumentType;
using EnumLanguageCode = DataAccess.EnumLanguageCode;
using CslaBlazorApp.Server.Services;

namespace CslaBlazorApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PDFController : ControllerBase {
	private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	private readonly IHostEnvironment _env;

	public PDFController(IHostEnvironment env) {
		_env = env;
	}


	[HttpPost("upload")]
	public async Task<IActionResult> Upload(
		IFormFile pdf, [FromForm] int publicationId,
		[FromForm] string documentType,
		[FromForm] string lang) {

		if (pdf == null || pdf.Length == 0) {
			return BadRequest("Please upload a file");
		}

		string filename = pdf.FileName;
		string extension = Path.GetExtension(filename).ToLower();

		string[] allowedExtensions = { ".pdf" };

		if (!allowedExtensions.Contains(extension)) {
			return BadRequest("File is not a supported (PDF only)");
		}

		string newFileName = $"{Guid.NewGuid()}{extension}";
			
		byte[] pdfBytes;
		using (var ms = new MemoryStream()) {
			pdf.CopyTo(ms);
			var fileBytes = ms.ToArray();
			pdfBytes = ms.ToArray(); 
		}

		try {
			var document = new DocumentDTO() {
				Filename = filename,
				MimeType = "application/pdf",
				Extension = extension.Remove(0,1).ToUpper(),
				CreatedOn = DateTime.Now,
				Description = null,
				UploadedBy = @"Dgucc",
				DocumentType = Enum.Parse<EnumDocumentType>(documentType),
				Language = Enum.Parse<EnumLanguageCode>(lang),
				File = pdfBytes,
				Thumbnail = DocnetService.PDFPage2JPGFixedHeight(pdfBytes, 297, 1),
				PublicationId = publicationId,
			};

			var dal = new DataAccess.Mock.DocumentDal(); // Temporarily........................
			//var dal = new DataAccess.MSSQL.DocumentDal(); // Temporarily.....................
			dal.Insert(document);

		} catch (Exception ex) {
			_log.Error(ex.Message);
			return BadRequest(ex.Message);
		}


		// return uploaded pdf for test purpose
		//return new FileStreamResult(new MemoryStream(pdfBytes), "application/pdf") {
		//	FileDownloadName = $"{Guid.NewGuid()}.pdf"
		//};			
		return Ok(filename + " Uploaded");
	}


	[HttpPost("upload/cover")]
	public async Task<IActionResult> UploadCover(
			IFormFile image, 
			[FromForm] int publicationId) {

		if (image == null || image.Length == 0) {
			return BadRequest("Please upload a file");
		}

		string filename = image.FileName;
		string extension = Path.GetExtension(filename).ToLower();

		string[] allowedExtensions = { ".jpg",".png", ".jpeg"};

		if (!allowedExtensions.Contains(extension)) {
			return BadRequest("File is not a supported (JPG,PNG only)");
		}

		string newFileName = $"{Guid.NewGuid()}{extension}";

		byte[] imageBytes;
		using (var ms = new MemoryStream()) {
			image.CopyTo(ms);
			var fileBytes = ms.ToArray();
			imageBytes = ms.ToArray(); 
		}

		try {
			var dal = new DataAccess.Mock.PublicationDal(); // Temporarily........................
			//var dal = new DataAccess.MSSQL.PublicationDal(); // Temporarily.....................

			dal.UpdateCover(publicationId, imageBytes);

		} catch (Exception ex) {
			_log.Error(ex.Message);
			return BadRequest(ex.Message);
		}


		// return uploaded pdf for test purpose
		//return new FileStreamResult(new MemoryStream(pdfBytes), "application/pdf") {
		//	FileDownloadName = $"{Guid.NewGuid()}.pdf"
		//};			
		return Ok(filename + " Uploaded");
	}
}
