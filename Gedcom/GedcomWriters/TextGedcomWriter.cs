using Gedcom.RecordStructures;
using System.Text;
using System.Text.Json;

namespace Gedcom.GedcomWriters;

public class TextGedcomWriter(GedcomDocument gedcom) : IGedcomWriter
{
    private GedcomDocument GedcomDocument { get; set; } = gedcom;

    public byte[] GetIndividual(string xref)
    {
        var individualRecord = GedcomDocument.GetIndividualRecord(xref);
        if (individualRecord.IsEmpty) return [];

        var individualDto = new IndividualDto(individualRecord);

        return Encoding.UTF8.GetBytes(GetIndividualLineItem(individualDto));
    }

    public byte[] GetIndividuals(string query = "")
    {
        var individualRecords = GedcomDocument.GetIndividualRecords(query);
        if (individualRecords.Count.Equals(0)) return [];

        var individualsStringBuilder = new StringBuilder();
        foreach (var individualRecord in individualRecords)
        {
            var individualDto = new IndividualDto(individualRecord);
            individualsStringBuilder.AppendLine(GetIndividualLineItem(individualDto));
        }

        return Encoding.UTF8.GetBytes(individualsStringBuilder.ToString());
    }

    private static string GetIndividualLineItem(IndividualDto individualDto)
    {
        var individualLineItemStringBuilder = new StringBuilder();

        individualLineItemStringBuilder.Append($"{individualDto.Surname}, {individualDto.Given}");
        individualLineItemStringBuilder.Append($" ({individualDto.Xref})");

        var birthAndDeathText =
            $" BIRTH: {individualDto.Birth?.Date.DayMonthYear ?? "Unknown birthdate"}" +
            $" {individualDto.Birth?.Place?.Name ?? "Unknown birth place"}" +
            $" *" +
            $" DEATH: {individualDto.Death?.Date.DayMonthYear ?? "Unknown death date"}" +
            $" {individualDto.Death?.Place?.Name ?? "Unknown death place"}";

        individualLineItemStringBuilder.Append(birthAndDeathText);

        return individualLineItemStringBuilder.ToString();
    }

    public byte[] GetFamily(string xref)
    {
        var familyRecord = GedcomDocument.GetFamilyRecord(xref);

        if (familyRecord.IsEmpty) return [];

        return Encoding.UTF8.GetBytes(GetFamilyLineItem(familyRecord));
    }

    public byte[] GetFamilies(string query = "")
    {
        var familyRecords = GedcomDocument.GetFamilyRecords(query);

        return []; // WriteRecords(familyRecords);
    }

    public string GetFamilyLineItem(FamilyRecord familyRecord)
    {
        var familyLineItemStringBuilder = new StringBuilder();

        var husbandIndividualRecord = GedcomDocument.GetIndividualRecord(familyRecord.Husband);
        if (husbandIndividualRecord.IsEmpty)
        {
            familyLineItemStringBuilder.Append("Husband: Unknown.");
        }
        else
        {
            var husbandDto = new IndividualDto(husbandIndividualRecord);
            familyLineItemStringBuilder.Append($"Husband: {husbandDto.FullName} ({husbandDto.Xref}).");
        }

        var wifeIndividualRecord = GedcomDocument.GetIndividualRecord(familyRecord.Wife);
        if (wifeIndividualRecord.IsEmpty)
        {
            familyLineItemStringBuilder.Append(" Wife: Unknown.");
        }
        else
        {
            var wifeDto = new IndividualDto(wifeIndividualRecord);
            familyLineItemStringBuilder.Append($" Wife: {wifeDto.FullName} ({wifeDto.Xref}).");
        }

        if (familyRecord.Children.Count == 0)
        {
            familyLineItemStringBuilder.Append(" No children.");
        }
        else
        {
            familyLineItemStringBuilder.Append(" Children: [");

            var childNames = new List<string>();
            foreach (var childXref in familyRecord.Children)
            {
                var childIndividualRecord = GedcomDocument.GetIndividualRecord(childXref);
                if (!childIndividualRecord.IsEmpty)
                {
                    var childDto = new IndividualDto(childIndividualRecord);
                    childNames.Add($"{childDto.FullName} ({childDto.Xref})");
                }
            }

            familyLineItemStringBuilder.Append(string.Join(", ", childNames));
            familyLineItemStringBuilder.Append("]");
        }

        return familyLineItemStringBuilder.ToString();
    }

    public string GetRepository(string xref)
    {
        var repositoryRecord = GedcomDocument.GetRepositoryRecord(xref);

        if (repositoryRecord.IsEmpty) return "{}";

        return ""; // WriteRecords(repositoryRecord);
    }

    public string GetRepositories(string query = "")
    {
        var repositoryRecords = GedcomDocument.GetRepositoryRecords(query);

        return ""; // WriteRecords(repositoryRecords);
    }

    public string GetSource(string xref)
    {
        var sourceRecord = GedcomDocument.GetSourceRecord(xref);

        if (sourceRecord.IsEmpty) return "{}";

        return ""; // WriteRecords(sourceRecord);
    }

    public string GetSources(string query = "")
    {
        var sourceRecords = GedcomDocument.GetSourceRecords(query);

        return ""; // WriteRecords(sourceRecords);
    }

    private static string WriteRecords(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}

