using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess {
    public class Document {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Filename { get; set; }
        
        [MaxLength(100)]
        public string MimeType { get; set; }

        [MaxLength(10)]
        public string Extension { get; set; }

        public DateTime CreatedOn { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string UploadedBy { get; set; }

        public byte[] File { get; set; }

        public EnumDocumentType DocumentType { get; set; }
        public EnumLanguageCode Language { get; set; }

        public PublicationDTO Publication { get; set; }

    }

    public enum EnumDocumentType {
        [Display(Name = "Rapport")]
        Report = 1,

        [Display(Name = "Synthèse")]
        Summary = 2,

        [Display(Name = "Communiqué de presse")]
        PressRelease = 3,

        [Display(Name = "Abstrait")]
        Abstract = 4,

        [Display(Name = "Conclusions et recommandations")]
        ConclusionsAndRecommendations = 5,

        [Display(Name = "Annexe")]
        Attachment = 6,

        [Display(Name = "Divers")]
        Misc = 7,

        [Display(Name = "Photo de couverture")]
        CoverPhoto = 8
    }

    public enum EnumLanguageCode {
        French = 1,
        Dutch = 2,
        German = 3,
        English = 4
    }
}
