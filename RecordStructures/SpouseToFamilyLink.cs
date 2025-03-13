using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(SpouseToFamilyLinkJsonConverter))]
public class SpouseToFamilyLink : RecordStructureBase
{
    public SpouseToFamilyLink() : base() { }
    public SpouseToFamilyLink(Record record) : base(record) { }

    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
}

internal class SpouseToFamilyLinkJsonConverter : JsonConverter<SpouseToFamilyLink>
{
    public override SpouseToFamilyLink? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, SpouseToFamilyLink spouseToFamilyLink, JsonSerializerOptions options)
    {
        var spouseToFamilyLinkJson = new SpouseToFamilyLinkJson(spouseToFamilyLink);
        JsonSerializer.Serialize(writer, spouseToFamilyLinkJson, spouseToFamilyLinkJson.GetType(), options);
    }
}

internal class SpouseToFamilyLinkJson
{
    public SpouseToFamilyLinkJson(SpouseToFamilyLink spouseToFamilyLink)
    {
        NoteStructures = spouseToFamilyLink.NoteStructures.Count == 0 ? null : spouseToFamilyLink.NoteStructures;
    }

    public List<NoteStructure>? NoteStructures { get; set; }
}

#region SPOUSE_TO_FAMILY_LINK p. 40
/* 

SPOUSE_TO_FAMILY_LINK:=

n FAMS @<XREF:FAM>@ {1:1} p.24
    +1 <<NOTE_STRUCTURE>> {0:M} p.37

*/
#endregion