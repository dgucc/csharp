using System;
using System.Data.SqlClient;
using Csla.Configuration;

namespace DataAccess.MSSQL {
	class Program {
		
		static void Main(string[] args) {
			//string connectionString = ConfigurationManager.ConnectionStrings["CslaDb"].ConnectionString;
			string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Csla;Integrated Security=True";
			using SqlConnection conn = new SqlConnection(connectionString);
			conn.Open();


			Console.WriteLine("ServerVersion: {0}", conn.ServerVersion);
			Console.WriteLine("DataSource: {0}", conn.DataSource);
			Console.WriteLine("Database: {0}", conn.Database.ToString());
			Console.WriteLine("State: {0}", conn.State.ToString());

			// Upload Publication.Cover
			/*
			var publicationId = 4;
			var fileName = @"Publication_0" + publicationId + "_NL.pdf";
			var pdfName = @"C:\workspace\visualstudio\Blazor\CSLA\sandbox\CslaBlazorApp\TEMP\Documents\" + fileName;
			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.CommandText = "usp_Publication_update";
			cmd.Parameters.AddWithValue(@"Id", publicationId);
			using MemoryStream msJpg = new MemoryStream(System.IO.File.ReadAllBytes(pdfName.Replace(".pdf", ".jpg")));
			cmd.Parameters.AddWithValue(@"Cover", msJpg.ToArray());
			cmd.ExecuteNonQuery();
			*/

			Console.WriteLine("Terminated");
			Console.Read();
		}
	}
}
