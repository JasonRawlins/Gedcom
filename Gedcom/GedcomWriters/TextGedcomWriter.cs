using Gedcom.DTOs;
using System.Text;

namespace Gedcom.GedcomWriters;

public class TextGedcomWriter : GedcomWriter
{
    public TextGedcomWriter(GedcomDocument gedcomDocument) : base(gedcomDocument)
    {
        GedcomDocument = gedcomDocument;
    }

    public override byte[] GetIndividuals(string xref = "")
    {
        var individualDtos = GetIndividualDtos(xref);

        var individualsStringBuilder = new StringBuilder();
        foreach (var individualDto in individualDtos)
        {
            individualsStringBuilder.AppendLine(GetIndividualLineItem(individualDto));
        }

        return Encoding.UTF8.GetBytes(individualsStringBuilder.ToString());
    }

    public override byte[] GetFamilies(string xref = "")
    {
        var familyDtos = GetFamilyDtos(xref);

        var familiesStringBuilder = new StringBuilder();
        foreach (var familyDto in familyDtos)
        {
            familiesStringBuilder.Append(GetFamilyLineItem(familyDto));
        }

        return Encoding.UTF8.GetBytes(familiesStringBuilder.ToString());
    }


    public override byte[] GetRepositories(string xref = "")
    {
        var repositoryRecords = GedcomDocument.GetRepositoryRecords();

        return []; // WriteRecords(repositoryRecords);
    }

    public override byte[] GetSources(string xref = "")
    {
        var sourceRecords = GedcomDocument.GetSourceRecords();

        return []; // WriteRecords(sourceRecords);
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

    public string GetFamilyLineItem(FamilyDto familyDto)
    {
        var familyLineItemStringBuilder = new StringBuilder();

        familyLineItemStringBuilder.Append($"({familyDto.Xref}) ");

        var husbandIndividualRecord = GedcomDocument.GetIndividualRecords().SingleOrDefault(r => r.Xref == familyDto.Husband);
        if (husbandIndividualRecord == null || husbandIndividualRecord.IsEmpty)
        {
            familyLineItemStringBuilder.Append("Husband: Unknown.");
        }
        else
        {
            var husbandDto = new IndividualDto(husbandIndividualRecord);
            familyLineItemStringBuilder.Append($"Husband: {husbandDto.FullName} ({husbandDto.Xref}).");
        }

        var wifeIndividualRecord = GedcomDocument.GetIndividualRecords().SingleOrDefault(r => r.Xref == familyDto.Wife);
        if (wifeIndividualRecord == null || wifeIndividualRecord.IsEmpty)
        {
            familyLineItemStringBuilder.Append(" Wife: Unknown.");
        }
        else
        {
            var wifeDto = new IndividualDto(wifeIndividualRecord);
            familyLineItemStringBuilder.Append($" Wife: {wifeDto.FullName} ({wifeDto.Xref}).");
        }

        if (familyDto.Children?.Count == 0)
        {
            familyLineItemStringBuilder.Append(" No children.");
        }
        else if (familyDto.Children?.Count > 0)
        {
            familyLineItemStringBuilder.Append(" Children: [");

            var childNames = new List<string>();
            foreach (var childXref in familyDto.Children!)
            {
                var childIndividualRecord = GedcomDocument.GetIndividualRecords().Single(r => r.Xref == childXref);
                if (!childIndividualRecord.IsEmpty)
                {
                    var childDto = new IndividualDto(childIndividualRecord);
                    childNames.Add($"{childDto.FullName} ({childDto.Xref})");
                }
            }

            familyLineItemStringBuilder.Append(string.Join(", ", childNames));
            familyLineItemStringBuilder.AppendLine("]");
        }

        return familyLineItemStringBuilder.ToString();
    }

}