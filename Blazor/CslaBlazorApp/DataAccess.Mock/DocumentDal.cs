using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using DataAccess;   

namespace DataAccess.Mock;

public class DocumentDal : IDocumentDal {

    private static readonly List<DocumentDTO> _documentTable = new List<DocumentDTO>{
        new DocumentDTO {
            Id=1,
            PublicationId=1,
            MimeType="application/pdf",
            Extension="PDF",
            CreatedOn=DateTime.Now,
            Description="Test",
            UploadedBy="Dgucc",
            DocumentType=EnumDocumentType.Report,
            Language=EnumLanguageCode.French,
            File=File.ReadAllBytes(@"../TEMP/Publication_01_FR.pdf"),
	        Thumbnail=File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory,  @"../TEMP/cover/Publication_01_FR_cover.jpg"))
        },
        new DocumentDTO {
            Id=2,
            PublicationId=1,
            MimeType="application/pdf",
			Extension="PDF",
			CreatedOn=DateTime.Now,
			Description="Test",
			UploadedBy="Dgucc",
			DocumentType=EnumDocumentType.Report,
            Language=EnumLanguageCode.Dutch,
			File=File.ReadAllBytes(@"../TEMP/Publication_01_NL.pdf"),
			Thumbnail=File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory,  @"../TEMP/cover/Publication_01_NL_cover.jpg"))
        },
        new DocumentDTO {
            Id=3,
            PublicationId=2,
            MimeType="application/pdf",
			Extension="PDF",
			CreatedOn=DateTime.Now,
			Description="Test",
			UploadedBy="Dgucc",
			DocumentType=EnumDocumentType.Report,
            Language=EnumLanguageCode.French,
			File=File.ReadAllBytes(@"../TEMP/Publication_02_FR.pdf"),
			Thumbnail=File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory,  @"../TEMP/cover/Publication_02_FR_cover.jpg"))
        },
        new DocumentDTO {
            Id=7,
            PublicationId=2,
			MimeType="application/pdf",
			Extension="PDF",
			CreatedOn=DateTime.Now,
			Description="Test",
			UploadedBy="Dgucc",
			DocumentType=EnumDocumentType.Report,
            Language=EnumLanguageCode.Dutch,
			File=File.ReadAllBytes(@"../TEMP/Publication_02_NL.pdf"),
			Thumbnail=File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory,  @"../TEMP/cover/Publication_02_NL_cover.jpg"))
        },
        new DocumentDTO {
            Id=8,
            PublicationId=3,
			MimeType="application/pdf",
			Extension="PDF",
			CreatedOn=DateTime.Now,
			Description="Test",
			UploadedBy="Dgucc",
			DocumentType=EnumDocumentType.Report,
            Language=EnumLanguageCode.Dutch,
			File=File.ReadAllBytes(@"../TEMP/Publication_03_NL.pdf"),
			Thumbnail=File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory,  @"../TEMP/cover/Publication_03_NL_cover.jpg"))
        },
        new DocumentDTO {
            Id=9,
            PublicationId=4,
			MimeType="application/pdf",
			Extension="PDF",
			CreatedOn=DateTime.Now,
			Description="Test",
			UploadedBy="Dgucc",
            DocumentType=EnumDocumentType.Report,
            Language=EnumLanguageCode.Dutch,
			File=File.ReadAllBytes(@"../TEMP/Publication_04_NL.pdf"),
			Thumbnail=File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"../TEMP/cover/Publication_04_NL_cover.jpg"))
		}
    };

    public bool Exists(int id){ 
        var document = _documentTable.Where(p => p.Id == id).FirstOrDefault();
        return !(document == null);
    }

	public DocumentDTO Get(int id){
        Console.WriteLine("[DAL] Get(id:{0})", id);
		var document = _documentTable.Where(p => p.Id == id).FirstOrDefault();
        if (document != null) {
            return document;
        } else {
            throw new KeyNotFoundException($"Id {id}");
        }
    }
    
    public List<DocumentDTO> Get(){
		return _documentTable.Where(r => true).ToList();
    }

    public List<DocumentDTO> GetByPublication(int publicationId){
        var documents = _documentTable.Where(p => p.PublicationId == publicationId).ToList();
        Console.WriteLine(XmlUtils.SerializeObject(documents));
		return _documentTable.Where(p => p.PublicationId == publicationId).ToList();
	}

    public DocumentDTO Insert(DocumentDTO document) {
        if (Exists(document.Id))
            throw new InvalidOperationException($"Key exists {document.Id}");
        lock (_documentTable) {
            int lastId = _documentTable.Max(m => m.Id);
            document.Id = ++lastId;
            _documentTable.Add(document);
        }
        return document;
    }

	public DocumentDTO Update(DocumentDTO document){
        Console.WriteLine("[DAL] Update()");
        lock (_documentTable) {
            var old = Get(document.Id);
            old.PublicationId = document.PublicationId;
            old.MimeType = document.MimeType;
            old.Extension = document.Extension;
            old.CreatedOn = document.CreatedOn;
            old.Description = document.Description;
            old.UploadedBy = document.UploadedBy;
            old.DocumentType = document.DocumentType;
            old.Language = document.Language;
            old.File = document.File;
            old.Thumbnail = document.Thumbnail;
            return old;
        }
    }

    public bool Delete(int id){
        Console.WriteLine("[DAL] Delete(Document id:{0})", id);
		var document = _documentTable.Where(p => p.Id == id).FirstOrDefault();
        if (document != null) {
            lock (_documentTable) {
                _documentTable.Remove(document);
                return true;
            }
        } else {
            return false;
        }            
    }

}

public static class XmlUtils {
	public static string SerializeObject<T>(this T objToSerialize) {
		XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
		ns.Add("", "");
		MemoryStream ms = new MemoryStream();

		XmlWriterSettings settings = new XmlWriterSettings();
		settings.OmitXmlDeclaration = true;
		settings.Encoding = new UnicodeEncoding(bigEndian: false, byteOrderMark: false);
		XmlWriter writer = XmlWriter.Create(ms, settings);

		XmlSerializer serializer = new XmlSerializer(objToSerialize.GetType());
		serializer.Serialize(writer, objToSerialize, ns);

		return Encoding.Unicode.GetString(ms.ToArray());
	}
}