using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(LdsSpouseSealingJsonConverter))]
public class LdsSpouseSealing : RecordStructureBase
{
    public LdsSpouseSealing() : base() { }
    public LdsSpouseSealing(Record record) : base(record) { }

    public string DateLdsOrdinance => _(C.DATE);
    public LdsOrdinanceStatus LdsSpouseSealingDateStatus => First<LdsOrdinanceStatus>(C.STAT);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public string PlaceLivingOrdinance => _(C.PLAC);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(C.SOUR);
    public string TempleCode => _(C.TEMP);

    public override string ToString() => $"{Record.Value}, {TempleCode}, {DateLdsOrdinance}";
}

internal class LdsSpouseSealingJsonConverter : JsonConverter<LdsSpouseSealing>
{
    public override LdsSpouseSealing? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, LdsSpouseSealing ldsSpouseSealing, JsonSerializerOptions options)
    {
        var ldsSpouseSealingJson = new LdsSpouseSealingJson(ldsSpouseSealing);
        JsonSerializer.Serialize(writer, ldsSpouseSealingJson, ldsSpouseSealingJson.GetType(), options);
    }
}

internal class LdsSpouseSealingJson : GedcomJson
{
    public LdsSpouseSealingJson(LdsSpouseSealing ldsSpouseSealing)
    {
        DateLdsOrdinance = JsonString(ldsSpouseSealing.DateLdsOrdinance);
        LdsSpouseSealingDateStatus = JsonRecord(ldsSpouseSealing.LdsSpouseSealingDateStatus);
        NoteStructures = JsonList(ldsSpouseSealing.NoteStructures);
        PlaceLivingOrdinance = JsonString(ldsSpouseSealing.PlaceLivingOrdinance);
        SourceCitations = JsonList(ldsSpouseSealing.SourceCitations);
        TempleCode = JsonString(ldsSpouseSealing.TempleCode);
    }

    public string? DateLdsOrdinance { get; set; }
    public LdsOrdinanceStatus? LdsSpouseSealingDateStatus { get; set; }
    public List<NoteStructure>? NoteStructures { get; set; }
    public string? PlaceLivingOrdinance { get; set; }
    public List<SourceCitation>? SourceCitations { get; set; }
    public string? TempleCode { get; set; }
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