using ClosedXML.Excel;
using Gedcom.DTOs;
using System.Text;

namespace Gedcom.GedcomWriters;

public class ExcelGedcomWriter : IGedcomWriter
{
    public GedcomDocument GedcomDocument { get; set; }

    public ExcelGedcomWriter(GedcomDocument gedcom)
    {
        GedcomDocument = gedcom;
    }

    public byte[] GetIndividuals(string xref = "")
    {
        var individualRecords = GedcomDocument.GetIndividualRecords();

        if (!string.IsNullOrEmpty(xref))
        {
            var individualRecord = individualRecords.SingleOrDefault(ir => ir.Xref == xref);
            if (individualRecord != null)
            {
                individualRecords = [individualRecord];
            }
            else
            {
                individualRecords = [];
            }
        }

        var individualDtos = individualRecords.Select(ir => new IndividualDto(ir)).ToList();

        using var templateStream = new MemoryStream(Properties.Resources.GedcomNetXlsxTemplate);
        using var templateWorkbook = new XLWorkbook(templateStream);
        using var workbook = new XLWorkbook();

        var templateSheet = templateWorkbook.Worksheet("Template");
        var targetSheet = templateSheet.CopyTo(workbook, $"{GedcomDocument.Header.Source.Tree.Name} individuals");

        var templateRow = 2;
        var lastUsedColumn = targetSheet.LastColumnUsed()!.ColumnNumber();

        for (int i = 0; i < individualDtos.Count; i++)
        {
            var individualDto = individualDtos[i];
            var targetRow = templateRow + i + 1;

            targetSheet.Row(templateRow).CopyTo(targetSheet.Row(targetRow));
            ReplaceTemplateValues(targetSheet, individualDto, targetRow, lastUsedColumn);
        }

        targetSheet.Row(templateRow).Delete();

        using var outputStream = new MemoryStream();
        workbook.SaveAs(outputStream);
        return outputStream.ToArray();
    }

    public byte[] GetFamilies(string xref = "")
    {
        throw new NotImplementedException();
    }

    public byte[] GetRepositories(string xref = "")
    {
        throw new NotImplementedException();
    }

    public byte[] GetSources(string xref = "")
    {
        throw new NotImplementedException();
    }

    private void ReplaceTemplateValues(IXLWorksheet worksheet, IndividualDto individualDto, int rowNumber, int lastUsedColumn)
    {
        for (int column = 1; column <= lastUsedColumn; column++)
        {
            var cell = worksheet.Cell(rowNumber, column);
            var value = cell.GetString();

            cell.Value = value switch
            {
                ContentTag.AncestryProfileLink => GedcomDocument.Header.Source.Tree.Name,
                ContentTag.BirthDate => individualDto.Birth?.Date.DayMonthYear,
                ContentTag.BirthPlace => individualDto.Birth?.Place?.Name,
                ContentTag.DeathDate => individualDto.Death?.Date.DayMonthYear,
                ContentTag.DeathPlace => individualDto.Death?.Place?.Name,
                ContentTag.FullName => individualDto.FullName,
                ContentTag.Given => individualDto.Given,
                ContentTag.Surname => individualDto.Surname,
                ContentTag.TreeName => GedcomDocument.Header.Source.Tree.Name,
                ContentTag.Xref => individualDto.Xref,
                _ => value,
            };
        }
    }

    private static class ContentTag
    {
        public const string AncestryProfileLink = "{{ANCESTRY_PROFILE_LINK}}";
        public const string BirthDate = "{{BIRTH_DATE}}";
        public const string BirthPlace = "{{BIRTH_PLACE}}";
        public const string DeathDate = "{{DEATH_DATE}}";
        public const string DeathPlace = "{{DEATH_PLACE}}";
        public const string FullName = "{{FULL_NAME}}";
        public const string Given = "{{GIVEN}}";
        public const string Surname = "{{SURNAME}}";
        public const string TreeName = "{{TREE_NAME}}";
        public const string Xref = "{{XREF}}";
    }
}