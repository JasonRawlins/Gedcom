using System.Text.Json;
using System.Text.Json.Serialization;
using Gedcom.Core;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(AssociationStructureJsonConverter))]
public class AssociationStructure : RecordStructureBase
{
    public AssociationStructure() : base() { }
    public AssociationStructure(Record record) : base(record) { }

    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string RelationIsDescriptor => GetValue(Tag.Relationship);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(Tag.Source);

    public override string ToString() => $"{Record.Value}, {RelationIsDescriptor}";
}

internal sealed class AssociationStructureJsonConverter : JsonConverter<AssociationStructure>
{
    public override AssociationStructure? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, AssociationStructure value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new AssociationJson(value), options);
    }
}

public class AssociationJson(AssociationStructure associationStructure) : GedcomJson
{
    public List<NoteJson>? Notes { get; set; } = GedcomJson.JsonList<NoteJson>(associationStructure.NoteStructures.Select(ns => new NoteJson(ns)).ToList());
    public string? RelationIsDescriptor { get; set; } = JsonString(associationStructure.RelationIsDescriptor);
    public List<SourceCitationJson>? SourceCitations { get; set; } = JsonList(associationStructure.SourceCitations.Select(sc => new SourceCitationJson(sc)).ToList());
    public override string ToString() => $"{RelationIsDescriptor}";
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