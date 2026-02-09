using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SpouseToFamilyLinkJsonConverter))]
public class SpouseToFamilyLink : RecordStructureBase
{
    public SpouseToFamilyLink() : base() { }
    public SpouseToFamilyLink(Record record) : base(record) { }

    private List<NoteStructure>? _noteStructures = null;
    public List<NoteStructure> NoteStructures => _noteStructures ??= List<NoteStructure>(Tag.Note);
    
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}";
}

internal sealed class SpouseToFamilyLinkJsonConverter : JsonConverter<SpouseToFamilyLink>
{
    public override SpouseToFamilyLink? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, SpouseToFamilyLink value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new SpouseToFamilyLinkDto(value), GedcomDto.SerializationOptions);
    }
}

public class SpouseToFamilyLinkDto(SpouseToFamilyLink spouseToFamilyLink) : GedcomDto
{
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(spouseToFamilyLink.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string Xref { get; set; } = spouseToFamilyLink.Xref;

    public override string ToString() => Xref;
}

#region SPOUSE_TO_FAMILY_LINK p. 40
/* 

SPOUSE_TO_FAMILY_LINK:=

n FAMS @<XREF:FAM>@ {1:1} p.24
    +1 <<NOTE_STRUCTURE>> {0:M} p.37

*/
#endregion