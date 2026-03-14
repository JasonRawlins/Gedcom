using Gedcom.DTOs;
using Gedcom.RecordStructures;
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
        // TODO: Filter by xref after retrieving, if necessary.
        var familyRecords = GedcomDocument.GetFamilyRecords();

        var familyRecord = GedcomDocument.GetFamilyRecord(xref);

        if (familyRecord.IsEmpty) return [];

        return Encoding.UTF8.GetBytes(GetFamilyLineItem(familyRecord));
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

    public string GetFamilyLineItem(FamilyRecord familyRecord)
    {
        var familyLineItemStringBuilder = new StringBuilder();

        var husbandIndividualRecord = GedcomDocument.GetIndividualRecords().Single(r => r.Xref == familyRecord.Husband);
        if (husbandIndividualRecord.IsEmpty)
        {
            familyLineItemStringBuilder.Append("Husband: Unknown.");
        }
        else
        {
            var husbandDto = new IndividualDto(husbandIndividualRecord);
            familyLineItemStringBuilder.Append($"Husband: {husbandDto.FullName} ({husbandDto.Xref}).");
        }

        var wifeIndividualRecord = GedcomDocument.GetIndividualRecords().Single(r => r.Xref == familyRecord.Wife);
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
                var childIndividualRecord = GedcomDocument.GetIndividualRecords().Single(r => r.Xref == childXref);
                if (!childIndividualRecord.IsEmpty)
                {
                    var childDto = new IndividualDto(childIndividualRecord);
                    childNames.Add($"{childDto.FullName} ({childDto.Xref})");
                }
            }

            familyLineItemStringBuilder.Append(string.Join(", ", childNames));
            familyLineItemStringBuilder.Append(']');
        }

        return familyLineItemStringBuilder.ToString();
    }

}

