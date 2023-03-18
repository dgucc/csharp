using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataAccess.MSSQL {
	class TestUspUpdate {
		
		static void TestMain(string[] args) {
			string connectionString = ConfigurationManager.ConnectionStrings["CslaDb"].ConnectionString;
			using SqlConnection conn = new SqlConnection(connectionString);
			conn.Open();


			Console.WriteLine("ServerVersion: {0}", conn.ServerVersion);
			Console.WriteLine("DataSource: {0}", conn.DataSource);
			Console.WriteLine("Database: {0}", conn.Database.ToString());
			Console.WriteLine("State: {0}", conn.State.ToString());

			var publicationId = 4;
			var fileName = @"Publication_0" + publicationId + "_NL.pdf";
			var pdfName = @"C:\workspace\visualstudio\Blazor\CSLA\sandbox\CslaBlazorApp\TEMP\Documents\" + fileName;
			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.CommandText = "usp_Document_update";
			cmd.Parameters.AddWithValue(@"Id", 6);
			cmd.Parameters.AddWithValue(@"FileName", Path.GetFileName(pdfName));
			//cmd.Parameters.AddWithValue(@"MimeType", "application/pdf");
			//cmd.Parameters.AddWithValue(@"Extension", "PDF");
			//using MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(pdfName));
			//cmd.Parameters.AddWithValue("@File", ms.ToArray());
			//using MemoryStream msJpg = new MemoryStream(System.IO.File.ReadAllBytes(pdfName.Replace(".pdf", ".jpg")));
			//cmd.Parameters.AddWithValue(@"Thumbnail", msJpg.ToArray());
			//cmd.Parameters.AddWithValue(@"DocumentType", "Report");
			//cmd.Parameters.AddWithValue(@"IsFR", 1);
			//cmd.Parameters.AddWithValue(@"IsNL", 0);
			//cmd.Parameters.AddWithValue(@"IsDE", 0);
			//cmd.Parameters.AddWithValue(@"IsEN", 0);
			cmd.Parameters.AddWithValue("@PublicationId", publicationId);

			cmd.ExecuteNonQuery();

			Console.WriteLine("Terminated");
			Console.Read();
		}
	}
}
