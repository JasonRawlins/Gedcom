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

        return Encoding.UTF8.GetBytes(SerializeObject(individualRecord));
    }

    public byte[] GetIndividuals(string query = "")
    {
        var individualRecords = GedcomDocument.GetIndividualRecords(query);

        return Encoding.UTF8.GetBytes(SerializeObject(individualRecords));
    }

    public string GetFamily(string xref)
    {
        var familyRecord = GedcomDocument.GetFamilyRecord(xref);

        if (familyRecord.IsEmpty) return "{}";

        return SerializeObject(familyRecord);
    }

    public string GetFamilies(string query = "")
    {
        var familyRecords = GedcomDocument.GetFamilyRecords(query);

        return SerializeObject(familyRecords);
    }

    public string GetRepository(string xref)
    {
        var repositoryRecord = GedcomDocument.GetRepositoryRecord(xref);

        if (repositoryRecord.IsEmpty) return "{}";

        return SerializeObject(repositoryRecord);
    }

    public string GetRepositories(string query = "")
    {
        var repositoryRecords = GedcomDocument.GetRepositoryRecords(query);

        return SerializeObject(repositoryRecords);
    }

    public string GetSource(string xref)
    {
        var sourceRecord = GedcomDocument.GetSourceRecord(xref);

        if (sourceRecord.IsEmpty) return "{}";

        return SerializeObject(sourceRecord);
    }

    public string GetSources(string query = "")
    {
        var sourceRecords = GedcomDocument.GetSourceRecords(query);

        return SerializeObject(sourceRecords);
    }

    private static string SerializeObject(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}

