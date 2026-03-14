using System.Text;
using System.Text.Json;

namespace Gedcom.GedcomWriters;

public class JsonGedcomWriter : GedcomWriter
{
    public JsonGedcomWriter(GedcomDocument gedcomDocument) : base(gedcomDocument)
    {
        GedcomDocument = gedcomDocument;
    }

    public override byte[] GetIndividuals(string xref = "")
    {
        var individualDtos = GetIndividualDtos(xref);

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(individualDtos, GedcomDto.SerializationOptions));
    }

    public override byte[] GetFamilies(string xref = "")
    {
        // TODO: Filter by xref after retrieving, if necessary.

        var familyRecords = GedcomDocument.GetFamilyRecords();

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(familyRecords));
    }

    public override byte[] GetRepositories(string xref = "")
    {
        // TODO: Filter by xref after retrieving, if necessary.

        var repositoryRecords = GedcomDocument.GetRepositoryRecords();

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(repositoryRecords, GedcomDto.SerializationOptions));
    }

    public override byte[] GetSources(string xref = "")
    {
        // TODO: Filter by xref after retrieving, if necessary.
        var sourceRecords = GedcomDocument.GetSourceRecords();

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(sourceRecords, GedcomDto.SerializationOptions));
    }
}

