using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(LdsIndividualOrdinanceJsonConverter))]
public class LdsIndividualOrdinance : RecordStructureBase
{
    public LdsIndividualOrdinance() : base() { }
    public LdsIndividualOrdinance(Record record) : base(record) { }

    public string DateLdsOrdinance => GetValue(Tag.Date);
    public LdsOrdinanceStatus LdsBaptismDateStatus => First<LdsOrdinanceStatus>(Tag.Status);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string PlaceLivingOrdinance => GetValue(Tag.Place);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(Tag.Source);
    public string TempleCode => GetValue(Tag.Temple);

    public override string ToString() => $"{Record.Value}, {TempleCode}, {PlaceLivingOrdinance}";
}

internal class LdsIndividualOrdinanceJsonConverter : JsonConverter<LdsIndividualOrdinance>
{
    public override LdsIndividualOrdinance? ReadJson(JsonReader reader, Type objectType, LdsIndividualOrdinance? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, LdsIndividualOrdinance? ldsIndividualOrdinance, JsonSerializer serializer)
    {
        if (ldsIndividualOrdinance  == null) throw new ArgumentNullException(nameof(ldsIndividualOrdinance));

        serializer.Serialize(writer, new LdsIndividualOrdinanceJson(ldsIndividualOrdinance));
    }
}

public class LdsIndividualOrdinanceJson(LdsIndividualOrdinance ldsIndividualOrdinance) : GedcomJson
{
    public string? DateLdsOrdinance { get; set; } = JsonString(ldsIndividualOrdinance.DateLdsOrdinance);
    public LdsOrdinanceStatusJson? LdsBaptismDateStatus { get; set; } = JsonRecord(new LdsOrdinanceStatusJson(ldsIndividualOrdinance.LdsBaptismDateStatus));
    public List<NoteJson>? Notes { get; set; } = JsonList(ldsIndividualOrdinance.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
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