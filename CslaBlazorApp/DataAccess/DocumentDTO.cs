using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess {
    public class DocumentDTO {
        public int Id { get; set; }

        [MaxLength(255)]
        public string? Filename { get; set; }
        
        [MaxLength(100)]
        public string? MimeType { get; set; }

        [MaxLength(10)]
        public string? Extension { get; set; }

        public DateTime CreatedOn { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [StringLength(255)]
        public string? UploadedBy { get; set; }

        public byte[] File { get; set; }

        public EnumDocumentType? DocumentType { get; set; }
        public EnumLanguageCode? Language { get; set; }

        public int PublicationId { get; set; }

    }
    
}
