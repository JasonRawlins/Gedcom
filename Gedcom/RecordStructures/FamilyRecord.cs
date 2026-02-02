using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(FamilyRecordJsonConverter))]
public class FamilyRecord : RecordStructureBase
{
    public FamilyRecord() : base() { }
    public FamilyRecord(Record record) : base(record) { }

    private string? adoptedByWhichParent = null;
    public string AdoptedByWhichParent => adoptedByWhichParent ??= GetValue(Tag.Adoption);

    private string? automatedRecordNumber = null;
    public string AutomatedRecordNumber => automatedRecordNumber ??= GetValue(Tag.RecordIdNumber);

    private ChangeDate? changeDate = null;
    public ChangeDate ChangeDate => changeDate ??= First<ChangeDate>(Tag.Change);

    private List<string>? children = null;
    public List<string> Children => children ??= GetStringList(Tag.Child);

    private string? countOfChildren = null;
    public string CountOfChildren => countOfChildren ??= GetValue(Tag.ChildrenCount);

    private FamilyEventStructure? divorce = null;
    public FamilyEventStructure Divorce => divorce ??= First<FamilyEventStructure>(Tag.Divorce);

    private List<EventStructure>? familyEventStructures = null;
    public List<EventStructure> FamilyEventStructures => familyEventStructures ??= [.. List(FamilyEventStructure.IsFamilyEventStructure).Select(r => new EventStructure(r))];

    private string? husband = null;
    public string Husband => husband ??= GetValue(Tag.Husband);
    // +1 <<LDS_SPOUSE_SEALING>> {0:M} p.36

    private FamilyEventStructure? marriage = null;
    public FamilyEventStructure Marriage => marriage ??= First<FamilyEventStructure>(Tag.Marriage);

    private List<MultimediaLink>? multimediaLinks = null;
    public List<MultimediaLink> MultimediaLinks => multimediaLinks ??= List<MultimediaLink>(Tag.Object);

    private List<NoteStructure>? noteStructures = null;
    public List<NoteStructure> NoteStructures => noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? restrictionNotice = null;
    public string RestrictionNotice => restrictionNotice ??= GetValue(Tag.Restriction);

    private List<SourceCitation>? sourceCitations = null;
    public List<SourceCitation> SourceCitations => sourceCitations ??= List<SourceCitation>(Tag.Source);

    private string? submitter = null;
    public string Submitter => submitter ??= GetValue(Tag.Submitter);

    private List<UserReferenceNumber>? userReferenceNumbers = null;
    public List<UserReferenceNumber> UserReferenceNumbers => userReferenceNumbers ??= List<UserReferenceNumber>(Tag.Reference);

    private string? wife = null;
    public string Wife => wife ??= GetValue(Tag.Wife);
   
    public string Xref => Record.Value;

    public override string ToString()
    {
        var childrenCountText = Children.Count == 1 ? "child" : "children";

        return $"{Record.Value}, {Husband} and {Wife} with {Children.Count} {childrenCountText}";
    }
}

internal sealed class FamilyRecordJsonConverter : JsonConverter<FamilyRecord>
{
    public override FamilyRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, FamilyRecord value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new FamilyJson(value), options);
    }
}

public class FamilyJson(FamilyRecord familyRecord) : GedcomJson
{
    public string? AdoptedByWhichParent { get; set; } = JsonString(familyRecord.AdoptedByWhichParent);
    public string? AutomatedRecordNumber { get; set; } = JsonString(familyRecord.AutomatedRecordNumber);
    public ChangeDateJson? ChangeDate { get; set; } = JsonRecord(new ChangeDateJson(familyRecord.ChangeDate));
    public List<string>? Children { get; set; } = JsonList(familyRecord.Children);
    public string? CountOfChildren { get; set; } = JsonString(familyRecord.CountOfChildren);
    public EventJson? Divorce { get; set; } = JsonRecord(new EventJson(familyRecord.Divorce));
    public List<EventJson>? Events { get; set; } = JsonList(familyRecord.FamilyEventStructures.Select(fes => new EventJson(fes)).ToList());
    public string? Husband { get; set; } = JsonString(familyRecord.Husband);
    // +1 <<LDS_SPOUSE_SEALING>> {0:M} p.36
    public EventJson? Marriage { get; set; } = JsonRecord(new EventJson(familyRecord.Marriage));
    public List<MultimediaLinkJson>? MultimediaLinks { get; set; } = JsonList(familyRecord.MultimediaLinks.Select(ml => new MultimediaLinkJson(ml)).ToList());
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(familyRecord.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
    public string? RestrictionNotice { get; set; } = JsonString(familyRecord.RestrictionNotice);
    public List<SourceCitationJson>? SourceCitations { get; set; } = JsonList(familyRecord.SourceCitations.Select(sc => new SourceCitationJson(sc)).ToList());
    public string? Submitter { get; set; } = JsonString(familyRecord.Submitter);
    public List<UserReferenceNumberJson>? UserReferenceNumbers { get; set; } = JsonList(familyRecord.UserReferenceNumbers.Select(urn => new UserReferenceNumberJson(urn)).ToList());
    public string? Wife { get; set; } = JsonString(familyRecord.Wife);
    public string Xref { get; set; } = familyRecord.Xref;
    public override string ToString()
    {
        var childrenCountText = Children?.Count == 1 ? "child" : "children";

        return $"({Xref}) {Husband} and {Wife} with {Children?.Count} {childrenCountText}";
    }
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