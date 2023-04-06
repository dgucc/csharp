using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace DataAccess.MSSQL {
	public class DocumentDal : IDocumentDal {
		const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Csla;Integrated Security=True";
		private SqlConnection conn = null;
		private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public DocumentDal() {
			conn = new SqlConnection(connectionString);
		}
		public bool Delete(int id) {
			conn.Open();
			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "usp_Document_delete";
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
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.CommandText = "usp_Document_select";
			cmd.Parameters.AddWithValue("@Id", id);
			var result = cmd.ExecuteReader();
			if (result.HasRows == false) {
				return false;
			} else {
				return true;
			}
		}

		public DocumentDTO Get(int id) {
			conn.Open();
			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.CommandText = "usp_Document_select";
			cmd.Parameters.AddWithValue("@Id", id);
			using SqlDataReader reader = cmd.ExecuteReader();
			reader.Read();
			var dto = new DocumentDTO();
			dto.Id = reader.GetInt32("Id");
			dto.Filename = (!reader.IsDBNull("Filename")) ? reader.GetString("Filename") : "";
			dto.MimeType = (!reader.IsDBNull("MimeType")) ? reader.GetString("MimeType") : "";
			dto.Extension = (!reader.IsDBNull("Extension")) ? reader.GetString("Extension") : "";
			dto.Description = (!reader.IsDBNull("Description")) ? reader.GetString("Description") : "";
			dto.UploadedBy = (!reader.IsDBNull("UploadedByUser")) ? reader.GetString("UploadedBy") : "";
			dto.DocumentType = Enum.Parse<EnumDocumentType>(reader.GetString("DocumentType"));
			dto.Language = Enum.Parse<EnumLanguageCode>(reader.GetString("language"));
			dto.PublicationId = reader.GetInt32("PublicationId");
			if (!reader.IsDBNull("CreatedOn")) {
				dto.CreatedOn = reader.GetDateTime("CreatedOn");
			}
			if (!reader.IsDBNull("File")) {
				dto.File = DocumentDalUtils.ParseStrictByteArray(reader, "File");
			}
			if (!reader.IsDBNull("Thumbnail")) {
				dto.File = DocumentDalUtils.ParseStrictByteArray(reader, "Thumbnail");
			}
			return dto;
		}

		public List<DocumentDTO> Get() {
			conn.Open();
			List<DocumentDTO> list = new List<DocumentDTO>();

			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.CommandText = "usp_Documents_select";
			using SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read()) {
				var dto = new DocumentDTO();
				dto.Id = reader.GetInt32("Id");
				dto.Filename = (!reader.IsDBNull("Filename")) ? reader.GetString("Filename") : "";
				dto.MimeType = (!reader.IsDBNull("MimeType")) ? reader.GetString("MimeType") : "";
				dto.Extension = (!reader.IsDBNull("Extension")) ? reader.GetString("Extension") : "";
				dto.Description = (!reader.IsDBNull("Description")) ? reader.GetString("Description") : "";
				dto.UploadedBy = (!reader.IsDBNull("UploadedByUser")) ? reader.GetString("UploadedBy") : "";
				dto.DocumentType = Enum.Parse<EnumDocumentType>(reader.GetString("DocumentType"));
				dto.Language = Enum.Parse<EnumLanguageCode>(reader.GetString("language"));
				dto.PublicationId = reader.GetInt32("PublicationId");
				if (!reader.IsDBNull("CreatedOn")) {
					dto.CreatedOn = reader.GetDateTime("CreatedOn");
				}

				if (!reader.IsDBNull("File")) {
					dto.File = DocumentDalUtils.ParseStrictByteArray(reader, "File");
				}
				if (!reader.IsDBNull("Thumbnail")) {
					dto.File = DocumentDalUtils.ParseStrictByteArray(reader, "Thumbnail");
				}
				list.Add(dto);
			}

			return list;
		}

		public List<DocumentDTO> GetByPublication(int publicationId) {
			List<DocumentDTO> list = new List<DocumentDTO>();
			try {
				conn.Open();
				using SqlCommand cmd = conn.CreateCommand();
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.CommandText = "usp_DocumentsByPublication_select";
				cmd.Parameters.AddWithValue("@PublicationId", publicationId);
				using SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read()) {
					var dto = new DocumentDTO();
					dto.Id = reader.GetInt32("Id");
					dto.Filename = (!reader.IsDBNull("Filename")) ? reader.GetString("Filename") : "";
					dto.MimeType = (!reader.IsDBNull("MimeType")) ? reader.GetString("MimeType") : "";
					dto.Extension = (!reader.IsDBNull("Extension")) ? reader.GetString("Extension") : "";
					dto.Description = (!reader.IsDBNull("Description")) ? reader.GetString("Description") : "";
					dto.UploadedBy = (!reader.IsDBNull("UploadedByUser")) ? reader.GetString("UploadedByUser") : "";
					dto.DocumentType = Enum.Parse<EnumDocumentType>(reader.GetString("DocumentType"));
					dto.Language = Enum.Parse<EnumLanguageCode>(reader.GetString("language"));
					dto.PublicationId = reader.GetInt32("PublicationId");
					if (!reader.IsDBNull("CreatedOn")) {
						dto.CreatedOn = reader.GetDateTime("CreatedOn");
					}
					if (!reader.IsDBNull("File")) {
						dto.File = DocumentDalUtils.ParseStrictByteArray(reader, "File");
					}
					if (!reader.IsDBNull("Thumbnail")) {
						dto.Thumbnail = DocumentDalUtils.ParseStrictByteArray(reader, "Thumbnail");
					}
					list.Add(dto);
				}
			} catch (Exception ex) {
				_log.Error(ex.StackTrace);
			}
			return list;
		}

		public DocumentDTO Insert(DocumentDTO document) {
			conn.Open();
			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.CommandText = "usp_Document_insert";
			cmd.Parameters.AddWithValue(@"Filename", document.Filename);
			cmd.Parameters.AddWithValue(@"MimeType", document.MimeType);
			cmd.Parameters.AddWithValue(@"Extension", document.Extension);
			cmd.Parameters.AddWithValue(@"CreatedOn", document.CreatedOn);
			cmd.Parameters.AddWithValue(@"Description", document.Description);
			cmd.Parameters.AddWithValue(@"UploadedByUser", document.UploadedBy);
			cmd.Parameters.AddWithValue(@"DocumentType", document.DocumentType.ToString());
			cmd.Parameters.AddWithValue(@"Language", document.Language.ToString());
			cmd.Parameters.AddWithValue(@"File", document.File);
			cmd.Parameters.AddWithValue(@"Thumbnail", document.Thumbnail);
			cmd.Parameters.AddWithValue(@"PublicationId", document.PublicationId);

			try {
				var result = cmd.ExecuteNonQuery();
			} catch (Exception ex) {
				_log.Error(ex.Message);
			}
			return document;
		}

		public DocumentDTO Update(DocumentDTO document) {
			conn.Open();
			using SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			cmd.CommandText = "usp_Document_update";
			cmd.Parameters.AddWithValue(@"Id", document.Id);
			cmd.Parameters.AddWithValue(@"Filename", document.Filename);
			cmd.Parameters.AddWithValue(@"MimeType", document.MimeType);
			cmd.Parameters.AddWithValue(@"Extension", document.Extension);
			cmd.Parameters.AddWithValue(@"CreatedOn", document.CreatedOn);
			cmd.Parameters.AddWithValue(@"Description", document.Description);
			cmd.Parameters.AddWithValue(@"UploadedByUser", document.UploadedBy);
			cmd.Parameters.AddWithValue(@"DocumentType", document.DocumentType.ToString());
			cmd.Parameters.AddWithValue(@"Language", document.Language.ToString());
			cmd.Parameters.AddWithValue(@"File", document.File);
			cmd.Parameters.AddWithValue(@"Thumbnail", document.Thumbnail);
			cmd.Parameters.AddWithValue(@"PublicationId", document.PublicationId);

			try {
				var result = cmd.ExecuteNonQuery();
			} catch (Exception ex) {
				_log.Error(ex.Message);
			}
			return document;
		}


	}

	public static class DocumentDalUtils {
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
