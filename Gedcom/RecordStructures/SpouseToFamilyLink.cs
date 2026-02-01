using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SpouseToFamilyLinkJsonConverter))]
public class SpouseToFamilyLink : RecordStructureBase
{
    public SpouseToFamilyLink() : base() { }
    public SpouseToFamilyLink(Record record) : base(record) { }

    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}";
}

internal sealed class SpouseToFamilyLinkJsonConverter : JsonConverter<SpouseToFamilyLink>
{
    public override SpouseToFamilyLink? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, SpouseToFamilyLink value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new SpouseToFamilyLinkJson(value), options);
    }
}

public class SpouseToFamilyLinkJson(SpouseToFamilyLink spouseToFamilyLink) : GedcomJson
{
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(spouseToFamilyLink.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
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