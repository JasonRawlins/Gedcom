using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SubmitterRecordJsonConverter))]
public class SubmitterRecord : RecordStructureBase
{
    public SubmitterRecord() : base() { }
    public SubmitterRecord(Record record) : base(record) { }

    private AddressStructure? addressStructure = null;
    public AddressStructure AddressStructure => addressStructure ??= First<AddressStructure>(Tag.Address);
   
    private string? automatedRecordId = null;
    public string AutomatedRecordId => automatedRecordId ??= GetValue(Tag.RecordIdNumber);

    private GedcomDate? changeDate = null;
    public GedcomDate ChangeDate => changeDate ??= First<GedcomDate>(Tag.Change);

    private List<string>? languagePreferences = null;
    public List<string> LanguagePreferences => languagePreferences ??= GetStringList(Tag.Language);

    private List<MultimediaLink>? multimediaLinks = null;
    public List<MultimediaLink> MultimediaLinks => multimediaLinks ??= List<MultimediaLink>(Tag.Media);

    private List<NoteStructure>? noteStructures = null;
    public List<NoteStructure> NoteStructures => noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? submitterName = null;
    public string SubmitterName => submitterName ??= GetValue(Tag.Name);

    private string? submitterRegisteredRfn = null;
    public string SubmitterRegisteredRfn => submitterRegisteredRfn ??= GetValue(Tag.RecordFileNumber);

    public override string ToString() => $"{Record.Value}, {SubmitterName}";
}

internal sealed class SubmitterRecordJsonConverter : JsonConverter<SubmitterRecord>
{
    public override SubmitterRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, SubmitterRecord value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new SubmitterJson(value), options);
    }
}

public class SubmitterJson(SubmitterRecord submitterRecord) : GedcomJson
{
    public AddressJson? Address { get; set; } = JsonRecord(new AddressJson(submitterRecord.AddressStructure));
    public string? AutomatedRecordId { get; set; } = JsonString(submitterRecord.AutomatedRecordId);
    public GedcomDateJson? ChangeDate { get; set; } = JsonRecord(new GedcomDateJson(submitterRecord.ChangeDate));
    public List<string>? LanguagePreferences { get; set; } = JsonList(submitterRecord.LanguagePreferences);
    public List<MultimediaLinkJson>? MultimediaLinks { get; set; } = JsonList(submitterRecord.MultimediaLinks.Select(ml => new MultimediaLinkJson(ml)).ToList());
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(submitterRecord.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
    public string? SubmitterName { get; set; } = JsonString(submitterRecord.SubmitterName);
    public string? SubmitterRegisteredReferenceNumber { get; set; } = JsonString(submitterRecord.SubmitterRegisteredRfn);

    public override string ToString() => $"{SubmitterName}";
}

#region SUBMITTER_RECORD p. 28-29
/* 

SUBMITTER_RECORD:=

n @<XREF:SUBM>@ SUBM {1:1}
    +1 NAME <SUBMITTER_NAME> {1:1} p.63
    +1 <<ADDRESS_STRUCTURE>> {0:1}* p.31
    +1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26
    +1 LANG <LANGUAGE_PREFERENCE> {0:3} p.51
    +1 RFN <SUBMITTER_REGISTERED_RFN> {0:1} p.63
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<CHANGE_DATE>> {0:1} p.31

The submitter record identifies an individual or organization that contributed information contained
in the GEDCOM transmission. All records in the transmission are assumed to be submitted by the
SUBMITTER referenced in the HEADer, unless a SUBMitter reference inside a specific record
points at a different SUBMITTER record.

* Note: submissions to the ancestral file require the name and address of the submitter.

*/
#endregion