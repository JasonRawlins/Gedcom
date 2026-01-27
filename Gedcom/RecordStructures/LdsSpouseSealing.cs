using Gedcom.Core;
using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(LdsSpouseSealingJsonConverter))]
public class LdsSpouseSealing : RecordStructureBase
{
    public LdsSpouseSealing() : base() { }
    public LdsSpouseSealing(Record record) : base(record) { }

    public string DateLdsOrdinance => GetValue(Tag.Date);
    public LdsOrdinanceStatus LdsSpouseSealingDateStatus => First<LdsOrdinanceStatus>(Tag.Status);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string PlaceLivingOrdinance => GetValue(Tag.Place);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(Tag.Source);
    public string TempleCode => GetValue(Tag.Temple);

    public override string ToString() => $"{Record.Value}, {TempleCode}, {DateLdsOrdinance}";
}

internal class LdsSpouseSealingJsonConverter : JsonConverter<LdsSpouseSealing>
{
    public override LdsSpouseSealing? ReadJson(JsonReader reader, Type objectType, LdsSpouseSealing? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, LdsSpouseSealing? ldsSpouseSealing, JsonSerializer serializer)
    {
        ArgumentNullException.ThrowIfNull(ldsSpouseSealing);
        serializer.Serialize(writer, new LdsSpouseSealingJson(ldsSpouseSealing));
    }
}

public class LdsSpouseSealingJson(LdsSpouseSealing ldsSpouseSealing) : GedcomJson
{
    public string? DateLdsOrdinance { get; set; } = JsonString(ldsSpouseSealing.DateLdsOrdinance);
    public LdsOrdinanceStatusJson? LdsSpouseSealingDateStatus { get; set; } = JsonRecord(new LdsOrdinanceStatusJson(ldsSpouseSealing.LdsSpouseSealingDateStatus));
    public List<NoteJson>? Notes { get; set; } = JsonList(ldsSpouseSealing.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
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