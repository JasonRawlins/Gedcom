using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Gedcom.CLI;

namespace Gedcom.CommandLineInterface;

public class ExcelWriter
{
    private WorkbookPart? WorkbookPart { get; set; }

    public void CreateIndividualsExcelSheet(List<IndividualListItem> individualListItems)
    {
        string sourceFilePath = @"C:\temp\GedcomNET\Resources\GedcomNET.xlsx";
        string targetFilePath = @"C:\temp\GedcomNET\Resources\GedcomNET-Changed.xlsx";

        File.Copy(sourceFilePath, targetFilePath, true);

        using (SpreadsheetDocument spreadsheet = SpreadsheetDocument.Open(targetFilePath, true))
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

                string cellValue = GetCellValue(cell);

                switch (cellValue)
                {
                    case ContentTag.Given:
                        cell.CellValue = new CellValue(individualListItem.Given);
                        break;
                    case ContentTag.Surname:
                        cell.CellValue = new CellValue(individualListItem.Surname);
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
                    default:
                        break;
                }

                // cell.DateType() has to be placed after the cell.CellValue() lines above. I don't
                // know why order matters, but it throws an error if I don't.
                cell.DataType = new EnumValue<CellValues>(CellValues.String);
            }
        }
    }

    private static class ContentTag
    {
        public const string Given = "_GIVEN_";
        public const string Surname = "_SURNAME_";
        public const string BirthDate = "_BIRTH_DATE_";
        public const string BirthPlace = "_BIRTH_PLACE_";
        public const string DeathDate = "_DEATH_DATE_";
        public const string DeathPlace = "_DEATH_PLACE_";
    }
}
