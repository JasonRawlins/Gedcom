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
        var repositoryDtos = GetRepositoryDtos(xref);

        var repositoryStringBuilder = new StringBuilder();
        foreach (var repositoryDto in repositoryDtos)
        {
            repositoryStringBuilder.AppendLine(GetRepositoryLineItem(repositoryDto));
        }

        return Encoding.UTF8.GetBytes(repositoryStringBuilder.ToString());
    }

    public override byte[] GetSources(string xref = "")
    {
        var sourceDtos = GetSourceDtos(xref);

        var sourceStringBuilder = new StringBuilder();
        foreach (var sourceDto in sourceDtos)
        {
            sourceStringBuilder.AppendLine(GetSourceLineItem(sourceDto));
        }

        return Encoding.UTF8.GetBytes(sourceStringBuilder.ToString());
    }

    private static string GetIndividualLineItem(IndividualDto individualDto)
    {
        var individualLineItemStringBuilder = new StringBuilder();

        individualLineItemStringBuilder.Append($"({individualDto.Xref}) ");
        individualLineItemStringBuilder.Append($"{individualDto.Surname}, {individualDto.Given}");

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
            familyLineItemStringBuilder.Append($"Husband: ({husbandDto.Xref}) {husbandDto.FullName}.");
        }

        var wifeIndividualRecord = GedcomDocument.GetIndividualRecords().SingleOrDefault(r => r.Xref == familyDto.Wife);
        if (wifeIndividualRecord == null || wifeIndividualRecord.IsEmpty)
        {
            familyLineItemStringBuilder.Append(" Wife: Unknown.");
        }
        else
        {
            var wifeDto = new IndividualDto(wifeIndividualRecord);
            familyLineItemStringBuilder.Append($" Wife: ({wifeDto.Xref}) {wifeDto.FullName}.");
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
                    childNames.Add($"({childDto.Xref}) {childDto.FullName}");
                }
            }

            familyLineItemStringBuilder.Append(string.Join(", ", childNames));
            familyLineItemStringBuilder.AppendLine("]");
        }

        return familyLineItemStringBuilder.ToString();
    }

    private static string? GetRepositoryLineItem(RepositoryDto repositoryDto)
    {
        return $"({repositoryDto.Xref}) {repositoryDto.Name}: {repositoryDto.Note}";
    }

    private static string? GetSourceLineItem(SourceDto sourceDto)
    {
        return $"({sourceDto.Xref}) {sourceDto.Title}: {sourceDto.Note}";
    }
}