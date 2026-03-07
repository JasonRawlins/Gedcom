using OfficeOpenXml;

namespace Gedcom.GedcomWriters;

public class ExcelGedcomWriter : IGedcomWriter
{
    private GedcomDocument Gedcom { get; set; }

    public ExcelGedcomWriter(GedcomDocument gedcom)
    {
        Gedcom = gedcom;
        ExcelPackage.License.SetNonCommercialOrganization("Gedcom.NET");
    }

    public byte[] GetIndividual(string xref)
    {
        throw new NotImplementedException();
    }

    public byte[] GetIndividuals(string query = "")
    {
        var individualRecords = Gedcom.GetIndividualRecords(query);
        var individualListItems = individualRecords.Select(ir => new IndividualListItem(ir)).ToList();
        var orderedIndividualListItems = individualListItems.OrderBy(ir => ir.Surname).ThenBy(ir => ir.Given).ToList();

        using var userTemplatePackage = new ExcelPackage(new MemoryStream(Properties.Resources.GedcomNetXlsxTemplate));
        var templateSheet = userTemplatePackage.Workbook.Worksheets["Template"];
        using var excelPackage = new ExcelPackage();
        var targetSheet = excelPackage.Workbook.Worksheets.Add($"{Gedcom.Header.Source.Tree.Name} individuals", templateSheet);

        var templateRow = 2; // The row containing the template values

        for (int i = 0; i < orderedIndividualListItems.Count; i++)
        {
            var individualListItem = orderedIndividualListItems[i];

            var targetRow = templateRow + i + 1; // The row where the copied template should go

            // Copy the template row to the next available row
            targetSheet.Cells[templateRow, 1, templateRow, targetSheet.Dimension.End.Column].Copy(targetSheet.Cells[targetRow, 1]);

            ReplaceTemplateValues(targetSheet, individualListItem, targetRow);
        }

        targetSheet.DeleteRow(templateRow);

        return excelPackage.GetAsByteArray();
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

    private void ReplaceTemplateValues(ExcelWorksheet worksheet, IndividualListItem individualListItem, int rowNumber)
    {
        // Loop through the cells in the row
        for (int column = worksheet.Dimension.Start.Column; column <= worksheet.Dimension.End.Column; column++)
        {
            var cell = worksheet.Cells[rowNumber, column];

            cell.Value = cell.Value switch
            {
                ContentTag.AncestryProfileLink => Gedcom.Header.Source.Tree.Name,
                ContentTag.BirthDate => individualListItem.Birthdate,
                ContentTag.BirthPlace => individualListItem.BirthPlace,
                ContentTag.DeathDate => individualListItem.DeathDate,
                ContentTag.DeathPlace => individualListItem.DeathPlace,
                ContentTag.FullName => individualListItem.FullName,
                ContentTag.Given => individualListItem.Given,
                ContentTag.Surname => individualListItem.Surname,
                ContentTag.TreeName => Gedcom.Header.Source.Tree.Name,
                _ => cell.Value,
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