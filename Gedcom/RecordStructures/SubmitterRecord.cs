using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SubmitterJsonConverter))]
public class SubmitterRecord : RecordStructureBase
{
    public SubmitterRecord() : base() { }
    public SubmitterRecord(Record record) : base(record) { }

    public AddressStructure AddressStructure => First<AddressStructure>(Tag.Address);
    public string AutomatedRecordId => GetValue(Tag.RecordIdNumber);
    public GedcomDate ChangeDate => First<GedcomDate>(Tag.Change);
    public List<string> LanguagePreferences => List(r => r.Tag.Equals(Tag.Language)).Select(r => r.Value).ToList();
    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(Tag.Media);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string SubmitterName => GetValue(Tag.Name);
    public string SubmitterRegisteredRfn => GetValue(Tag.RecordFileNumber);

    public override string ToString() => $"{Record.Value}, {SubmitterName}";
}

internal class SubmitterJsonConverter : JsonConverter<SubmitterRecord>
{
    public override SubmitterRecord? ReadJson(JsonReader reader, Type objectType, SubmitterRecord? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, SubmitterRecord? submitterRecord, JsonSerializer serializer)
    {
        if (submitterRecord == null) throw new ArgumentNullException(nameof(submitterRecord));

        serializer.Serialize(writer, new SubmitterJson(submitterRecord));
    }
}

public class SubmitterJson(SubmitterRecord submitterRecord) : GedcomJson
{
    public AddressJson? Address { get; set; } = JsonRecord(new AddressJson(submitterRecord.AddressStructure));
    public string? AutomatedRecordId { get; set; } = JsonString(submitterRecord.AutomatedRecordId);
    public GedcomDateJson? ChangeDate { get; set; } = JsonRecord(new GedcomDateJson(submitterRecord.ChangeDate));
    public List<string>? LanguagePreferences { get; set; } = JsonList(submitterRecord.LanguagePreferences);
    public List<MultimediaLinkJson>? MultimediaLinks { get; set; } = JsonList(submitterRecord.MultimediaLinks.Select(ml => new MultimediaLinkJson(ml)).ToList());
    public List<NoteJson>? Notes { get; set; } = JsonList(submitterRecord.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
    public string? SubmitterName { get; set; } = JsonString(submitterRecord.SubmitterName);
    public string? SubmitterRegisteredReferenceNumber { get; set; } = JsonString(submitterRecord.SubmitterRegisteredRfn);
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