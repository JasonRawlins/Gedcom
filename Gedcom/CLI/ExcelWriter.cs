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
        using (var excelPackage = new ExcelPackage())
        {
            foreach (var individualListItem in individualListItems) 
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add(individualListItem.FullName);
                worksheet.Cells["A1"].Value = HeaderTree.Name;
                worksheet.Cells["A3"].Value = "Born";
                worksheet.Cells["B3"].Value = $"{individualListItem.Birth.DayMonthYear} at {individualListItem.BirthPlace}";
                worksheet.Cells["A4"].Value = "Death";
                worksheet.Cells["B4"].Value = $"{individualListItem.Death.DayMonthYear} at {individualListItem.DeathPlace}";

            }

            return excelPackage.GetAsByteArray();
        }       
    }

    //private void ReplaceDefinedNames(Cell cell, IndividualListItem individualListItem)
    //{
    //    string cellValue = GetCellValue(cell);

    //    cell.CellValue = cellValue switch
    //    {
    //        ContentTag.AncestryProfileLink => new CellValue(HeaderTree.Name),
    //        ContentTag.BirthDate => new CellValue(individualListItem.Birth.DayMonthYear),
    //        ContentTag.BirthPlace => new CellValue(individualListItem.BirthPlace),
    //        ContentTag.DeathDate => new CellValue(individualListItem.Death.DayMonthYear),
    //        ContentTag.DeathPlace => new CellValue(individualListItem.DeathPlace),
    //        ContentTag.FullName => new CellValue(individualListItem.FullName),
    //        ContentTag.Given => new CellValue(individualListItem.Given),
    //        ContentTag.Surname => new CellValue(individualListItem.Surname),
    //        ContentTag.TreeName => new CellValue(HeaderTree.Name),
    //        _ => new CellValue(cellValue),
    //    };

    //    // cell.DateType() has to be placed after the cell.CellValue() lines above. I don't
    //    // know why order matters, but it throws an error if I don't.
    //    cell.DataType = new EnumValue<CellValues>(CellValues.String);
    //}

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
