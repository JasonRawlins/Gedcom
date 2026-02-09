using Gedcom.RecordStructures;
using System.Text;
using System.Text.Json;

namespace Gedcom.GedcomWriters;

public class TextGedcomWriter(GedcomDocument gedcom) : IGedcomWriter
{
    private GedcomDocument GedcomDocument { get; set; } = gedcom;

    public string GetIndividual(string xref)
    {
        var individualRecord = GedcomDocument.GetIndividualRecord(xref);

        if (individualRecord.IsEmpty) return "";

        return GetIndividualLineItem(individualRecord);
    }

    public string GetIndividuals(string query = "")
    {
        var individualRecords = GedcomDocument.GetIndividualRecords(query);
        var orderedIndividualRecords = individualRecords
            .OrderBy(ir => ir.Surname)
            .ThenBy(ir => ir.Given);

        var individualsStringBuilder = new StringBuilder();

        foreach (var individualRecord in orderedIndividualRecords)
        {
            individualsStringBuilder.AppendLine(GetIndividualLineItem(individualRecord));
        }

        return individualsStringBuilder.ToString();
    }

    private static string GetIndividualLineItem(IndividualRecord individualRecord)
    {
        var individualLineItemStringBuilder = new StringBuilder();

        individualLineItemStringBuilder.Append($"{individualRecord.Surname}, {individualRecord.Given}");
        individualLineItemStringBuilder.Append($" ({individualRecord.Xref})");

        var birthAndDeathText =
            $" BIRTH: {individualRecord.Birth.GedcomDate.DayMonthYear}" +
            $" {individualRecord.Birth.PlaceStructure.PlaceName}" +
            $" *" +
            $" DEATH: {individualRecord.Death.GedcomDate.DayMonthYear}" +
            $" {individualRecord.Death.PlaceStructure.PlaceName}";

        individualLineItemStringBuilder.Append(birthAndDeathText);

        return individualLineItemStringBuilder.ToString();
    }

    public string GetFamily(string xref)
    {
        var familyRecord = GedcomDocument.GetFamilyRecord(xref);

        if (familyRecord.IsEmpty) return "{}";

        return WriteRecords(familyRecord);
    }

    public string GetFamilies(string query = "")
    {
        var familyRecords = GedcomDocument.GetFamilyRecords(query);

        return WriteRecords(familyRecords);
    }

    public string GetRepository(string xref)
    {
        var repositoryRecord = GedcomDocument.GetRepositoryRecord(xref);

        if (repositoryRecord.IsEmpty) return "{}";

        return WriteRecords(repositoryRecord);
    }

    public string GetRepositories(string query = "")
    {
        var repositoryRecords = GedcomDocument.GetRepositoryRecords(query);

        return WriteRecords(repositoryRecords);
    }

    public string GetSource(string xref)
    {
        var sourceRecord = GedcomDocument.GetSourceRecord(xref);

        if (sourceRecord.IsEmpty) return "{}";

        return WriteRecords(sourceRecord);
    }

    public string GetSources(string query = "")
    {
        var sourceRecords = GedcomDocument.GetSourceRecords(query);

        return WriteRecords(sourceRecords);
    }

    public byte[] GetAsByteArray(string query = "")
    {
        return Encoding.UTF8.GetBytes(GetFamilies());
    }

    private static string WriteRecords(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}

