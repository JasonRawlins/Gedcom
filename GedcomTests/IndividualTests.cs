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
            json.Contains(TestTree.Individuals.DylanDavis.Xref)
            && json.Contains(TestTree.Individuals.FionaDouglas.Xref)
            && json.Contains(TestTree.Individuals.GwenJones.Xref)
            && json.Contains(TestTree.Individuals.JamesSmith.Xref)
            && json.Contains(TestTree.Individuals.MarySmith.Xref)
            && json.Contains(TestTree.Individuals.OwenDavis.Xref)
            && json.Contains(TestTree.Individuals.SaraDavis.Xref);
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
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.INDI, Xref = TestTree.Individuals.SaraDavis.Xref});
        GedcomAssert.RecordJsonIsValid(exporter, exporter.IndividualRecordJson, AssertFunction);

        static bool AssertFunction(string json) => 
            json.Contains(TestTree.Individuals.SaraDavis.Xref)
                && !(
                json.Contains(TestTree.Individuals.DylanDavis.Xref) || 
                json.Contains(TestTree.Individuals.FionaDouglas.Xref) || 
                json.Contains(TestTree.Individuals.GwenJones.Xref) || 
                json.Contains(TestTree.Individuals.JamesSmith.Xref) || 
                json.Contains(TestTree.Individuals.MarySmith.Xref) ||
                json.Contains(TestTree.Individuals.OwenDavis.Xref));
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
            json.Contains(TestTree.Individuals.DylanDavis.Xref)
            && json.Contains(TestTree.Individuals.OwenDavis.Xref)
            && json.Contains(TestTree.Individuals.SaraDavis.Xref);
    }

    [TestMethod]
    public void ExportIndividualsHtmlTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.INDI, Format = Tag.HTML });
        GedcomAssert.RecordHtmlIsValid(exporter, exporter.IndividualsHtml, AssertFunction);

        static bool AssertFunction (string html) =>
            html.Contains(TestTree.Individuals.DylanDavis.XrefId)
            && html.Contains(TestTree.Individuals.FionaDouglas.XrefId)
            && html.Contains(TestTree.Individuals.GwenJones.XrefId)
            && html.Contains(TestTree.Individuals.JamesSmith.XrefId)
            && html.Contains(TestTree.Individuals.MarySmith.XrefId)
            && html.Contains(TestTree.Individuals.OwenDavis.XrefId)
            && html.Contains(TestTree.Individuals.SaraDavis.XrefId);
    }

    [TestMethod]
    public void ExportIndividualXslxTest()
    {
        // This is an integration test.
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