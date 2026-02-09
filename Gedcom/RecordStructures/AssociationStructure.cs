using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(AssociationStructureJsonConverter))]
public class AssociationStructure : RecordStructureBase
{
    public AssociationStructure() : base() { }
    public AssociationStructure(Record record) : base(record) { }

    private List<NoteStructure>? _noteStructures = null;
    public List<NoteStructure> NoteStructures => _noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? _relationIsDescriptor = null;
    public string RelationIsDescriptor => _relationIsDescriptor ??= GetValue(Tag.Relationship);

    private List<SourceCitation>? _sourceCitations = null;
    public List<SourceCitation> SourceCitations => _sourceCitations ??= List<SourceCitation>(Tag.Source);
    
    public override string ToString() => $"{Record.Value}, {RelationIsDescriptor}";
}

internal sealed class AssociationStructureJsonConverter : JsonConverter<AssociationStructure>
{
    public override AssociationStructure? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, AssociationStructure value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new AssociationDto(value), GedcomDto.SerializationOptions);
    }
}

public class AssociationDto(AssociationStructure associationStructure) : GedcomDto
{
    public List<NoteDto>? Notes { get; set; } = GedcomDto.GetList<NoteDto>(associationStructure.NoteStructures.Select(ns => new NoteDto(ns)).ToList());
    public string? RelationIsDescriptor { get; set; } = GetString(associationStructure.RelationIsDescriptor);
    public List<SourceCitationDto>? SourceCitations { get; set; } = GetList(associationStructure.SourceCitations.Select(sc => new SourceCitationDto(sc)).ToList());
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