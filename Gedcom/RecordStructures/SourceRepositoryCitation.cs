using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SourceRepositoryCitationJsonConverter))]
public class SourceRepositoryCitation : RecordStructureBase
{
    public SourceRepositoryCitation() : base() { }
    public SourceRepositoryCitation(Record record) : base(record) { }

    private List<CallNumber>? callNumbers = null;
    public List<CallNumber> CallNumbers => callNumbers ??= List<CallNumber>(Tag.CallNumber);

    private List<NoteStructure>? noteStructures = null;
    public List<NoteStructure> NoteStructures => noteStructures ??= List<NoteStructure>(Tag.Note);

    public override string ToString() => $"{Record.Value}";
}

internal sealed class SourceRepositoryCitationJsonConverter : JsonConverter<SourceRepositoryCitation>
{
    public override SourceRepositoryCitation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, SourceRepositoryCitation value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new SourceRepositoryCitationJson(value), options);
    }
}

public class SourceRepositoryCitationJson(SourceRepositoryCitation sourceRepositoryCitation) : GedcomJson
{
    public List<CallNumberJson>? CallNumbers { get; set; } = JsonList(sourceRepositoryCitation.CallNumbers.Select(cn => new CallNumberJson(cn)).ToList());
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(sourceRepositoryCitation.NoteStructures.Select(ns => new NoteJson(ns)).ToList());

    public override string ToString() => string.Join(", ", CallNumbers ?? []);
}

#region SOURCE_REPOSITORY_CITATION p. 40
/*

SOURCE_REPOSITORY_CITATION:=

n REPO [ @XREF:REPO@ | <NULL>] {1:1} p.27
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 CALN <SOURCE_CALL_NUMBER> {0:M} p.61
        +2 MEDI <SOURCE_MEDIA_TYPE> {0:1}

This structure is used within a source record to point to a name and address record of the holder of the
source document. Formal and informal repository name and addresses are stored in the
REPOSITORY_RECORD. Informal repositories include owner's of an unpublished work or of a rare
published source, or a keeper of personal collections. An example would be the owner of a family Bible
containing unpublished family genealogical entries. More formal repositories, such as the Family History
Library, should show a call number of the source at that repository. The call number of that source
should be recorded using a subordinate CALN tag. Systems which do not use repository name and
address record, should describe where the information cited is stored in the <<NOTE_STRUCTURE>>
of the REPOsitory source citation structure.

*/
#endregion