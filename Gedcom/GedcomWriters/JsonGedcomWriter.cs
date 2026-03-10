using Gedcom.RecordStructures;
using System.Text;
using System.Text.Json;

namespace Gedcom.GedcomWriters;

public class JsonGedcomWriter(GedcomDocument gedcom) : IGedcomWriter
{
    public GedcomDocument GedcomDocument { get; set; } = gedcom;

    public byte[] GetIndividuals(string xref = "")
    {
        // TODO: Filter by xref after retrieving, if necessary.
        var individualRecords = GedcomDocument.GetIndividualRecords();
        if (individualRecords.Count == 0) return [];

        var individualDtos = new List<IndividualDto>();

        foreach (var individualRecord in individualRecords)
        {
            individualDtos.Add(new IndividualDto(individualRecord));
        }

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(individualDtos));
    }

    public byte[] GetFamilies(string xref = "")
    {
        // TODO: Filter by xref after retrieving, if necessary.

        var familyRecords = GedcomDocument.GetFamilyRecords();

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(familyRecords));
    }

    public byte[] GetRepositories(string xref = "")
    {
        // TODO: Filter by xref after retrieving, if necessary.

        var repositoryRecords = GedcomDocument.GetRepositoryRecords();

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(repositoryRecords));
    }

    public byte[] GetSources(string xref = "")
    {
        // TODO: Filter by xref after retrieving, if necessary.
        var sourceRecords = GedcomDocument.GetSourceRecords();

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(sourceRecords));
    }
}

