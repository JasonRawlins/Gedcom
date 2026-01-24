using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SourceJsonConverter))]
public class SourceRecord : RecordStructureBase
{
    public SourceRecord() : base() { }
    public SourceRecord(Record record) : base(record) { }

    public string AutomatedRecordId => GetValue(Tag.RecordIdNumber);
    public string CallNumber => GetValue(Tag.CallNumber);
    public ChangeDate ChangeDate => First<ChangeDate>(Tag.Change);
    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(Tag.Object);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string RepositoryXref => GetValue(Tag.Repository);
    public NoteStructure SourceDescriptiveTitle => First<NoteStructure>(Tag.Title);
    public NoteStructure SourceFiledByEntry => First<NoteStructure>(Tag.Abbreviation);
    public NoteStructure SourceOriginator => First<NoteStructure>(Tag.Author);
    public NoteStructure SourcePublicationFacts => First<NoteStructure>(Tag.Publication);
    public SourceRecordData SourceRecordData => First<SourceRecordData>(Tag.Data);
    public List<SourceRepositoryCitation> SourceRepositoryCitations => List<SourceRepositoryCitation>(Tag.Repository);
    public NoteStructure TextFromSource => First<NoteStructure>(Tag.Text);
    public List<UserReferenceNumber> UserReferenceNumbers => List<UserReferenceNumber>(Tag.Reference);
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {AutomatedRecordId}";
}

internal class SourceJsonConverter : JsonConverter<SourceRecord>
{
    public override SourceRecord? ReadJson(JsonReader reader, Type objectType, SourceRecord? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, SourceRecord? sourceRecord, JsonSerializer serializer)
    {
        if (sourceRecord == null) throw new ArgumentNullException(nameof(sourceRecord));

        serializer.Serialize(writer, new SourceJson(sourceRecord));
    }
}

public class SourceJson : GedcomJson, IComparable<SourceJson>
{
    public SourceJson(SourceRecord sourceRecord)
    {
        AutomatedRecordId = JsonString(sourceRecord.AutomatedRecordId);
        CallNumber = JsonString(sourceRecord.CallNumber);
        ChangeDate = JsonRecord(new ChangeDateJson(sourceRecord.ChangeDate));
        DescriptiveTitle = JsonString(sourceRecord.SourceDescriptiveTitle.Text);
        FiledByEntry = JsonRecord(new NoteJson(sourceRecord.SourceFiledByEntry));
        IsEmpty = sourceRecord.IsEmpty;
        MultimediaLinks = JsonList(sourceRecord.MultimediaLinks.Select(ml => new MultimediaLinkJson(ml)).ToList());
        Note = JsonString(sourceRecord.NoteStructures.FirstOrDefault()?.Text ?? "");
        Originator = JsonString(sourceRecord.SourceOriginator.Text);
        PublicationFacts = JsonString(sourceRecord.SourcePublicationFacts.Text);
        RecordData = JsonRecord(new SourceDataJson(sourceRecord.SourceRecordData));
        RepositoryCitations = JsonList(sourceRecord.SourceRepositoryCitations.Select(src => new SourceRepositoryCitationJson(src)).ToList());
        RepositoryXref = JsonString(sourceRecord.RepositoryXref);
        TextFromSource = JsonRecord(new NoteJson(sourceRecord.TextFromSource));
        UserReferenceNumbers = JsonList(sourceRecord.UserReferenceNumbers.Select(urn => new UserReferenceNumberJson(urn)).ToList());
        Xref = sourceRecord.Xref;
    }

    public string? AutomatedRecordId { get; set; }
    public string? CallNumber { get; set; }
    public ChangeDateJson? ChangeDate { get; set; }
    public string? DescriptiveTitle { get; set; }
    public NoteJson? FiledByEntry { get; set; }
    public List<MultimediaLinkJson>? MultimediaLinks { get; set; }
    public string? Note { get; set; }
    public string? Originator { get; set; }
    public string? PublicationFacts { get; set; }
    public SourceDataJson? RecordData { get; set; }
    public List<SourceRepositoryCitationJson>? RepositoryCitations { get; set; }
    public string? RepositoryXref { get; set; }
    public NoteJson? TextFromSource { get; set; }
    public List<UserReferenceNumberJson>? UserReferenceNumbers { get; set; }
    public string Xref { get; set; }

    public int CompareTo(SourceJson? other)
    {
        if (other == null) return 1;

        return other.DescriptiveTitle!.CompareTo(other.DescriptiveTitle);
    }
}

#region SOURCE_RECORD p. 27-28
/* 

SOURCE_RECORD:=

0 @<XREF:SOUR>@ SOUR {1:1}
    1 DATA {0:1}
        2 EVEN <EVENTS_RECORDED> {0:M} p.50
            3 DATE <DATE_PERIOD> {0:1} p.46
            3 PLAC <SOURCE_JURISDICTION_PLACE> {0:1} p.62
        2 AGNC <RESPONSIBLE_AGENCY> {0:1} p.60
        2 <<NOTE_STRUCTURE>> {0:M} p.37
    1 AUTH <SOURCE_ORIGINATOR> {0:1} p.62
        2 [CONC|CONT] <SOURCE_ORIGINATOR> {0:M} p.62
    1 TITL <SOURCE_DESCRIPTIVE_TITLE> {0:1} p.62
        2 [CONC|CONT] <SOURCE_DESCRIPTIVE_TITLE> {0:M} p.62
    1 ABBR <SOURCE_FILED_BY_ENTRY> {0:1} p.62
    1 PUBL <SOURCE_PUBLICATION_FACTS> {0:1} p.62
        2 [CONC|CONT] <SOURCE_PUBLICATION_FACTS> {0:M} p.62
    1 TEXT <TEXT_FROM_SOURCE> {0:1} p.63
        2 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M} p.63
    1 <<SOURCE_REPOSITORY_CITATION>> {0:M} p.40
    1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    1 <<CHANGE_DATE>> {0:1} p.31
    1 <<NOTE_STRUCTURE>> {0:M} p.37
    1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26

Source records are used to provide a bibliographic description of the source cited. (See the
<<SOURCE_CITATION>> structure, page 39, which contains the pointer to this source record.)

*/
#endregion