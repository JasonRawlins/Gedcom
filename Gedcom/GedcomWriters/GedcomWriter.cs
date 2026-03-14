using Gedcom.DTOs;

namespace Gedcom.GedcomWriters;

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
}