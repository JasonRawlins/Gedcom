using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(LdsIndividualOrdinanceJsonConverter))]
public class LdsIndividualOrdinance : RecordStructureBase
{
    public LdsIndividualOrdinance() : base() { }
    public LdsIndividualOrdinance(Record record) : base(record) { }

    public string DateLdsOrdinance => _(Tag.Date);
    public LdsOrdinanceStatus LdsBaptismDateStatus => First<LdsOrdinanceStatus>(Tag.Status);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string PlaceLivingOrdinance => _(Tag.Place);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(Tag.Source);
    public string TempleCode => _(Tag.Temple);

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

internal class LdsIndividualOrdinanceJson : GedcomJson
{
    public LdsIndividualOrdinanceJson(LdsIndividualOrdinance ldsIndividualOrdinance)
    {
        DateLdsOrdinance = JsonString(ldsIndividualOrdinance.DateLdsOrdinance);
        LdsBaptismDateStatus = JsonRecord(ldsIndividualOrdinance.LdsBaptismDateStatus);
        Notes = JsonList(ldsIndividualOrdinance.NoteStructures);
        PlaceLivingOrdinance = JsonString(ldsIndividualOrdinance.PlaceLivingOrdinance);
        SourceCitations = JsonList(ldsIndividualOrdinance.SourceCitations);
        TempleCode = JsonString(ldsIndividualOrdinance.TempleCode);
    }

    public string? DateLdsOrdinance { get; set; }
    public LdsOrdinanceStatus? LdsBaptismDateStatus { get; set; }
    public List<NoteStructure>? Notes { get; set; }
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