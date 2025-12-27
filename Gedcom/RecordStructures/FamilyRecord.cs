using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(FamilyJsonConverter))]
public class FamilyRecord : RecordStructureBase
{
    public FamilyRecord() : base() { }
    public FamilyRecord(Record record) : base(record) { }

    public string AdoptedByWhichParent => GetValue(Tag.Adoption);
    public string AutomatedRecordNumber => GetValue(Tag.RecordIdNumber);
    public ChangeDate ChangeDate => First<ChangeDate>(Tag.Change);
    public List<string> Children => List(r => r.Tag.Equals(Tag.Child)).Select(r => r.Value).ToList();
    public string CountOfChildren => GetValue(Tag.ChildrenCount);
    public List<FamilyEventStructure> FamilyEventStructures => List<FamilyEventStructure>(Tag.Family);
    public string Husband => GetValue(Tag.Husband);
    // +1 <<LDS_SPOUSE_SEALING>> {0:M} p.36
    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(Tag.Object);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string RestrictionNotice => GetValue(Tag.Restriction);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(Tag.Source);
    public string Submitter => GetValue(Tag.Submitter);
    public List<UserReferenceNumber> UserReferenceNumbers => List<UserReferenceNumber>(Tag.Reference);
    public string Wife => GetValue(Tag.Wife);
    public string Xref => Record.Value;

    public override string ToString()
    {
        var childrenCountText = Children.Count == 1 ? "child" : "children";

        return $"{Record.Value}, {Husband} and {Wife} with {Children.Count} {childrenCountText}";
    }
}

internal class FamilyJsonConverter : JsonConverter<FamilyRecord>
{
    public override FamilyRecord? ReadJson(JsonReader reader, Type objectType, FamilyRecord? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, FamilyRecord? familyRecord, JsonSerializer serializer)
    {
        if (familyRecord == null) throw new ArgumentNullException(nameof(familyRecord));

        serializer.Serialize(writer, new FamilyJson(familyRecord));
    }
}

internal class FamilyJson : GedcomJson
{
    public FamilyJson(FamilyRecord familyRecord)
    {
        AdoptedByWhichParent = JsonString(familyRecord.AdoptedByWhichParent);
        AutomatedRecordNumber = JsonString(familyRecord.AutomatedRecordNumber);
        ChangeDate = JsonRecord(familyRecord.ChangeDate);
        Children = JsonList(familyRecord.Children);
        CountOfChildren = JsonString(familyRecord.CountOfChildren);
        FamilyEvents = JsonList(familyRecord.FamilyEventStructures);
        Husband = JsonString(familyRecord.Husband);
        // +1 <<LDS_SPOUSE_SEALING>> {0:M} p.36
        MultimediaLinks = JsonList(familyRecord.MultimediaLinks);
        Notes = JsonList(familyRecord.NoteStructures);
        RestrictionNotice = JsonString(familyRecord.RestrictionNotice);
        SourceCitations = JsonList(familyRecord.SourceCitations);
        Submitter = JsonString(familyRecord.Submitter);
        UserReferenceNumbers = JsonList(familyRecord.UserReferenceNumbers);
        Wife = JsonString(familyRecord.Wife);
        Xref = familyRecord.Xref;

    }

    public string? AdoptedByWhichParent { get; set; }
    public string? AutomatedRecordNumber { get; set; }
    public ChangeDate? ChangeDate { get; set; }
    public List<string>? Children { get; set; }
    public string? CountOfChildren { get; set; }
    public List<FamilyEventStructure>? FamilyEvents { get; set; }
    public string? Husband { get; set; }
    // +1 <<LDS_SPOUSE_SEALING>> {0:M} p.36
    public List<MultimediaLink>? MultimediaLinks { get; set; }
    public List<NoteStructure>? Notes { get; set; }
    public string? RestrictionNotice { get; set; }
    public List<SourceCitation>? SourceCitations { get; set; }
    public string? Submitter { get; set; }
    public List<UserReferenceNumber>? UserReferenceNumbers { get; set; }
    public string? Wife { get; set; }
    public string Xref { get; set; }
}

#region FAM_RECORD (FAM) p. 24
/*

FAM_RECORD:=

n @<XREF:FAM>@ FAM {1:1}
    +1 RESN <RESTRICTION_NOTICE> {0:1) p.60
    +1 <<FAMILY_EVENT_STRUCTURE>> {0:M} p.32
    +1 HUSB @<XREF:INDI>@ {0:1} p.25
    +1 WIFE @<XREF:INDI>@ {0:1} p.25
    +1 CHIL @<XREF:INDI>@ {0:M} p.25
    +1 NCHI <COUNT_OF_CHILDREN> {0:1} p.44
    +1 SUBM @<XREF:SUBM>@ {0:M} p.28
    +1 <<LDS_SPOUSE_SEALING>> {0:M} p.36
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<CHANGE_DATE>> {0:1} p.31
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<SOURCE_CITATION>> {0:M} p.39
    +1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26

The FAMily record is used to record marriages, common law marriages, and family unions caused by
two people becoming the parents of a child. There can be no more than one HUSB/father and one
WIFE/mother listed in each FAM_RECORD. If, for example, a man participated in more than one
family union, then he would appear in more than one FAM_RECORD. The family record structure
assumes that the HUSB/father is male and WIFE/mother is female.

The preferred order of the CHILdren pointers within a FAMily structure is chronological by birth.

 */
#endregion