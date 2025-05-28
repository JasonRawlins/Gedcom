using Gedcom.RecordStructures;
using OfficeOpenXml;

namespace Gedcom.CLI;

public class ExcelWriter : IGedcomWriter
{
    private HeaderTree HeaderTree { get; set; }

    public ExcelWriter(HeaderTree headerTree)
    {
        HeaderTree = headerTree;
        ExcelPackage.License.SetNonCommercialOrganization("Gedcom.NET");
    }

    public byte[] GetIndividuals(List<IndividualListItem> individualListItems)
    {
        // Hard-coding the path to the template file for development. Once the template.xlsx
        // is complete, this will come from project resources.
        using var userTemplatePackage = new ExcelPackage(@"C:\temp\GedcomNET\Resources\GedcomNET-user-template.xlsx");
        var templateSheet = userTemplatePackage.Workbook.Worksheets["Template"];
        using var excelPackage = new ExcelPackage();

        foreach (var individualListItem in individualListItems)
        {
            excelPackage.Workbook.Worksheets.Add(individualListItem.FullName, templateSheet);
            ReplaceDefinedNames(excelPackage.Workbook.Worksheets[individualListItem.FullName], individualListItem);
        }

     

        return excelPackage.GetAsByteArray();
    }

    private void ReplaceDefinedNames(ExcelWorksheet worksheet, IndividualListItem individualListItem)
    {
        foreach (var cell in worksheet.Cells)
        {
            cell.Value = cell.Value switch
            {
                ContentTag.AncestryProfileLink => HeaderTree.Name,
                ContentTag.BirthDate => individualListItem.Birth.DayMonthYear,
                ContentTag.BirthPlace => individualListItem.BirthPlace,
                ContentTag.DeathDate => individualListItem.Death.DayMonthYear,
                ContentTag.DeathPlace => individualListItem.DeathPlace,
                ContentTag.FullName => individualListItem.FullName,
                ContentTag.Given => individualListItem.Given,
                ContentTag.Surname => individualListItem.Surname,
                ContentTag.TreeName => HeaderTree.Name,
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
