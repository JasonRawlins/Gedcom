using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Gedcom.RecordStructures;

namespace Gedcom.CLI;

public class ExcelWriter(HeaderTree headerTree, Stream templateStream) : IGedcomWriter
{
    private HeaderTree HeaderTree { get; set; } = headerTree;
    private Stream TemplateStream { get; set; } = templateStream;
    private WorkbookPart? WorkbookPart { get; set; }

    public byte[] GetIndividuals(List<IndividualListItem> individualListItems)
    {
        using (var spreadsheet = SpreadsheetDocument.Open(TemplateStream, true))
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

            using var memoryStream = new MemoryStream();
            spreadsheet.Clone(memoryStream);
            return memoryStream.ToArray();
        }
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

        cell.CellValue = cellValue switch
        {
            ContentTag.AncestryProfileLink => new CellValue(HeaderTree.Name),
            ContentTag.BirthDate => new CellValue(individualListItem.Birth.DayMonthYear),
            ContentTag.BirthPlace => new CellValue(individualListItem.BirthPlace),
            ContentTag.DeathDate => new CellValue(individualListItem.Death.DayMonthYear),
            ContentTag.DeathPlace => new CellValue(individualListItem.DeathPlace),
            ContentTag.FullName => new CellValue(individualListItem.FullName),
            ContentTag.Given => new CellValue(individualListItem.Given),
            ContentTag.Surname => new CellValue(individualListItem.Surname),
            ContentTag.TreeName => new CellValue(HeaderTree.Name),
            _ => new CellValue(cellValue),
        };

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
