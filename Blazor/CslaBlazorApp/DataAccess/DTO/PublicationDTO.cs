using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess;

public class PublicationDTO
{
    public int Id { get; set; }
    //public string? LegislativeLevel { get; set; }

    //public string? PublicationType { get; set; }

    //public string? ApprovedBy { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public DateTime? PublishDate { get; set; }

    public string? RequestorEmail { get; set; }

    public string? TitleFr { get; set; }

    public string? TitleNl { get; set; }

    public string? TitleEn { get; set; }

    public string? TitleDe { get; set; }

    //public string? HeaderFr { get; set; }

    //public string? HeaderNl { get; set; }

    //public string? HeaderEn { get; set; }

    //public string? HeaderDe { get; set; }

    public byte[]? Cover { get; set; }

    //public List<EnumTopicType>? Topics { get; set; }

    public List<DocumentDTO>? Documents { get; set; }

}
