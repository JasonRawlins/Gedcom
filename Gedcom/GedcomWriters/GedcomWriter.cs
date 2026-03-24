using Gedcom.DTOs;
using Gedcom.RecordStructures;

namespace Gedcom.GedcomWriters;

// 2026-03-24. There is a lot of code duplication in this file. I could make a generic for 
// all of them, but that will change the core code too much. I expect these GedcomWriters
// to change significantly in the future, so I'm not going to make the methods generic
// for now.
public abstract class GedcomWriter : IGedcomWriter
{
    public GedcomDocument GedcomDocument { get; set; }

    public GedcomWriter(GedcomDocument gedcomDocument) => GedcomDocument = gedcomDocument;

    public abstract byte[] GetIndividuals(string xref = "");

    public abstract byte[] GetFamilies(string xref = "");

    public abstract byte[] GetRepositories(string xref = "");

    public abstract byte[] GetSources(string xref = "");

    public static IGedcomWriter Create(GedcomDocument gedcom, string format)
    {
        return format switch
        {
            Constants.Excel => new ExcelGedcomWriter(gedcom),
            Constants.Html => new HtmlGedcomWriter(gedcom),
            Constants.Json => new JsonGedcomWriter(gedcom),
            Constants.Text => new TextGedcomWriter(gedcom),
            _ => throw new NotSupportedException($"The format '{format}' is not supported."),
        };
    }

    protected List<IndividualDto> GetIndividualDtos(string xref = "")
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

        return [.. individualRecords.Select(ir => new IndividualDto(ir))];
    }

    protected List<FamilyDto> GetFamilyDtos(string xref = "")
    {
        var familyRecords = GedcomDocument.GetFamilyRecords();

        if (!string.IsNullOrEmpty(xref))
        {
            var familyRecord = familyRecords.SingleOrDefault(fr => fr.Xref == xref);
            if (familyRecord == null)
            {
                familyRecords = [];
            }
            else
            {
                familyRecords = [familyRecord];
            }
        }

        //var familyManager = new FamilyManager(GedcomDocument);

        //var families = new List<Entities.Family>();

        //foreach (var familyRecord in familyRecords)
        //{
        //    var family = familyManager.CreateFamily(familyRecord.Xref, Generation.Current, Generation.Child);
        //    families.Add(family);
        //}

        return [.. familyRecords.Select(fr => new FamilyDto(fr))];
    }

    protected List<RepositoryDto> GetRepositoryDtos(string xref = "")
    {
        var repositoryRecords = GedcomDocument.GetRepositoryRecords();

        if (!string.IsNullOrEmpty(xref))
        {
            var repositoryRecord = repositoryRecords.SingleOrDefault(rr => rr.Xref == xref);
            if (repositoryRecord == null)
            {
                repositoryRecords = [];
            }
            else
            {
                repositoryRecords = [repositoryRecord];
            }
        }

        return [.. repositoryRecords.Select(rr => new RepositoryDto(rr))];
    }

    protected List<SourceDto> GetSourceDtos(string xref = "")
    {
        var sourceRecords = GedcomDocument.GetSourceRecords();

        if (!string.IsNullOrEmpty(xref))
        {
            var sourceRecord = sourceRecords.SingleOrDefault(sr => sr.Xref == xref);
            if (sourceRecord == null)
            {
                sourceRecords = [];
            }
            else
            {
                sourceRecords = [sourceRecord];
            }
        }

        return [.. sourceRecords.Select(sr => new SourceDto(sr))];
    }
}