using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(AssociationStructureJsonConverter))]
public class AssociationStructure : RecordStructureBase
{
    public AssociationStructure() : base() { }
    public AssociationStructure(Record record) : base(record) { }

    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public string RelationIsDescriptor => _(C.RELA);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(C.SOUR);

    public override string ToString() => $"{Record.Value}, {RelationIsDescriptor}";
}

internal class AssociationStructureJsonConverter : JsonConverter<AssociationStructure>
{
    public override AssociationStructure? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, AssociationStructure associationStructure, JsonSerializerOptions options)
    {
        var associationStructureJson = new AssociationStructureJson(associationStructure);
        JsonSerializer.Serialize(writer, associationStructureJson, associationStructureJson.GetType(), options);
    }
}

internal class AssociationStructureJson : GedcomJson
{
    public AssociationStructureJson(AssociationStructure associationStructure)
    {
        NoteStructures = JsonList(associationStructure.NoteStructures);
        RelationIsDescriptor = JsonString(associationStructure.RelationIsDescriptor);
        SourceCitations = JsonList(associationStructure.SourceCitations);
    }

    public List<NoteStructure>? NoteStructures { get; set; }
    public string? RelationIsDescriptor { get; set; }
    public List<SourceCitation>? SourceCitations { get; set; }
}

#region ASSOCIATION_STRUCTURE p. 31 
/* 

ASSOCIATION_STRUCTURE:=

n ASSO @<XREF:INDI>@ {1:1} p.25
    +1 RELA <RELATION_IS_DESCRIPTOR> {1:1} p.60
    +1 <<SOURCE_CITATION>> {0:M} p.39
    +1 <<NOTE_STRUCTURE>> {0:M} p.37

The association pointer only associates INDIvidual records to INDIvidual records.

*/
#endregion