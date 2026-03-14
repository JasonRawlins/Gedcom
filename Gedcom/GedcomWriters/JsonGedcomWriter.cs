using Gedcom.DTOs;
using System.Text;
using System.Text.Json;

namespace Gedcom.GedcomWriters;

public class JsonGedcomWriter(GedcomDocument gedcom) : IGedcomWriter
{
    public GedcomDocument GedcomDocument { get; set; } = gedcom;

    public byte[] GetIndividuals(string xref = "")
    {
        var individualRecords = GedcomDocument.GetIndividualRecords();

        if (!string.IsNullOrEmpty(xref))
        {
            var individualRecord = individualRecords.SingleOrDefault(ir => ir.Xref == xref);
            if (individualRecord == null)
            {
                individualRecords = [];
            }
            else
            {
                individualRecords = [individualRecord];
            }
        }

        var individualDtos = individualRecords.Select(ir => new IndividualDto(ir));

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(individualDtos, GedcomDto.SerializationOptions));
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

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(repositoryRecords, GedcomDto.SerializationOptions));
    }

    public byte[] GetSources(string xref = "")
    {
        // TODO: Filter by xref after retrieving, if necessary.
        var sourceRecords = GedcomDocument.GetSourceRecords();

        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(sourceRecords, GedcomDto.SerializationOptions));
    }
}

