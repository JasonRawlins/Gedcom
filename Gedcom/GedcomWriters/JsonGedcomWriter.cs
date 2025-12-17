using Newtonsoft.Json;
using System.Text;

namespace Gedcom.GedcomWriters;

public class JsonGedcomWriter(Gedcom gedcom) : IGedcomWriter
{
    private Gedcom Gedcom { get; set; } = gedcom;

    public string GetIndividual(string xref)
    {
        var individualRecord = Gedcom.GetIndividualRecord(xref);

        if (individualRecord.IsEmpty) return "{}";

        return SerializeObject(individualRecord);
    }

    public string GetIndividuals(string query = "")
    {
        var individualRecords = Gedcom.GetIndividualRecords(query);

        return SerializeObject(individualRecords);
    }

    public string GetFamily(string xref)
    {
        var familyRecord = Gedcom.GetFamilyRecord(xref);

        if (familyRecord.IsEmpty) return "{}";

        return SerializeObject(familyRecord);
    }

    public string GetFamilies(string query = "")
    {
        var familyRecords = Gedcom.GetFamilyRecords(query);

        return SerializeObject(familyRecords);
    }

    public string GetRepository(string xref)
    {
        var repositoryRecord = Gedcom.GetRepositoryRecord(xref);

        if (repositoryRecord.IsEmpty) return "{}";

        return SerializeObject(repositoryRecord);
    }

    public string GetRepositories(string query = "")
    {
        var repositoryRecords = Gedcom.GetRepositoryRecords(query);

        return SerializeObject(repositoryRecords);
    }

    public string GetSource(string xref)
    {
        var sourceRecord = Gedcom.GetSourceRecord(xref);

        if (sourceRecord.IsEmpty) return "{}";

        return SerializeObject(sourceRecord);
    }

    public string GetSources(string query = "")
    {
        var sourceRecords = Gedcom.GetSourceRecords(query);

        return SerializeObject(sourceRecords);
    }

    public byte[] GetAsByteArray(string query = "")
    {
        return Encoding.UTF8.GetBytes(GetFamilies());
    }

    private string SerializeObject(object obj)
    {
        return JsonConvert.SerializeObject(obj, JsonSettings.DefaultOptions);
    }
}

