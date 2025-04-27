using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(LdsIndividualOrdinanceJsonConverter))]
public class LdsIndividualOrdinance : RecordStructureBase
{
    public LdsIndividualOrdinance() : base() { }
    public LdsIndividualOrdinance(Record record) : base(record) { }

    public string DateLdsOrdinance => _(C.DATE);
    public LdsOrdinanceStatus LdsBaptismDateStatus => First<LdsOrdinanceStatus>(C.STAT);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public string PlaceLivingOrdinance => _(C.PLAC);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(C.SOUR);
    public string TempleCode => _(C.TEMP);

    public override string ToString() => $"{Record.Value}, {TempleCode}, {PlaceLivingOrdinance}";
}

internal class LdsIndividualOrdinanceJsonConverter : JsonConverter<LdsIndividualOrdinance>
{
    public override LdsIndividualOrdinance? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, LdsIndividualOrdinance ldsIndividualOrdinance, JsonSerializerOptions options)
    {
        var ldsIndividualOrdinanceJson = new LdsIndividualOrdinanceJson(ldsIndividualOrdinance);
        JsonSerializer.Serialize(writer, ldsIndividualOrdinanceJson, ldsIndividualOrdinanceJson.GetType(), options);
    }
}

internal class LdsIndividualOrdinanceJson : GedcomJson
{
    public LdsIndividualOrdinanceJson(LdsIndividualOrdinance ldsIndividualOrdinance)
    {
        DateLdsOrdinance = JsonString(ldsIndividualOrdinance.DateLdsOrdinance);
        LdsBaptismDateStatus = JsonRecord(ldsIndividualOrdinance.LdsBaptismDateStatus);
        NoteStructures = JsonList(ldsIndividualOrdinance.NoteStructures);
        PlaceLivingOrdinance = JsonString(ldsIndividualOrdinance.PlaceLivingOrdinance);
        SourceCitations = JsonList(ldsIndividualOrdinance.SourceCitations);
        TempleCode = JsonString(ldsIndividualOrdinance.TempleCode);
    }

    public string? DateLdsOrdinance { get; set; }
    public LdsOrdinanceStatus? LdsBaptismDateStatus { get; set; }
    public List<NoteStructure>? NoteStructures { get; set; }
    public string? PlaceLivingOrdinance { get; set; }
    public List<SourceCitation>? SourceCitations { get; set; }
    public string? TempleCode { get; set; }
}

#region LDS_INDIVIDUAL_ORDINANCE p. 
/* 

LDS_INDIVIDUAL_ORDINANCE:=
[
n [ BAPL | CONL ] {1:1}
    +1 DATE <DATE_LDS_ORD> {0:1} p.46
    +1 TEMP <TEMPLE_CODE> {0:1} p.63
    +1 PLAC <PLACE_LIVING_ORDINANCE> {0:1} p.58
    +1 STAT <LDS_BAPTISM_DATE_STATUS> {0:1} p.51
        +2 DATE <CHANGE_DATE> {1:1} p.44
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<SOURCE_CITATION>> {0:M} p.39
|
n ENDL {1:1}
    +1 DATE <DATE_LDS_ORD> {0:1} p.46
    +1 TEMP <TEMPLE_CODE> {0:1} p.63
    +1 PLAC <PLACE_LIVING_ORDINANCE> {0:1} p.58
    +1 STAT <LDS_ENDOWMENT_DATE_STATUS> {0:1} p.52
        +2 DATE <CHANGE_DATE> {1:1} p.44
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<SOURCE_CITATION>> {0:M} p.39
|
n SLGC {1:1}
    +1 DATE <DATE_LDS_ORD> {0:1} p.46
    +1 TEMP <TEMPLE_CODE> {0:1} p.63
    +1 PLAC <PLACE_LIVING_ORDINANCE> {0:1} p.58
    +1 FAMC @<XREF:FAM>@ {1:1} p.24
    +1 STAT <LDS_CHILD_SEALING_DATE_STATUS> {0:1} p.51
        +2 DATE <CHANGE_DATE> {1:1} p.44
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<SOURCE_CITATION>> {0:M} p.39
]

*/
#endregion