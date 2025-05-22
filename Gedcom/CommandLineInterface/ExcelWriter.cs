using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Gedcom.CLI;
using Gedcom.RecordStructures;

namespace Gedcom.CommandLineInterface;

public class ExcelWriter
{
    private string SourceFileFullName { get; set; }
    private string TargetFileFullName { get; set; }
    private HeaderTree HeaderTree { get; set; }

    public ExcelWriter(string sourceFileFullName, string targetFileFullName, HeaderTree headerTree)
    {
        HeaderTree = headerTree;
        SourceFileFullName = sourceFileFullName;
        TargetFileFullName = targetFileFullName;
    }

    private WorkbookPart? WorkbookPart { get; set; }

    public void CreateIndividualsExcelSheet(List<IndividualListItem> individualListItems)
    {
        File.Copy(SourceFileFullName, TargetFileFullName, true);

        using (SpreadsheetDocument spreadsheet = SpreadsheetDocument.Open(TargetFileFullName, true))
        {
            WorkbookPart = spreadsheet.WorkbookPart;
            var templateSheet = WorkbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == "Template");
            var worksheetPart = (WorksheetPart)WorkbookPart.GetPartById(templateSheet.Id);
            var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            foreach (var individualListItem in individualListItems)
            {
                CreateSheet(templateSheet, individualListItem);
            }

            templateSheet.Remove();
            WorkbookPart.DeletePart(worksheetPart);

            spreadsheet.WorkbookPart.Workbook.Save();
            spreadsheet.Save();
        }
    }

    private void CreateSheet(Sheet templateSheet, IndividualListItem individualListItem)
    {
        var sourceSheetPart = (WorksheetPart)WorkbookPart.GetPartById(templateSheet.Id);
        var newSheetPart = WorkbookPart.AddNewPart<WorksheetPart>();
        newSheetPart.Worksheet = (Worksheet)sourceSheetPart.Worksheet.CloneNode(true);

        var sheets = WorkbookPart.Workbook.GetFirstChild<Sheets>();
        uint newSheetId = (uint)(sheets.ChildElements.Count + 1);

        var newSheet = new Sheet()
        {
            Name = individualListItem.FullName,
            SheetId = newSheetId,
            Id = WorkbookPart.GetIdOfPart(newSheetPart)
        };

        sheets.Append(newSheet);

        ReplaceAllDefinedNames(newSheet, individualListItem);
    }

    private string GetCellValue(Cell cell)
    {
        if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
        {
            return WorkbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>()
                .ElementAt(int.Parse(cell.CellValue.Text)).InnerText;
        }

        return cell.CellValue?.Text ?? "";
    }

    private void ReplaceAllDefinedNames(Sheet sheet, IndividualListItem individualListItem)
    {
        var worksheetPart = (WorksheetPart)WorkbookPart.GetPartById(sheet.Id);

        foreach (var row in worksheetPart.Worksheet.Descendants<Row>())
        {
            foreach (var cell in row.Elements<Cell>())
            {
                ReplaceDefinedNames(cell, individualListItem);              
            }
        }
    }

    private void ReplaceDefinedNames(Cell cell, IndividualListItem individualListItem)
    {
        string cellValue = GetCellValue(cell);

        switch (cellValue)
        {
            case ContentTag.AncestryProfileLink:
                cell.CellValue = new CellValue(HeaderTree.Name);
                break;
            case ContentTag.BirthDate:
                cell.CellValue = new CellValue(individualListItem.Birth.DayMonthYear);
                break;
            case ContentTag.BirthPlace:
                cell.CellValue = new CellValue(individualListItem.BirthPlace);
                break;
            case ContentTag.DeathDate:
                cell.CellValue = new CellValue(individualListItem.Death.DayMonthYear);
                break;
            case ContentTag.DeathPlace:
                cell.CellValue = new CellValue(individualListItem.DeathPlace);
                break;
            case ContentTag.FullName:
                cell.CellValue = new CellValue(individualListItem.FullName);
                break;
            case ContentTag.Given:
                cell.CellValue = new CellValue(individualListItem.Given);
                break;
            case ContentTag.Surname:
                cell.CellValue = new CellValue(individualListItem.Surname);
                break;
            case ContentTag.TreeName:
                cell.CellValue = new CellValue(HeaderTree.Name);
                break;
            default:
                cell.CellValue = new CellValue(cellValue);
                break;
        }

        // cell.DateType() has to be placed after the cell.CellValue() lines above. I don't
        // know why order matters, but it throws an error if I don't.
        cell.DataType = new EnumValue<CellValues>(CellValues.String);
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
