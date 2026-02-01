using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(ChildToFamilyLinkJsonConverter))]
public class ChildToFamilyLink : RecordStructureBase
{
    public ChildToFamilyLink() : base() { }
    public ChildToFamilyLink(Record record) : base(record) { }

    private string? adoptedByWhichParent = null;
    public string AdoptedByWhichParent => adoptedByWhichParent ??= GetValue(Tag.Adoption);

    private string? childLinkageStatus = null;
    public string ChildLinkageStatus => childLinkageStatus ?? GetValue(Tag.Status);

    private List<NoteStructure>? noteStructures = null;
    public List<NoteStructure> NoteStructures => noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? pedigreeLinkageType = null;
    public string PedigreeLinkageType => pedigreeLinkageType ??= GetValue(Tag.Pedigree);
   
    public string Xref => Record.Value;

    public override string ToString() => $"{Record.Value}, {PedigreeLinkageType}";
}

internal sealed class ChildToFamilyLinkJsonConverter : JsonConverter<ChildToFamilyLink>
{
    public override ChildToFamilyLink? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, ChildToFamilyLink value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new ChildToFamilyLinkJson(value), options);
    }
}

public class ChildToFamilyLinkJson(ChildToFamilyLink childToFamilyLink) : GedcomJson
{
    public string? AdoptedByWhichParent { get; set; } = JsonString(childToFamilyLink.AdoptedByWhichParent);
    public string? ChildLinkageStatus { get; set; } = JsonString(childToFamilyLink.ChildLinkageStatus);
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(childToFamilyLink.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
    public string? PedigreeLinkageType { get; set; } = JsonString(childToFamilyLink.PedigreeLinkageType);
    public string? Xref { get; set; } = JsonString(childToFamilyLink.Xref);
    public override string ToString() => $"{Xref}";
}

#region CHILD_TO_FAMILY_LINK p. 31-32
/* 

CHILD_TO_FAMILY_LINK:=

n FAMC @<XREF:FAM>@ {1:1} p.24
    +1 PEDI <PEDIGREE_LINKAGE_TYPE> {0:1} p.57
    +1 STAT <CHILD_LINKAGE_STATUS> {0:1} p.44
    +1 <<NOTE_STRUCTURE>> {0:M} p.37

[Editor] Possible errata in The Gedcom Standard 5.5.1 The ADOP tag on page 34 
has an structure that looks like this: 

    n ADOP {1:1}
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
    +1 FAMC @<XREF:FAM>@ {0:1} p.24
        +2 ADOP<ADOPTED_BY_WHICH_PARENT> {0:1}

However,the ADOP line is missing from FAMC structure on page 32. I'm guessing 
that may have been a minor ommission. I'm adding ADOP to this class.

*/
#endregion