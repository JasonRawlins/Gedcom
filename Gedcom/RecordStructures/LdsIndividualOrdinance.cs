using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(LdsIndividualOrdinanceJsonConverter))]
public class LdsIndividualOrdinance : RecordStructureBase
{
    public LdsIndividualOrdinance() : base() { }
    public LdsIndividualOrdinance(Record record) : base(record) { }

    private string? dateLdsOrdinance = null;
    public string DateLdsOrdinance => dateLdsOrdinance ??= GetValue(Tag.Date);

    private LdsOrdinanceStatus? ldsBaptismDateStatus = null;
    public LdsOrdinanceStatus LdsBaptismDateStatus => ldsBaptismDateStatus ??= First<LdsOrdinanceStatus>(Tag.Status);

    private List<NoteStructure>? noteStructures = null;
    public List<NoteStructure> NoteStructures => noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? placeLivingOrdinance = null;
    public string PlaceLivingOrdinance => placeLivingOrdinance ??= GetValue(Tag.Place);

    private List<SourceCitation>? sourceCitations = null;
    public List<SourceCitation> SourceCitations => sourceCitations ??= List<SourceCitation>(Tag.Source);

    private string? templeCode = null;
    public string TempleCode => templeCode ??= GetValue(Tag.Temple);

    public override string ToString() => $"{Record.Value}, {TempleCode}, {PlaceLivingOrdinance}";
}

internal sealed class LdsIndividualOrdinanceJsonConverter : JsonConverter<LdsIndividualOrdinance>
{
    public override LdsIndividualOrdinance? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, LdsIndividualOrdinance value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new LdsIndividualOrdinanceJson(value), options);
    }
}

public class LdsIndividualOrdinanceJson(LdsIndividualOrdinance ldsIndividualOrdinance) : GedcomJson
{
    public string? DateLdsOrdinance { get; set; } = JsonString(ldsIndividualOrdinance.DateLdsOrdinance);
    public LdsOrdinanceStatusJson? LdsBaptismDateStatus { get; set; } = JsonRecord(new LdsOrdinanceStatusJson(ldsIndividualOrdinance.LdsBaptismDateStatus));
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(ldsIndividualOrdinance.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
    public string? PlaceLivingOrdinance { get; set; } = JsonString(ldsIndividualOrdinance.PlaceLivingOrdinance);
    public List<SourceCitationJson>? SourceCitations { get; set; } = JsonList(ldsIndividualOrdinance.SourceCitations.Select(sc => new SourceCitationJson(sc)).ToList());
    public string? TempleCode { get; set; } = JsonString(ldsIndividualOrdinance.TempleCode);
    public override string ToString() => $"{TempleCode} {DateLdsOrdinance}";
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