using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(SourceRecordJsonConverter))]
public class SourceRecord : RecordStructureBase
{
    internal SourceRecord() : base() { }
    public SourceRecord(Record record) : base(record) { }

    public SourceRecordData SourceRecordData => First<SourceRecordData>(C.DATA);
    public NoteStructure SourceOriginator => First<NoteStructure>(C.AUTH);
    public NoteStructure SourceDescriptiveTitle => First<NoteStructure>(C.TITL);
    public NoteStructure SourceFiledByEntry => First<NoteStructure>(C.ABBR);
    public NoteStructure SourcePublicationFacts => First<NoteStructure>(C.PUBL);
    public NoteStructure TextFromSource => First<NoteStructure>(C.TEXT);
    public List<SourceRepositoryCitation> SourceRepositoryCitations => List<SourceRepositoryCitation>("");
    public List<UserReferenceNumber> UserReferenceNumbers => List<UserReferenceNumber>(C.REFN);
    public string AutomatedRecordId => _(C.RIN);
    public ChangeDate ChangeDate => First<ChangeDate>(C.CHAN);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(C.OBJE);
}

internal class SourceRecordJsonConverter : JsonConverter<SourceRecord>
{
    public override SourceRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, SourceRecord sourceRecord, JsonSerializerOptions options)
    {
        var sourceRecordJson = new SourceRecordJson(sourceRecord);
        JsonSerializer.Serialize(writer, sourceRecordJson, sourceRecordJson.GetType(), options);
    }
}

internal class SourceRecordJson : GedcomJson
{
    public SourceRecordJson(SourceRecord sourceRecord)
    {
        SourceRecordData = JsonRecord(sourceRecord.SourceRecordData);
        SourceOriginator = JsonRecord(sourceRecord.SourceOriginator);
        SourceDescriptiveTitle = JsonRecord(sourceRecord.SourceDescriptiveTitle);
        SourceFiledByEntry = JsonRecord(sourceRecord.SourceFiledByEntry);
        SourcePublicationFacts = JsonRecord(sourceRecord.SourcePublicationFacts);
        TextFromSource = JsonRecord(sourceRecord.TextFromSource);
        SourceRepositoryCitations = JsonList(sourceRecord.SourceRepositoryCitations);
        UserReferenceNumbers = JsonList(sourceRecord.UserReferenceNumbers);
        AutomatedRecordId = JsonString(sourceRecord.AutomatedRecordId);
        ChangeDate = JsonRecord(sourceRecord.ChangeDate);
        NoteStructures = JsonList(sourceRecord.NoteStructures);
        MultimediaLinks = JsonList(sourceRecord.MultimediaLinks);
    }

    public SourceRecordData? SourceRecordData { get; set; }
    public NoteStructure? SourceOriginator { get; set; }
    public NoteStructure? SourceDescriptiveTitle { get; set; }
    public NoteStructure? SourceFiledByEntry { get; set; }
    public NoteStructure? SourcePublicationFacts { get; set; }
    public NoteStructure? TextFromSource { get; set; }
    public List<SourceRepositoryCitation>? SourceRepositoryCitations { get; set; }
    public List<UserReferenceNumber>? UserReferenceNumbers { get; set; }
    public string? AutomatedRecordId { get; set; }
    public ChangeDate? ChangeDate { get; set; }
    public List<NoteStructure>? NoteStructures { get; set; }
    public List<MultimediaLink>? MultimediaLinks { get; set; }
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