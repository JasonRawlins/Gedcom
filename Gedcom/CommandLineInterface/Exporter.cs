using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Gedcom.CLI;

public class Exporter
{
    public static string[] RecordTypes = [C.FAM, C.INDI, C.OBJE, C.NOTE, C.REPO, C.SOUR, C.SUBM, C.GEDC /* GEDC is not a real top-level record type. It's used when the whole gedcom is exported. */];
    public static string[] OutputFormats = [C.JSON, C.LIST, C.HTML, C.XSLX];

    public Options Options { get; set; }
    public Gedcom Gedcom { get; set; }

    public Exporter(Gedcom gedcom) : this(gedcom, new Options())
    {
    }

    public Exporter(Gedcom gedcom, Options options)
    {
        Gedcom = gedcom;
        Options = options ?? new Options();
    }

    public string GedcomJson() => JsonConvert.SerializeObject(Gedcom, JsonSettings.DefaultOptions);

    // Family (FAM)
    public string FamilyRecordJson() => GetRecordJson(Gedcom.GetFamilyRecord(Options.Xref, Options.Query));
    public string FamilyRecordsJson() => GetRecordsJson(Gedcom.GetFamilyRecords(Options.Query));

    // Individual (INDI)
    public string IndividualRecordJson() => GetRecordJson(Gedcom.GetIndividualRecord(Options.Xref, Options.Query));
    public string IndividualRecordsJson() => GetRecordsJson(Gedcom.GetIndividualRecords(Options.Query));
    public string IndividualsHtml()
    {
        var individualListItems = Gedcom.GetIndividualRecords().Select(ir => new IndividualListItem(ir)).ToList();
        return GetIndividualsHtml(individualListItems);
    }

    public string IndividualsXslx()
    {
        var individualListItems = Gedcom.GetIndividualRecords().Select(ir => new IndividualListItem(ir)).ToList();
        return GetIndividualsXslx(individualListItems);
    }

    // Repository (REPO)
    public string RepositoryRecordJson() => GetRecordJson(Gedcom.GetRepositoryRecord(Options.Xref));
    public string RepositoryRecordsJson() => GetRecordsJson(Gedcom.GetRepositoryRecords(Options.Query));

    // Source (SOUR)
    public string SourceRecordJson() => GetRecordJson(Gedcom.GetSourceRecord(Options.Xref));
    public string SourceRecordsJson() => GetRecordsJson(Gedcom.GetSourceRecords(Options.Query));

    private string GetRecordJson(RecordStructureBase recordStructure)
    {
        if (recordStructure.IsEmpty) return "";
        return JsonConvert.SerializeObject(recordStructure, JsonSettings.DefaultOptions);
    }

    private string GetRecordsJson(IEnumerable<RecordStructureBase> recordStructures)
    {
        if (recordStructures.Count() == 0) return "";
        return JsonConvert.SerializeObject(recordStructures, JsonSettings.DefaultOptions);
    }

    private string GetIndividualsHtml(List<IndividualListItem> individualListItems)
    {
        var htmlTemplate = Encoding.UTF8.GetString(Properties.Resources.IndividualsHtmlTemplate);
        var individualLis = HtmlWriter.CreateIndividualLis(individualListItems, Gedcom.Header.Source.Tree.AutomatedRecordId);
        var finalHtml = htmlTemplate.Replace("{{INDIVIDUAL_LIST_ITEMS}}", string.Join(Environment.NewLine, individualLis));

        return finalHtml;
    }

    private string GetIndividualsXslx(List<IndividualListItem> individualListItems)
    {
        string sourceFilePath = @"C:\temp\GedcomNET\Resources\GedcomNET.xlsx";
        string targetFilePath = @"C:\temp\GedcomNET\Resources\GedcomNET-Changed.xlsx";

        File.Copy(sourceFilePath, targetFilePath, true);

        using (SpreadsheetDocument spreadsheet = SpreadsheetDocument.Open(targetFilePath, true))
        {
            var templateSheet = spreadsheet.WorkbookPart.Workbook.Descendants<Sheet>().FirstOrDefault(s => s.Name == "Template");
            var worksheetPart = (WorksheetPart)spreadsheet.WorkbookPart.GetPartById(templateSheet.Id);
            var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

            foreach (var individualListItem in individualListItems)
            {
                CreateSheet(spreadsheet.WorkbookPart, templateSheet, individualListItem);
            }

            worksheetPart.Worksheet.Save();
            spreadsheet.Save();
        }

        return "";
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

    private static class GedcomDefinedNames
    {
        public const string Given = "GIVEN";
        public const string Surname = "SURNAME";
        public const string BirthDate = "BIRTH_DATE";
        public const string BirthPlace = "BIRTH_PLACE";
        public const string DeathDate = "DEATH_DATE";
        public const string DeathPlace = "DEATH_PLACE";
    }

    public string GetCliCommand()
    {
        return $"gedcom -i {Options.InputFilePath} -o {Options.OutputFilePath} -t {Options.RecordType} -f {Options.Format} -x {Options.Xref}";
    }

    public List<string> Errors
    {
        get
        {
            var argumentErrors = new List<string>();

            if (!File.Exists(Options.InputFilePath))
            {
                argumentErrors.Add($"{ErrorMessages.InputFilePathIsRequired} '{Options.InputFilePath}'");
            }

            string directoryPath = System.IO.Path.GetDirectoryName(Options.OutputFilePath) ?? "";
            if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
            {
                argumentErrors.Add(ErrorMessages.OutputFilePathIsRequired);
            }

            if (!RecordTypes.Contains(Options.RecordType))
            {
                argumentErrors.Add($"{Options.RecordType} {ErrorMessages.InvalidRecordType}");
            }

            if (!OutputFormats.Contains(Options.Format))
            {
                argumentErrors.Add($"{Options.Format} {ErrorMessages.InvalidFormat}");
            }

            if (!string.IsNullOrEmpty(Options.Xref))
            {
                var isValidXref = Regex.IsMatch(Options.Xref, @"@.*@");
                if (!isValidXref)
                {
                    // It looks like Ancestry is the one that prepends a letter to the xrefs based on 
                    // type, like "I" for INDI xrefs ("@I234@"). This is not part of the standard.
                    // See comment below on xref_ID for more details.
                    argumentErrors.Add($"{Options.Xref} {ErrorMessages.InvalidXref}");
                }
            }

            return argumentErrors;
        }
    }

    public class ErrorMessages
    {
        public const string InputFilePathIsRequired = "Could not find the input file:";
        public const string OutputFilePathIsRequired = "The output file path must refer to an existing directory.";
        public const string InvalidRecordType = "is not a valid record type. (FAM, INDI, OBJE, NOTE, REPO, SOUR, SUBM)";
        public const string InvalidFormat = "is not a valid export format. (JSON, LIST)";
        public const string InvalidXref = "is not a valid xref.";
    }
}