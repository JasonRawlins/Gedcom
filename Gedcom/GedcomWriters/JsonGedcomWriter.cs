using Gedcom.RecordStructures;
using System.Text;
using System.Text.Json;

namespace Gedcom.GedcomWriters;

public class JsonGedcomWriter(GedcomDocument gedcom) : IGedcomWriter
{
    private GedcomDocument GedcomDocument { get; set; } = gedcom;

    public byte[] GetIndividual(string xref)
    {
        var individualRecord = GedcomDocument.GetIndividualRecord(xref);
        if (individualRecord.IsEmpty) return [];

        var individualDto = new IndividualDto(individualRecord);

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(individualDto));
    }

    public byte[] GetIndividuals(string query = "")
    {
        var individualRecords = GedcomDocument.GetIndividualRecords(query);
        if (individualRecords.Count.Equals(0)) return [];

        var individualDtos = new List<IndividualDto>();

        foreach (var individualRecord in individualRecords)
        {
            individualDtos.Add(new IndividualDto(individualRecord));
        }

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(individualDtos));
    }

    public string GetFamily(string xref)
    {
        var familyRecord = GedcomDocument.GetFamilyRecord(xref);

        if (familyRecord.IsEmpty) return "{}";

        return JsonSerializer.Serialize(familyRecord);
    }

    public string GetFamilies(string query = "")
    {
        var familyRecords = GedcomDocument.GetFamilyRecords(query);

        return JsonSerializer.Serialize(familyRecords);
    }

    public string GetRepository(string xref)
    {
        var repositoryRecord = GedcomDocument.GetRepositoryRecord(xref);

        if (repositoryRecord.IsEmpty) return "{}";

        return JsonSerializer.Serialize(repositoryRecord);
    }

    public string GetRepositories(string query = "")
    {
        var repositoryRecords = GedcomDocument.GetRepositoryRecords(query);

        return JsonSerializer.Serialize(repositoryRecords);
    }

    public string GetSource(string xref)
    {
        var sourceRecord = GedcomDocument.GetSourceRecord(xref);

        if (sourceRecord.IsEmpty) return "{}";

        return JsonSerializer.Serialize(sourceRecord);
    }

    public string GetSources(string query = "")
    {
        var sourceRecords = GedcomDocument.GetSourceRecords(query);

        return JsonSerializer.Serialize(sourceRecords);
    }
}

