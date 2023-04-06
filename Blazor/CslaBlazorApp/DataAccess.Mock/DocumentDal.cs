using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccess.Mock;
    
public class DocumentDal : IDocumentDal {

    private static readonly List<DocumentDTO> _documentTable = new List<DocumentDTO>{
        new DocumentDTO {
            Id=1,
            PublicationId=1,
            DocumentType=EnumDocumentType.Report,
            Language=EnumLanguageCode.French,
            File=File.ReadAllBytes(@"../TEMP/Publication_01_FR.pdf"),
	        Thumbnail=File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory,  @"../TEMP/cover/Publication_01_FR_cover.jpg"))
        },
        new DocumentDTO {
            Id=2,
            PublicationId=1,
            DocumentType=EnumDocumentType.Report,
            Language=EnumLanguageCode.Dutch,
			File=File.ReadAllBytes(@"../TEMP/Publication_01_NL.pdf"),
			Thumbnail=File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory,  @"../TEMP/cover/Publication_01_NL_cover.jpg"))
        },
        new DocumentDTO {
            Id=3,
            PublicationId=2,
            DocumentType=EnumDocumentType.Report,
            Language=EnumLanguageCode.French,
			File=File.ReadAllBytes(@"../TEMP/Publication_02_FR.pdf"),
			Thumbnail=File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory,  @"../TEMP/cover/Publication_02_FR_cover.jpg"))
        },
        new DocumentDTO {
            Id=7,
            PublicationId=2,
            DocumentType=EnumDocumentType.Report,
            Language=EnumLanguageCode.Dutch,
			File=File.ReadAllBytes(@"../TEMP/Publication_02_NL.pdf"),
			Thumbnail=File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory,  @"../TEMP/cover/Publication_02_NL_cover.jpg"))
        },
        new DocumentDTO {
            Id=8,
            PublicationId=3,
            DocumentType=EnumDocumentType.Report,
            Language=EnumLanguageCode.Dutch,
			File=File.ReadAllBytes(@"../TEMP/Publication_03_NL.pdf"),
			Thumbnail=File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory,  @"../TEMP/cover/Publication_03_NL_cover.jpg"))
        },
        new DocumentDTO {
            Id=9,
            PublicationId=4,
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

    public List<DocumentDTO> GetByPublication(int documentId){
        return new List<DocumentDTO>();
    }

    public DocumentDTO Insert(DocumentDTO document){
        return new DocumentDTO();
    }
    
    public DocumentDTO Update(DocumentDTO document){
        Console.WriteLine("[DAL] Update()");
        lock (_documentTable) {
            var old = Get(document.Id);
            old.PublicationId = document.PublicationId;
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