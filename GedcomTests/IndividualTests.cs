using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualTests
{
    private Exporter? Exporter;
    [TestInitialize]
    public void BeforeEach()
    {
        Exporter = new Exporter(TestUtilities.CreateGedcom());
    }

    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var individualJson = Exporter!.GetIndividualJson(TestTree.Individuals.SaraDavis.Xref);

        Assert.IsTrue(individualJson.Contains(TestTree.Individuals.SaraDavis.Xref) &&
                !(individualJson.Contains(TestTree.Individuals.DylanDavis.Xref) ||
                individualJson.Contains(TestTree.Individuals.FionaDouglas.Xref) ||
                individualJson.Contains(TestTree.Individuals.GwenJones.Xref) ||
                individualJson.Contains(TestTree.Individuals.JamesSmith.Xref) ||
                individualJson.Contains(TestTree.Individuals.MarySmith.Xref) ||
                individualJson.Contains(TestTree.Individuals.OwenDavis.Xref)));
    }

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var individualsJson = Exporter!.GetIndividualsJson();

        Assert.IsTrue(
            individualsJson.Contains(TestTree.Individuals.DylanDavis.Xref)
            && individualsJson.Contains(TestTree.Individuals.FionaDouglas.Xref)
            && individualsJson.Contains(TestTree.Individuals.GwenJones.Xref)
            && individualsJson.Contains(TestTree.Individuals.JamesSmith.Xref)
            && individualsJson.Contains(TestTree.Individuals.MarySmith.Xref)
            && individualsJson.Contains(TestTree.Individuals.OwenDavis.Xref)
            && individualsJson.Contains(TestTree.Individuals.SaraDavis.Xref));
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
    public void NonExistingIndividualJsonTest()
    {
        var individualJson = Exporter!.GetIndividualJson("INVALID_XREF");

        Assert.IsTrue(individualJson.Equals("{}"));
    }

    [TestMethod]
    public void QueryIndividualsJsonTest()
    {
        var individualsJson = Exporter!.GetIndividualsJson("Davis");

        Assert.IsTrue(individualsJson.Contains(TestTree.Individuals.DylanDavis.Xref)
            && individualsJson.Contains(TestTree.Individuals.OwenDavis.Xref)
            && individualsJson.Contains(TestTree.Individuals.SaraDavis.Xref));
    }

    [TestMethod]
    public void ExportIndividualsHtmlTest()
    {
        var individualsHtml = Exporter!.GetIndividualsHtml();

        Assert.IsTrue(individualsHtml.Contains(TestTree.Individuals.DylanDavis.XrefId)
            && individualsHtml.Contains(TestTree.Individuals.FionaDouglas.XrefId)
            && individualsHtml.Contains(TestTree.Individuals.GwenJones.XrefId)
            && individualsHtml.Contains(TestTree.Individuals.JamesSmith.XrefId)
            && individualsHtml.Contains(TestTree.Individuals.MarySmith.XrefId)
            && individualsHtml.Contains(TestTree.Individuals.OwenDavis.XrefId)
            && individualsHtml.Contains(TestTree.Individuals.SaraDavis.XrefId));
    }

    [TestMethod]
    public void ExportIndividualXslxTest()
    {
        // This is an integration test.
        //var exporter = new Exporter(Gedcom, new Options()
        //{
        //    RecordType = Tag.INDI,
        //    Format = C.XSLX,
        //    InputFilePath = @"C:\temp\GedcomNET\Resources\GedcomNET.ged",
        //    OutputFilePath = @"C:\temp\GedcomNET\Resources\GedcomNET-output.xlsx"
        //});

        //var excelSheetBytes = exporter.IndividualsExcel();

        //File.WriteAllBytes(exporter.Options.OutputFilePath, excelSheetBytes);
    }
}