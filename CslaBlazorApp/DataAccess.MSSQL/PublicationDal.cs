using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;


namespace DataAccess.MSSQL {
	public class PublicationDal : IPublicationDal {
		const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Csla;Integrated Security=True";
		private SqlConnection conn = null;
		private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public PublicationDal() {
			//connectionString = ConfigurationManager.ConnectionStrings["CslaDb"].ConnectionString;
			//connectionString = ConfigurationManager.AppSettings["CslaDb"];
			conn = new SqlConnection(connectionString);
		}

		public bool Delete(int id) {
			conn.Open();
			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "DELETE FROM [dbo].[Publication] WHERE [Id]=@Id";
			cmd.Parameters.AddWithValue("@Id", id);
			try {
				cmd.ExecuteNonQuery();
			} catch (SqlException ex) {
				_log.Error(ex.ToString());
			}
			return true;
		}

		public bool Exists(int id) {
			conn.Open();
			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT Id FROM [dbo].[Publication] WHERE [Id]=@Id";
			cmd.Parameters.AddWithValue("@Id", id);
			var result = cmd.ExecuteReader();
			if (result.HasRows == false) {
				return false;
			} else {
				return true;
			}
		}

		public PublicationDTO Get(int id) {
			PublicationDTO dto = null;
			conn.Open();
			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = "SELECT [Id],[ApprovalDate],[PublishDate],[RequestorEmail],[TitleNl],[TitleFr],[TitleDe],[TitleEn],[Cover] FROM [dbo].[Publication] WHERE [Id]=@Id";
			cmd.Parameters.AddWithValue("@id", id);
			using SqlDataReader reader = cmd.ExecuteReader();
			reader.Read();
			dto = new PublicationDTO() {
				Id = reader.GetInt32("Id"),
				ApprovalDate = reader.GetDateTime("ApprovalDate"),
				PublishDate = reader.GetDateTime("PublishDate"),
				RequestorEmail = reader.GetString("RequestorEmail"),
				TitleFr = reader.GetString("TitleFr"),
				TitleNl = reader.GetString("TitleNl"),
				TitleDe = reader.GetString("TitleDe"),
				TitleEn = reader.GetString("TitleEn"),
			};
			if (!reader.IsDBNull("Cover")) {
				dto.Cover = DalUtils.ParseStrictByteArray(reader, "Cover");
			}
			return dto;
		}

		public List<PublicationDTO> Get() {
			conn.Open();
			List<PublicationDTO> list = new List<PublicationDTO>();

			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.CommandText = "SELECT [Id],[ApprovalDate],[PublishDate],[RequestorEmail],[TitleNl],[TitleFr],[TitleDe],[TitleEn],[Cover] FROM [dbo].[Publication]";
			using SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read()) {
				var dto = new PublicationDTO();
				dto.Id = reader.GetInt32("Id");
				dto.ApprovalDate = reader.GetDateTime("ApprovalDate");
				dto.PublishDate = reader.GetDateTime("PublishDate");
				dto.RequestorEmail = reader.GetString("RequestorEmail");
				dto.TitleFr = reader.GetString("TitleFr");
				dto.TitleNl = reader.GetString("TitleNl");
				dto.TitleDe = reader.GetString("TitleDe");
				dto.TitleEn = reader.GetString("TitleEn");
				if (!reader.IsDBNull("Cover")) {
					dto.Cover = DalUtils.ParseStrictByteArray(reader, "Cover");
				}
				list.Add(dto);
			}

			return list;
		}

		public PublicationDTO Insert(PublicationDTO publication) {
			conn.Open();
			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.CommandText = "usp_Publication_insert";
			cmd.Parameters.AddWithValue(@"ApprovalDate", publication.ApprovalDate);
			cmd.Parameters.AddWithValue(@"PublishDate", publication.PublishDate);
			cmd.Parameters.AddWithValue(@"RequestorEmail", publication.RequestorEmail);
			cmd.Parameters.AddWithValue(@"TitleFr", publication.TitleFr);
			cmd.Parameters.AddWithValue(@"TitleNl", publication.TitleNl);
			cmd.Parameters.AddWithValue(@"TitleDe", publication.TitleDe);
			cmd.Parameters.AddWithValue(@"TitleEn", publication.TitleEn);

			try {
				var result = cmd.ExecuteNonQuery();
			} catch (Exception ex) {
				_log.Error(ex.Message);
			}
			return publication;
		}

		public PublicationDTO Update(PublicationDTO publication) {
			conn.Open();
			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.CommandText = "usp_Publication_update";
			cmd.Parameters.AddWithValue(@"Id", publication.Id);
			cmd.Parameters.AddWithValue(@"ApprovalDate", publication.ApprovalDate);
			cmd.Parameters.AddWithValue(@"PublishDate", publication.PublishDate);
			cmd.Parameters.AddWithValue(@"RequestorEmail", publication.RequestorEmail);
			cmd.Parameters.AddWithValue(@"TitleFr", publication.TitleFr);
			cmd.Parameters.AddWithValue(@"TitleNl", publication.TitleNl);
			cmd.Parameters.AddWithValue(@"TitleDe", publication.TitleDe);
			cmd.Parameters.AddWithValue(@"TitleEn", publication.TitleEn);
			cmd.Parameters.AddWithValue(@"Cover", publication.Cover);

			try {
				var result = cmd.ExecuteNonQuery();
			} catch (Exception ex) {
				_log.Error(ex.Message);
			}
			return publication;
		}

	}

	public static class DalUtils{
		// https://www.akadia.com/services/dotnet_read_write_blob.html

		public static byte[] ParseStrictByteArray(this SqlDataReader reader, string columnName) {
			int colIdx = reader.GetOrdinal(columnName);
			long size = reader.GetBytes(colIdx, 0, null, 0, 0);
			byte[] imageValue = new byte[size];
			// essentially, we are loading all this data in memory, either way... Might as well do it in one swoop if we can
			int bufferSize = (int)Math.Min(int.MaxValue, size);
			//int.MaxValue = 2,147,483,647 = roughly 2 GB of data, so if the data > 2GB we have to read in chunks

			if (size > bufferSize) {

				long bytesRead = 0;
				int position = 0;
				//we need to copy the data over, which means we DON'T want a full copy of all the data in memory. 
				//We need to reduce the buffer size (but not too much, as multiple calls to the reader also affect performance a lot)
				bufferSize = 104857600; //this is roughly 100MB
				byte[] buffer = new byte[bufferSize];
				while (bytesRead < size) {
					if (size - bytesRead < bufferSize)
						bufferSize = Convert.ToInt32(size - bytesRead);

					bytesRead += reader.GetBytes(colIdx, position, buffer, 0, bufferSize);
					//shift the buffer into the final array
					Array.Copy(buffer, 0, imageValue, position, bufferSize);
					position += bufferSize;
				}
			} else {
				//single read into the image buffer
				reader.GetBytes(colIdx, 0, imageValue, 0, bufferSize);
			}
			return imageValue;
		}
	}

}
