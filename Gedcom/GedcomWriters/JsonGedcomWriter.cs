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
        var familyDtos = GetFamilyDtos(xref);

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(familyDtos));
    }

    public override byte[] GetRepositories(string xref = "")
    {
        var repositoryDtos = GetRepositoryDtos(xref);

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(repositoryDtos, GedcomDto.SerializationOptions));
    }

    public override byte[] GetSources(string xref = "")
    {
        var sourceDtos = GetSourceDtos(xref);

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(sourceDtos, GedcomDto.SerializationOptions));
    }
}

