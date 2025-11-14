using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(SpouseToFamilyLinkJsonConverter))]
public class SpouseToFamilyLink : RecordStructureBase
{
    public SpouseToFamilyLink() : base() { }
    public SpouseToFamilyLink(Record record) : base(record) { }

    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
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

internal class SpouseToFamilyLinkJson : GedcomJson
{
    public SpouseToFamilyLinkJson(SpouseToFamilyLink spouseToFamilyLink)
    {
        Notes = JsonList(spouseToFamilyLink.NoteStructures);
        Xref = spouseToFamilyLink.Xref;
    }

    public List<NoteStructure>? Notes { get; set; }
    public string Xref { get; set; }
}

#region SPOUSE_TO_FAMILY_LINK p. 40
/* 

SPOUSE_TO_FAMILY_LINK:=

n FAMS @<XREF:FAM>@ {1:1} p.24
    +1 <<NOTE_STRUCTURE>> {0:M} p.37

*/
#endregion