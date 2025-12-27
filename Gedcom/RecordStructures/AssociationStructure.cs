using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(AssociationJsonConverter))]
public class AssociationStructure : RecordStructureBase
{
    public AssociationStructure() : base() { }
    public AssociationStructure(Record record) : base(record) { }

    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string RelationIsDescriptor => GetValue(Tag.Relationship);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(Tag.Source);

    public override string ToString() => $"{Record.Value}, {RelationIsDescriptor}";
}

internal class AssociationJsonConverter : JsonConverter<AssociationStructure>
{
    public override AssociationStructure? ReadJson(JsonReader reader, Type objectType, AssociationStructure? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, AssociationStructure? associationStructure, JsonSerializer serializer)
    {
        if (associationStructure == null) throw new ArgumentNullException(nameof(associationStructure));

        serializer.Serialize(writer, new AssociationJson(associationStructure));
    }
}

internal class AssociationJson : GedcomJson
{
    public AssociationJson(AssociationStructure associationStructure)
    {
        Notes = JsonList(associationStructure.NoteStructures);
        RelationIsDescriptor = JsonString(associationStructure.RelationIsDescriptor);
        SourceCitations = JsonList(associationStructure.SourceCitations);
    }

    public List<NoteStructure>? Notes { get; set; }
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