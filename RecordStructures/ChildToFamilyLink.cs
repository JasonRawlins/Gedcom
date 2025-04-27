using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(ChildToFamilyLinkJsonConverter))]
public class ChildToFamilyLink : RecordStructureBase
{
    public ChildToFamilyLink() : base() { }
    public ChildToFamilyLink(Record record) : base(record) { }

    public string AdoptedByWhichParent => _(C.ADOP);
    public string ChildLinkageStatus => _(C.STAT);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public string PedigreeLinkageType => _(C.PEDI);

    public override string ToString() => $"{Record.Value}, {PedigreeLinkageType}";
}

internal class ChildToFamilyLinkJsonConverter : JsonConverter<ChildToFamilyLink>
{
    public override ChildToFamilyLink? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, ChildToFamilyLink childToFamilyLink, JsonSerializerOptions options)
    {
        var childToFamilyLinkJson = new ChildToFamilyLinkJson(childToFamilyLink);
        JsonSerializer.Serialize(writer, childToFamilyLinkJson, childToFamilyLinkJson.GetType(), options);
    }
}

internal class ChildToFamilyLinkJson : GedcomJson
{
    public ChildToFamilyLinkJson(ChildToFamilyLink childToFamilyLink)
    {
        AdoptedByWhichParent = JsonString(childToFamilyLink.AdoptedByWhichParent);
        ChildLinkageStatus = JsonString(childToFamilyLink.ChildLinkageStatus);
        NoteStructures = JsonList(childToFamilyLink.NoteStructures);
        PedigreeLinkageType = JsonString(childToFamilyLink.PedigreeLinkageType);
        Xref = JsonString(childToFamilyLink.Xref);
    }

    public string? AdoptedByWhichParent { get; set; }
    public string? ChildLinkageStatus { get; set; }
    public List<NoteStructure>? NoteStructures { get; set; }
    public string? PedigreeLinkageType { get; set; }
    public string? Xref { get; set; }
}

#region CHILD_TO_FAMILY_LINK p. 31-32
/* 

CHILD_TO_FAMILY_LINK:=

n FAMC @<XREF:FAM>@ {1:1} p.24
    +1 PEDI <PEDIGREE_LINKAGE_TYPE> {0:1} p.57
    +1 STAT <CHILD_LINKAGE_STATUS> {0:1} p.44
    +1 <<NOTE_STRUCTURE>> {0:M} p.37

[Editor] Possible errata in The Gedcom Standard 5.1.1 The ADOP tag on page 34 
has an structure that looks like this: 

    n ADOP {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    +1 FAMC @<XREF:FAM>@ {0:1} p.24
        +2 ADOP<ADOPTED_BY_WHICH_PARENT> {0:1}

However,the ADOP line is missing from FAMC structure on page 32. I'm guessing 
that may have been a minor ommission. I'm adding ADOP to this class.

*/
#endregion