using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(LdsIndividualOrdinanceJsonConverter))]
public class LdsIndividualOrdinance : RecordStructureBase
{
    public LdsIndividualOrdinance() : base() { }
    public LdsIndividualOrdinance(Record record) : base(record) { }

    private string? _dateLdsOrdinance = null;
    public string DateLdsOrdinance => _dateLdsOrdinance ??= GetValue(Tag.Date);

    private LdsOrdinanceStatus? _ldsBaptismDateStatus = null;
    public LdsOrdinanceStatus LdsBaptismDateStatus => _ldsBaptismDateStatus ??= First<LdsOrdinanceStatus>(Tag.Status);

    private List<NoteStructure>? _noteStructures = null;
    public List<NoteStructure> NoteStructures => _noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? _placeLivingOrdinance = null;
    public string PlaceLivingOrdinance => _placeLivingOrdinance ??= GetValue(Tag.Place);

    private List<SourceCitation>? _sourceCitations = null;
    public List<SourceCitation> SourceCitations => _sourceCitations ??= List<SourceCitation>(Tag.Source);

    private string? _templeCode = null;
    public string TempleCode => _templeCode ??= GetValue(Tag.Temple);

    public override string ToString() => $"{Record.Value}, {TempleCode}, {PlaceLivingOrdinance}";
}

internal sealed class LdsIndividualOrdinanceJsonConverter : JsonConverter<LdsIndividualOrdinance>
{
    public override LdsIndividualOrdinance? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, LdsIndividualOrdinance value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new LdsIndividualOrdinanceDto(value), GedcomDto.SerializationOptions);
    }
}

public class LdsIndividualOrdinanceDto(LdsIndividualOrdinance ldsIndividualOrdinance) : GedcomDto
{
    public string? DateLdsOrdinance { get; set; } = GetString(ldsIndividualOrdinance.DateLdsOrdinance);
    public LdsOrdinanceStatusDto? LdsBaptismDateStatus { get; set; } = GetRecord(new LdsOrdinanceStatusDto(ldsIndividualOrdinance.LdsBaptismDateStatus));
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(ldsIndividualOrdinance.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string? PlaceLivingOrdinance { get; set; } = GetString(ldsIndividualOrdinance.PlaceLivingOrdinance);
    public List<SourceCitationDto>? SourceCitations { get; set; } = GetList(ldsIndividualOrdinance.SourceCitations.Select(sc => new SourceCitationDto(sc)).ToList());
    public string? TempleCode { get; set; } = GetString(ldsIndividualOrdinance.TempleCode);
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