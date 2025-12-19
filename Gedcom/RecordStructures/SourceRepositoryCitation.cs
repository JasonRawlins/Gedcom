using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SourceRepositoryCitationJsonConverter))]
public class SourceRepositoryCitation : RecordStructureBase
{
    public SourceRepositoryCitation() : base() { }
    public SourceRepositoryCitation(Record record) : base(record) { }

    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public List<CallNumber> CallNumbers => List<CallNumber>(Tag.CallNumber);

    public override string ToString() => $"{Record.Value}";
}

internal class SourceRepositoryCitationJsonConverter : JsonConverter<SourceRepositoryCitation>
{
    public override SourceRepositoryCitation? ReadJson(JsonReader reader, Type objectType, SourceRepositoryCitation? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, SourceRepositoryCitation? sourceRepositoryCitation, JsonSerializer serializer)
    {
        if (sourceRepositoryCitation == null) throw new ArgumentNullException(nameof(sourceRepositoryCitation));

        serializer.Serialize(writer, new SourceRepositoryCitationJson(sourceRepositoryCitation));
    }
}

internal class SourceRepositoryCitationJson : GedcomJson
{
    public SourceRepositoryCitationJson(SourceRepositoryCitation sourceRepositoryCitation)
    {
        CallNumbers = JsonList(sourceRepositoryCitation.CallNumbers);
        Notes = JsonList(sourceRepositoryCitation.NoteStructures);
    }

    public List<CallNumber>? CallNumbers { get; set; }
    public List<NoteStructure>? Notes { get; set; }
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