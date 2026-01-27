using Newtonsoft.Json;

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

internal class SpouseToFamilyLinkJsonConverter : JsonConverter<SpouseToFamilyLink>
{
    public override SpouseToFamilyLink? ReadJson(JsonReader reader, Type objectType, SpouseToFamilyLink? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, SpouseToFamilyLink? spouseToFamilyLink, JsonSerializer serializer)
    {
        if (spouseToFamilyLink == null) throw new ArgumentNullException(nameof(spouseToFamilyLink));

        serializer.Serialize(writer, new SpouseToFamilyLinkJson(spouseToFamilyLink));
    }
}

public class SpouseToFamilyLinkJson(SpouseToFamilyLink spouseToFamilyLink) : GedcomJson
{
    public List<NoteJson>? Notes { get; set; } = JsonList(spouseToFamilyLink.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
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