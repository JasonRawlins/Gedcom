using Gedcom;
using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" record, not
// its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public sealed class IndividualTests
{
    public static Gedcom.Gedcom Gedcom { get; set; }
   
    static IndividualTests()
    {
        Gedcom = TestUtilities.CreateGedcom();
    }

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndJsonOutput();
        options.RecordType = C.INDI;

        var exporter = new Exporter(Gedcom, options);

        GedcomAssert.ExporterIsValid(exporter);

        var individualsJson = exporter.IndividualRecordsJson();

        Assert.IsTrue(
            individualsJson.Contains(TestTree.Individuals.RobertDavis.Xref)
            && individualsJson.Contains(TestTree.Individuals.RosaGarcia.Xref)
            && individualsJson.Contains(TestTree.Individuals.MariaDavis.Xref)
            && individualsJson.Contains(TestTree.Individuals.DylanLewis.Xref)
            && individualsJson.Contains(TestTree.Individuals.GwenLewis.Xref)
            && individualsJson.Contains(TestTree.Individuals.MateoDavis.Xref));
    }

    [TestMethod]
    public void ExportIndividualsListTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndTextOutput();
        options.Format = C.LIST;
        options.RecordType = C.INDI;

        var exporter = new Exporter(Gedcom, options);

        GedcomAssert.ExporterIsValid(exporter);

        var individualRecords = Gedcom.GetIndividualRecords();
        var individualsList = exporter.IndividualRecordsList();

        Assert.IsTrue(individualsList.Count == individualRecords.Count);
    }

    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndJsonOutput();
        options.RecordType = C.INDI;
        options.Xref = TestTree.Individuals.MariaDavis.Xref;

        var exporter = new Exporter(Gedcom, options);

        GedcomAssert.ExporterIsValid(exporter);

        var individualJson = exporter.IndividualRecordJson();

        Assert.IsTrue(
            individualJson.Contains(TestTree.Individuals.MariaDavis.Xref)
                && !(individualJson.Contains(TestTree.Individuals.DylanLewis.Xref) || individualJson.Contains(TestTree.Individuals.GwenLewis.Xref)));
    }

    [TestMethod]
    public void NonExistingIndividualJsonTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndJsonOutput();
        options.RecordType = C.INDI;
        options.Xref = "@INVALID_XREF@";

        var exporter = new Exporter(Gedcom, options);

        GedcomAssert.ExporterIsValid(exporter);

        var individualJson = exporter.IndividualRecordJson();

        Assert.IsTrue(string.IsNullOrWhiteSpace(individualJson));
    }

    [TestMethod]
    public void QueryIndividualsJsonTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndJsonOutput();
        options.RecordType = C.INDI;
        options.Query = "Davis";

        var exporter = new Exporter(Gedcom, options);

        GedcomAssert.ExporterIsValid(exporter);

        var individualRecordsJson = exporter.IndividualRecordsJson();

        Assert.IsTrue(
            individualRecordsJson.Contains(TestTree.Individuals.RobertDavis.Xref)
            && individualRecordsJson.Contains(TestTree.Individuals.MariaDavis.Xref)
            && individualRecordsJson.Contains(TestTree.Individuals.MateoDavis.Xref)
            && !individualRecordsJson.Contains(TestTree.Individuals.GwenLewis.Xref));
    }
}