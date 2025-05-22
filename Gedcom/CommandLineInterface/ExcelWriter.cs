using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Gedcom.CLI;

namespace Gedcom.CommandLineInterface;

public class ExcelWriter
{
    string sourceFilePath = @"C:\temp\GedcomNET\Resources\GedcomNET.xlsx";
    string targetFilePath = @"C:\temp\GedcomNET\Resources\GedcomNET-Changed.xlsx";

    public ExcelWriter()
    {   
        File.Copy(sourceFilePath, targetFilePath, true);

        using (var spreadsheetDocument = SpreadsheetDocument.Open(targetFilePath, true))
        {
            var templateSheet = spreadsheetDocument.WorkbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == "Template");
            var worksheetPart = (WorksheetPart)spreadsheetDocument.WorkbookPart.GetPartById(templateSheet.Id);
            var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

           

            worksheetPart.Worksheet.Save();
            spreadsheetDocument.Save();
        }
    }

    public void WriteIndividuals(List<IndividualListItem> individualListItems)
    {
        //foreach (var individualListItem in individualListItems)
        //{
        //    CreateSheet(spreadsheet.WorkbookPart, templateSheet, individualListItem);
        //}
    }

    private void CreateSheet(WorkbookPart workbookPart, Sheet templateSheet, IndividualListItem individualListItem)
    {
        var sourceSheetPart = (WorksheetPart)workbookPart.GetPartById(templateSheet.Id);
        var newSheetPart = workbookPart.AddNewPart<WorksheetPart>();
        newSheetPart.Worksheet = (Worksheet)sourceSheetPart.Worksheet.CloneNode(true);

        var sheets = workbookPart.Workbook.GetFirstChild<Sheets>();
        uint newSheetId = (uint)(sheets.ChildElements.Count + 1);

        var newSheet = new Sheet()
        {
            Name = individualListItem.FullName,
            SheetId = newSheetId,
            Id = workbookPart.GetIdOfPart(newSheetPart)
        };

        sheets.Append(newSheet);

        ReplaceAllDefinedNames(workbookPart, newSheet, individualListItem);
    }

    private static void ReplaceAllDefinedNames(WorkbookPart workbookPart, Sheet sheet, IndividualListItem individualListItem)
    {
        var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);

        foreach (var row in worksheetPart.Worksheet.Descendants<Row>())
        {
            foreach (var cell in row.Elements<Cell>())
            {

                string cellValue = GetCellValue(workbookPart, cell);

                switch (cellValue)
                {
                    case "_GIVEN_":
                        cell.CellValue = new CellValue(individualListItem.Given);
                        break;
                    case "_SURNAME_":
                        cell.CellValue = new CellValue(individualListItem.Surname);
                        break;
                    case "_BIRTH_DATE_":
                        cell.CellValue = new CellValue(individualListItem.Birth.DayMonthYear);
                        break;
                    case "_BIRTH_PLACE_":
                        cell.CellValue = new CellValue(individualListItem.BirthPlace);
                        break;
                    case "_DEATH_DATE_":
                        cell.CellValue = new CellValue(individualListItem.Death.DayMonthYear);
                        break;
                    case "_DEATH_PLACE_":
                        cell.CellValue = new CellValue(individualListItem.DeathPlace);
                        break;
                    default:
                        cell.CellValue = new CellValue("UNKNOWN TAG");
                        break;

                }

                cell.DataType = new EnumValue<CellValues>(CellValues.String);
            }
        }
    }

    static string GetCellValue(WorkbookPart workbookPart, Cell cell)
    {
        if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
        {
            return workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>()
                .ElementAt(int.Parse(cell.CellValue.Text)).InnerText;
        }

        return cell.CellValue?.Text ?? "";
    }

    private static class GedcomExcelTags
    {
        public const string Given = "_GIVEN_";
        public const string Surname = "_SURNAME_";
        public const string BirthDate = "_BIRTH_DATE_";
        public const string BirthPlace = "_BIRTH_PLACE_";
        public const string DeathDate = "_DEATH_DATE_";
        public const string DeathPlace = "_DEATH_PLACE_";
    }
}
