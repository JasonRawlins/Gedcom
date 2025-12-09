using Gedcom;
using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public sealed class IndividualTests
{
    public static Gedcom.Gedcom Gedcom { get; set; }
   
    static IndividualTests() => Gedcom = TestUtilities.CreateGedcom();

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.INDI });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.IndividualRecordsJson, AssertFunction);

        static bool AssertFunction(string json) => 
            json.Contains(TestTree.Individuals.RobertDavis.Xref)
            && json.Contains(TestTree.Individuals.RosaGarcia.Xref)
            && json.Contains(TestTree.Individuals.MariaDavis.Xref)
            && json.Contains(TestTree.Individuals.DylanLewis.Xref)
            && json.Contains(TestTree.Individuals.GwenLewis.Xref)
            && json.Contains(TestTree.Individuals.MateoDavis.Xref);
    }

    [TestMethod]
    public void ExportIndividualsListTest()
    {
        // TODO: Fix this after you finish the json tests.
        //var exporter = new Exporter(Gedcom, new Options() { RecordType = C.INDI, Format = C.LIST });
        //var individualRecords = Gedcom.GetIndividualRecords();

        //GedcomAssert.RecordJsonIsValid(exporter, exporter.IndividualRecordsList, AssertFunction, nameof(ExportIndividualsListTest));

        //var individualsList = exporter.IndividualRecordsList();

        //bool AssertFunction(string json) => json.Count == json.Count);
    }

    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.INDI, Xref = TestTree.Individuals.MariaDavis.Xref});
        GedcomAssert.RecordJsonIsValid(exporter, exporter.IndividualRecordJson, AssertFunction);

        static bool AssertFunction(string json) => 
            json.Contains(TestTree.Individuals.MariaDavis.Xref)
                && !(json.Contains(TestTree.Individuals.DylanLewis.Xref) || json.Contains(TestTree.Individuals.GwenLewis.Xref));
    }

    [TestMethod]
    public void NonExistingIndividualJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.INDI, Xref = "INVALID_XREF" });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.IndividualRecordJson, AssertFunction, false);

        static bool AssertFunction(string json) => string.IsNullOrWhiteSpace(json);
    }

    [TestMethod]
    public void QueryIndividualsJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() {  RecordType = Tag.INDI, Query = "Davis" });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.IndividualRecordsJson, AssertFunction);

        static bool AssertFunction(string json) =>
            json.Contains(TestTree.Individuals.RobertDavis.Xref)
            && json.Contains(TestTree.Individuals.MariaDavis.Xref)
            && json.Contains(TestTree.Individuals.MateoDavis.Xref)
            && !json.Contains(TestTree.Individuals.GwenLewis.Xref);
    }

    [TestMethod]
    public void ExportIndividualsHtmlTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.INDI, Format = Tag.HTML });
        GedcomAssert.RecordHtmlIsValid(exporter, exporter.IndividualsHtml, AssertFunction);

        static bool AssertFunction (string html) =>
            html.Contains(TestTree.Individuals.RobertDavis.XrefId)
            && html.Contains(TestTree.Individuals.RosaGarcia.XrefId)
            && html.Contains(TestTree.Individuals.MariaDavis.XrefId)
            && html.Contains(TestTree.Individuals.DylanLewis.XrefId)
            && html.Contains(TestTree.Individuals.GwenLewis.XrefId)
            && html.Contains(TestTree.Individuals.MateoDavis.XrefId);
    }

    [TestMethod]
    public void ExportIndividualXslxTest()
    {
        var exporter = new Exporter(Gedcom, new Options()
        {
            RecordType = Tag.INDI,
            Format = C.XSLX,
            GedPath = @"C:\temp\GedcomNET\Resources\GedcomNET.ged",
            OutputFilePath = @"C:\temp\GedcomNET\Resources\GedcomNET-output.xlsx"
        });

        var excelSheetBytes = exporter.IndividualsExcel();

        File.WriteAllBytes(exporter.Options.OutputFilePath, excelSheetBytes);
    }
}