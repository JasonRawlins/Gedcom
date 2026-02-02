using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(LdsSpouseSealingJsonConverter))]
public class LdsSpouseSealing : RecordStructureBase
{
    public LdsSpouseSealing() : base() { }
    public LdsSpouseSealing(Record record) : base(record) { }

    private string? _dateLdsOrdinance = null;
    public string DateLdsOrdinance => _dateLdsOrdinance ??= GetValue(Tag.Date);

    private LdsOrdinanceStatus? _ldsSpouseSealingDateStatus = null;
    public LdsOrdinanceStatus LdsSpouseSealingDateStatus => _ldsSpouseSealingDateStatus ??= First<LdsOrdinanceStatus>(Tag.Status);

    private List<NoteStructure>? _noteStructures = null;
    public List<NoteStructure> NoteStructures => _noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? _placeLivingOrdinance = null;
    public string PlaceLivingOrdinance => _placeLivingOrdinance ??= GetValue(Tag.Place);

    private List<SourceCitation>? _sourceCitations = null;
    public List<SourceCitation> SourceCitations => _sourceCitations ??= List<SourceCitation>(Tag.Source);

    private string? _templeCode = null;
    public string TempleCode => _templeCode ??= GetValue(Tag.Temple);

    public override string ToString() => $"{Record.Value}, {TempleCode}, {DateLdsOrdinance}";
}

internal sealed class LdsSpouseSealingJsonConverter : JsonConverter<LdsSpouseSealing>
{
    public override LdsSpouseSealing? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, LdsSpouseSealing value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new LdsSpouseSealingJson(value), options);
    }
}

public class LdsSpouseSealingJson(LdsSpouseSealing ldsSpouseSealing) : GedcomJson
{
    public string? DateLdsOrdinance { get; set; } = JsonString(ldsSpouseSealing.DateLdsOrdinance);
    public LdsOrdinanceStatusJson? LdsSpouseSealingDateStatus { get; set; } = JsonRecord(new LdsOrdinanceStatusJson(ldsSpouseSealing.LdsSpouseSealingDateStatus));
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(ldsSpouseSealing.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
    public string? PlaceLivingOrdinance { get; set; } = JsonString(ldsSpouseSealing.PlaceLivingOrdinance);
    public List<SourceCitationJson>? SourceCitations { get; set; } = JsonList(ldsSpouseSealing.SourceCitations.Select(sc => new SourceCitationJson(sc)).ToList());
    public string? TempleCode { get; set; } = JsonString(ldsSpouseSealing.TempleCode);
    public override string ToString() => $"{TempleCode}";
}

#region LDS_SPOUSE_SEALING p. 36
/* 

LDS_SPOUSE_SEALING:=

n SLGS {1:1}
    +1 DATE <DATE_LDS_ORD> {0:1} p.46
    +1 TEMP <TEMPLE_CODE> {0:1} p.63
    +1 PLAC <PLACE_LIVING_ORDINANCE> {0:1} p.58
    +1 STAT <LDS_SPOUSE_SEALING_DATE_STATUS> {0:1} p.52
        +2 DATE <CHANGE_DATE> {1:1} p.44
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<SOURCE_CITATION>> {0:M}

*/
#endregion