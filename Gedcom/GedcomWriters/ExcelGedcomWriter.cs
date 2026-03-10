using ClosedXML.Excel;

namespace Gedcom.GedcomWriters;

public class ExcelGedcomWriter : IGedcomWriter
{
    public GedcomDocument GedcomDocument { get; set; }

    public ExcelGedcomWriter(GedcomDocument gedcom)
    {
        GedcomDocument = gedcom;
    }

    public byte[] GetIndividual(string xref)
    {
        throw new NotImplementedException();
    }

    public byte[] GetIndividuals(string query = "")
    {
        var individualRecords = GedcomDocument.GetIndividualRecords();
        var orderedIndividualListItems = individualRecords
            .Select(ir => new IndividualListItem(ir))
            .OrderBy(ir => ir.Surname)
            .ThenBy(ir => ir.Given)
            .ToList();

        using var templateStream = new MemoryStream(Properties.Resources.GedcomNetXlsxTemplate);
        using var templateWorkbook = new XLWorkbook(templateStream);
        using var workbook = new XLWorkbook();

        var templateSheet = templateWorkbook.Worksheet("Template");
        var targetSheet = templateSheet.CopyTo(workbook, $"{GedcomDocument.Header.Source.Tree.Name} individuals");

        var templateRow = 2;
        var lastUsedColumn = targetSheet.LastColumnUsed()!.ColumnNumber();

        for (int i = 0; i < orderedIndividualListItems.Count; i++)
        {
            var individualListItem = orderedIndividualListItems[i];
            var targetRow = templateRow + i + 1;

            targetSheet.Row(templateRow).CopyTo(targetSheet.Row(targetRow));
            ReplaceTemplateValues(targetSheet, individualListItem, targetRow, lastUsedColumn);
        }

        targetSheet.Row(templateRow).Delete();

        using var outputStream = new MemoryStream();
        workbook.SaveAs(outputStream);
        return outputStream.ToArray();
    }

    public byte[] GetFamily(string xref)
    {
        throw new NotImplementedException();
    }

    public byte[] GetFamilies(string query = "")
    {
        throw new NotImplementedException();
    }

    public string GetRepository(string xref)
    {
        throw new NotImplementedException();
    }

    public string GetRepositories(string query = "")
    {
        throw new NotImplementedException();
    }

    public string GetSource(string xref)
    {
        throw new NotImplementedException();
    }

    public string GetSources(string query = "")
    {
        throw new NotImplementedException();
    }

    private void ReplaceTemplateValues(IXLWorksheet worksheet, IndividualListItem individualListItem, int rowNumber, int lastUsedColumn)
    {
        for (int column = 1; column <= lastUsedColumn; column++)
        {
            var cell = worksheet.Cell(rowNumber, column);
            var value = cell.GetString();

            cell.Value = value switch
            {
                ContentTag.AncestryProfileLink => GedcomDocument.Header.Source.Tree.Name,
                ContentTag.BirthDate => individualListItem.Birthdate,
                ContentTag.BirthPlace => individualListItem.BirthPlace,
                ContentTag.DeathDate => individualListItem.DeathDate,
                ContentTag.DeathPlace => individualListItem.DeathPlace,
                ContentTag.FullName => individualListItem.FullName,
                ContentTag.Given => individualListItem.Given,
                ContentTag.Surname => individualListItem.Surname,
                ContentTag.TreeName => GedcomDocument.Header.Source.Tree.Name,
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
    }
}