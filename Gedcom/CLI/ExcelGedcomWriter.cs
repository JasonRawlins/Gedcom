using OfficeOpenXml;

namespace Gedcom.CLI;

public class ExcelGedcomWriter : IGedcomWriter
{
    private Gedcom Gedcom { get; set; }

    public ExcelGedcomWriter(Gedcom gedcom)
    {
        Gedcom = gedcom;
        ExcelPackage.License.SetNonCommercialOrganization("Gedcom.NET");
    }

    public string GetIndividual(string xref)
    {
        throw new NotImplementedException();
    }

    public string GetIndividuals(string query = "")
    {
        throw new NotImplementedException();
    }

    public string GetFamily(string xref)
    {
        throw new NotImplementedException();
    }

    public string GetFamilies(string query = "")
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

    public byte[] GetAsByteArray(string query = "")
    {
        var individualRecords = Gedcom.GetIndividualRecords(query);
        var individualListItems = individualRecords.Select(ir => new IndividualListItem(ir)).ToList();

        // Hard-coding the path to the template file for development.
        // Once the template.xlsx is complete, this will come from project resources.
        using var userTemplatePackage = new ExcelPackage(@"C:\temp\GedcomNET\Resources\GedcomNET-template.xlsx");
        var templateSheet = userTemplatePackage.Workbook.Worksheets["Template"];
        using var excelPackage = new ExcelPackage();
        var targetSheet = excelPackage.Workbook.Worksheets.Add($"{Gedcom.Header.Source.Tree.Name} individuals", templateSheet);

        var templateRow = 2; // The row containing the template values

        for (int i = 0; i < individualListItems.Count; i++)
        {
            var individualListItem = individualListItems[i];

            var targetRow = templateRow + i + 1; // The row where the copied template should go

            // Copy the template row to the next available row
            targetSheet.Cells[templateRow, 1, templateRow, targetSheet.Dimension.End.Column].Copy(targetSheet.Cells[targetRow, 1]);

            ReplaceTemplateValues(targetSheet, individualListItem, targetRow);
        }

        targetSheet.DeleteRow(templateRow);

        return excelPackage.GetAsByteArray();
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
                ContentTag.BirthDate => individualListItem.Birth.DayMonthYear,
                ContentTag.BirthPlace => individualListItem.BirthPlace,
                ContentTag.DeathDate => individualListItem.Death.DayMonthYear,
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
        public const string AncestryProfileLink = "_ANCESTRY_PROFILE_LINK_";
        public const string BirthDate = "_BIRTH_DATE_";
        public const string BirthPlace = "_BIRTH_PLACE_";
        public const string DeathDate = "_DEATH_DATE_";
        public const string DeathPlace = "_DEATH_PLACE_";
        public const string FullName = "_FULL_NAME_";
        public const string Given = "_GIVEN_";
        public const string Surname = "_SURNAME_";
        public const string TreeName = "_TREE_NAME_";
    }
}