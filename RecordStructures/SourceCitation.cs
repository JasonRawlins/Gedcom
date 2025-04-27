using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(SourceCitationJsonConverter))]
public class SourceCitation : RecordStructureBase
{
    public SourceCitation() : base() { }
    public SourceCitation(Record record) : base(record) { }

    public string CertaintyAssessment => _(C.QUAY);
    public SourceCitationData Data => First<SourceCitationData>(C.DATA);
    public EventTypeCitedFrom EventTypeCitedFrom => First<EventTypeCitedFrom>(C.EVEN);
    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(C.OBJE);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public string WhereWithinSource => _(C.PAGE);

    public override string ToString() => $"{Record.Value}, {WhereWithinSource}";
}

internal class SourceCitationJsonConverter : JsonConverter<SourceCitation>
{
    public override SourceCitation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, SourceCitation sourceCitation, JsonSerializerOptions options)
    {
        var sourceCitationJson = new SourceCitationJson(sourceCitation);
        JsonSerializer.Serialize(writer, sourceCitationJson, sourceCitationJson.GetType(), options);
    }
}

internal class SourceCitationJson : GedcomJson
{
    public SourceCitationJson(SourceCitation sourceCitation)
    {
        CertaintyAssessment = JsonString(sourceCitation.CertaintyAssessment);
        Data = JsonRecord(sourceCitation.Data);
        EventTypeCitedFrom = JsonRecord(sourceCitation.EventTypeCitedFrom);
        MultimediaLinks = JsonList(sourceCitation.MultimediaLinks);
        Notes = JsonList(sourceCitation.NoteStructures.Select(ns => ns.Text).ToList());
        WhereWithinSource = JsonString(sourceCitation.WhereWithinSource);
        Xref = sourceCitation.Xref;
    }

    public string? CertaintyAssessment { get; set; }
    public SourceCitationData? Data { get; set; }
    public EventTypeCitedFrom? EventTypeCitedFrom { get; set; }
    public List<MultimediaLink>? MultimediaLinks { get; set; }
    public List<string>? Notes { get; set; }
    public string? WhereWithinSource { get; set; }
    public string? Xref { get; set; }
}

#region SOURCE_CITATION p. 39
/* 

SOURCE_CITATION:=

[ // pointer to source record (preferred)
n SOUR @<XREF:SOUR>@ {1:1} p.27
    +1 PAGE <WHERE_WITHIN_SOURCE> {0:1} p.64
    +1 EVEN <EVENT_TYPE_CITED_FROM> {0:1} p.49
        +2 ROLE <ROLE_IN_EVENT> {0:1} p.61
    +1 DATA {0:1}
        +2 DATE <ENTRY_RECORDING_DATE> {0:1} p.48
        +2 TEXT <TEXT_FROM_SOURCE> {0:M} p.63
            +3 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}
    +1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 QUAY <CERTAINTY_ASSESSMENT> {0:1} p.43

| // Systems not using source records

n SOUR <SOURCE_DESCRIPTION> {1:1} p.61
    +1 [CONC|CONT] <SOURCE_DESCRIPTION> {0:M}
    +1 TEXT <TEXT_FROM_SOURCE> {0:M} p.63
        +2 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}
    +1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 QUAY <CERTAINTY_ASSESSMENT> {0:1} p.43
]

The data provided in the <<SOURCE_CITATION>> structure is source-related information specific
to the data being cited. (See GEDCOM examples starting on page 74.) Systems that do not use a
(SOURCE_RECORD) must use the non-preferred second SOURce citation structure option. When
systems that support the zero level source record format encounters a source citation that does not
contain pointers to source records, then that system needs to create a SOURCE_RECORD format
and store the source description information found in the non-structured source citation in the title
area for the new source record.

The information intended to be placed in the citation structure includes:
* The pointer to the SOURCE_RECORD, which contains a more general description of the source
used for the fact being cited.

* Information, such as a page number, to help the user find the cited data within the referenced
source. This is stored in the “.SOUR.PAGE” tag context.

* Actual text from the source that was used in making assertions, for example a date phrase as
actually recorded in the source, or significant notes written by the recorder, or an applicable
sentence from a letter. This is stored in the “.SOUR.DATA.TEXT” tag context.

* Data that allows an assessment of the relative value of one source over another for making the
recorded assertions (primary or secondary source, etc.). Data needed for this assessment is data
that would help determine how much time from the date of the asserted fact and when the source
was actually recorded, what type of event was cited, and what type of role did this person have in
the cited source.

  - Date when the entry was recorded in source document is stored in the
    ".SOUR.DATA.DATE" tag context.
  - The type of event that initiated the recording is stored in the “SOUR.EVEN” tag context. The
    value used is the event code taken from the table of choices shown in the
    EVENT_TYPE_CITED_FROM primitive on page 49
  - The role of this person in the event is stored in the ".SOUR.EVEN.ROLE" context.SOURCE_CITATION:=

*/
#endregion