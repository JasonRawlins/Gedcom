using ClosedXML.Excel;
using Gedcom.DTOs;

namespace Gedcom.GedcomWriters;

public class ExcelGedcomWriter : GedcomWriter
{
    public ExcelGedcomWriter(GedcomDocument gedcomDocument) : base(gedcomDocument)
    {
        GedcomDocument = gedcomDocument;
    }

    public override byte[] GetIndividuals(string xref = "")
    {
        var individualDtos = GetIndividualDtos(xref);

        using var templateStream = new MemoryStream(Properties.Resources.GedcomNetXlsxIndividualsTemplate);
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
            ReplaceTemplateValuesForIndividual(targetSheet, individualDto, targetRow, lastUsedColumn);
        }

        targetSheet.Row(templateRow).Delete();

        using var outputStream = new MemoryStream();
        workbook.SaveAs(outputStream);

        return outputStream.ToArray();
    }

    public override byte[] GetFamilies(string xref = "")
    {
        return [];
        //throw new NotImplementedException();

        //var familyRecords = new List<FamilyRecord>(); 

        //if (!string.IsNullOrEmpty(xref))
        //{
        //    var familyRecord = familyRecords.SingleOrDefault(ir => ir.Xref == xref);
        //    if (familyRecord != null)
        //    {
        //        familyRecords = [familyRecord];
        //    }
        //}
        //else
        //{
        //    familyRecords = GedcomDocument.GetFamilyRecords();
        //}

        //var familyDtos = familyRecords.Select(fr => new FamilyDto(fr)).ToList();

        //using var templateStream = new MemoryStream(Properties.Resources.GedcomNetFamiliesXlsxTemplate);
        //using var templateWorkbook = new XLWorkbook(templateStream);
        //using var workbook = new XLWorkbook();

        //var templateSheet = templateWorkbook.Worksheet("Template");
        //var targetSheet = templateSheet.CopyTo(workbook, $"{GedcomDocument.Header.Source.Tree.Name} individuals");

        //var templateRow = 2;
        //var lastUsedColumn = targetSheet.LastColumnUsed()!.ColumnNumber();

        //for (int i = 0; i < familyDtos.Count; i++)
        //{
        //    var familyDto = familyDtos[i];
        //    var targetRow = templateRow + i + 1;

        //    targetSheet.Row(templateRow).CopyTo(targetSheet.Row(targetRow));
        //    //ReplaceTemplateValues(targetSheet, familyDto, targetRow, lastUsedColumn);
        //}

        //targetSheet.Row(templateRow).Delete();

        //using var outputStream = new MemoryStream();
        //workbook.SaveAs(outputStream);
        //return outputStream.ToArray();
    }

    public override byte[] GetRepositories(string xref = "")
    {
        var repositoryDtos = GetRepositoryDtos(xref);

        using var templateStream = new MemoryStream(Properties.Resources.GedcomNetXlsxRepositoriesTemplate);
        using var templateWorkbook = new XLWorkbook(templateStream);
        using var workbook = new XLWorkbook();

        var templateSheet = templateWorkbook.Worksheet("Template");
        var targetSheet = templateSheet.CopyTo(workbook, $"{GedcomDocument.Header.Source.Tree.Name} repositories");

        var templateRow = 2;
        var lastUsedColumn = targetSheet.LastColumnUsed()!.ColumnNumber();

        for (int i = 0; i < repositoryDtos.Count; i++)
        {
            var repositoryDto = repositoryDtos[i];
            var targetRow = templateRow + i + 1;

            targetSheet.Row(templateRow).CopyTo(targetSheet.Row(targetRow));
            ReplaceTemplateValuesForRepository(targetSheet, repositoryDto, targetRow, lastUsedColumn);
        }

        targetSheet.Row(templateRow).Delete();

        using var outputStream = new MemoryStream();
        workbook.SaveAs(outputStream);

        return outputStream.ToArray();
    }

    public override byte[] GetSources(string xref = "")
    {
        var sourcesDtos = GetSourceDtos(xref);

        using var templateStream = new MemoryStream(Properties.Resources.GedcomNetXlsxSourcesTemplate);
        using var templateWorkbook = new XLWorkbook(templateStream);
        using var workbook = new XLWorkbook();

        var templateSheet = templateWorkbook.Worksheet("Template");
        var targetSheet = templateSheet.CopyTo(workbook, $"{GedcomDocument.Header.Source.Tree.Name} repositories");

        var templateRow = 2;
        var lastUsedColumn = targetSheet.LastColumnUsed()!.ColumnNumber();

        for (int i = 0; i < sourcesDtos.Count; i++)
        {
            var sourceDto = sourcesDtos[i];
            var targetRow = templateRow + i + 1;

            targetSheet.Row(templateRow).CopyTo(targetSheet.Row(targetRow));
            ReplaceTemplateValuesForSource(targetSheet, sourceDto, targetRow, lastUsedColumn);
        }

        targetSheet.Row(templateRow).Delete();

        using var outputStream = new MemoryStream();
        workbook.SaveAs(outputStream);

        return outputStream.ToArray();
    }

    private void ReplaceTemplateValuesForIndividual(IXLWorksheet worksheet, IndividualDto individualDto, int rowNumber, int lastUsedColumn)
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

    private static void ReplaceTemplateValuesForRepository(IXLWorksheet worksheet, RepositoryDto repositoryDto, int rowNumber, int lastUsedColumn)
    {
        for (int column = 1; column <= lastUsedColumn; column++)
        {
            var cell = worksheet.Cell(rowNumber, column);
            var value = cell.GetString();

            cell.Value = value switch
            {
                ContentTag.Name => repositoryDto.Name,
                ContentTag.Xref => repositoryDto.Xref,
                _ => value,
            };
        }
    }

    private static void ReplaceTemplateValuesForSource(IXLWorksheet worksheet, SourceDto sourceDto, int rowNumber, int lastUsedColumn)
    {
        for (int column = 1; column <= lastUsedColumn; column++)
        {
            var cell = worksheet.Cell(rowNumber, column);
            var value = cell.GetString();

            cell.Value = value switch
            {
                ContentTag.Title => sourceDto.Title,
                ContentTag.Xref => sourceDto.Xref,
                _ => value,
            };
        }
    }
}