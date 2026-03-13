using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class IndividualDto : GedcomDto
{
    public IndividualDto(IndividualRecord individualRecord)
    {
        Aliases = GetList(individualRecord.Aliases);
        AncestorInterests = GetList(individualRecord.AncestorInterests);
        AncestralFileNumber = GetString(individualRecord.AncestralFileNumber);
        Associations = GetList(individualRecord.AssociationStructures.Select(_as => new AssociationDto(_as)).ToList());
        AutomatedRecordId = GetString(individualRecord.AutomatedRecordId);
        Birth = GetRecord(new EventDto(individualRecord.Birth));
        ChangeDate = GetRecord(new ChangeDateDto(individualRecord.ChangeDate));
        ChildToFamilyLinks = GetList(individualRecord.ChildToFamilyLinks.Select(ctfl => new ChildToFamilyLinkDto(ctfl)).ToList());
        Death = GetRecord(new EventDto(individualRecord.Death));
        DescendantInterests = GetList(individualRecord.DescendantInterests);
        Events = individualRecord.IndividualEventStructures.Select(ies => new EventDto(ies)).ToList();
        Given = GetString(individualRecord.Given);
        IsEmpty = individualRecord.IsEmpty;
        LdsIndividualOrdinances = GetList(individualRecord.LdsIndividualOrdinances.Select(lio => new LdsIndividualOrdinanceDto(lio)).ToList());
        MultimediaLinks = GetList(individualRecord.MultimediaLinks.Select(ml => new MultimediaLinkDto(ml)).ToList());
        Notes = GetList(individualRecord.NoteStructures.Select(ns => ns.Text).ToList());
        PermanentRecordFileNumber = GetString(individualRecord.PermanentRecordFileNumber);
        RestrictionNotice = GetString(individualRecord.RestrictionNotice);
        Sex = GetString(individualRecord.SexValue);
        SourceCitations = GetList(individualRecord.SourceCitations.Select(sc => new SourceCitationDto(sc)).ToList());
        SpouseToFamilyLinks = GetList(individualRecord.SpouseToFamilyLinks.Select(stfl => new SpouseToFamilyLinkDto(stfl)).ToList());
        Submitter = GetString(individualRecord.Submitter);
        Surname = GetString(individualRecord.Surname);
        TreeId = "";
        UserReferenceNumbers = GetList(individualRecord.UserReferenceNumbers.Select(urn => new UserReferenceNumberDto(urn)).ToList());
        Xref = individualRecord.Xref;
    }

    public IndividualDto(IndividualRecord individualRecord, string treeId) : this(individualRecord)
    {
        TreeId = treeId;
    }

    public List<string>? Aliases { get; set; } = [];
    public List<string>? AncestorInterests { get; set; } = [];
    public string? AncestralFileNumber { get; set; }

    public string AncestryLink
    {
        get
        {
            var xrefNumbersOnly = string.IsNullOrEmpty(Xref) ? "" : Xref.Replace("@", "").Replace("I", "");
            return $"https://www.ancestry.com/family-tree/person/tree/{TreeId}/person/{xrefNumbersOnly}/facts";
        }
    }

    public List<AssociationDto>? Associations { get; set; } = [];
    public string? AutomatedRecordId { get; set; }
    public EventDto? Birth { get; set; }
    public ChangeDateDto? ChangeDate { get; set; }
    public List<ChildToFamilyLinkDto>? ChildToFamilyLinks { get; set; } = [];
    public EventDto? Death { get; set; }
    public List<string>? DescendantInterests { get; set; } = [];
    public List<EventDto>? Events { get; set; } = [];
    public string FullName => $"{Given} {Surname}";
    public string? Given { get; set; }
    public List<LdsIndividualOrdinanceDto>? LdsIndividualOrdinances { get; set; } = [];
    public List<MultimediaLinkDto>? MultimediaLinks { get; set; } = [];
    public List<string>? Notes { get; set; } = [];
    public string? PermanentRecordFileNumber { get; set; }
    public string? RestrictionNotice { get; set; }
    public string? Sex { get; set; }
    public List<SourceCitationDto>? SourceCitations { get; set; } = [];
    public List<SpouseToFamilyLinkDto>? SpouseToFamilyLinks { get; set; } = [];
    public string? Submitter { get; set; }
    public string? Surname { get; set; }
    public string? TreeId { get; set; }
    public List<UserReferenceNumberDto>? UserReferenceNumbers { get; set; } = [];
    public string Xref { get; set; }

    public override string ToString() => $"{Given} {Surname}";
}