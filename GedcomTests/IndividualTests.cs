using Gedcom;
using System.Text;

namespace GedcomTests;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" record, not
// its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public sealed class IndividualTests
{
    public static Gedcom.Gedcom Gedcom { get; set; }
   
    static IndividualTests()
    {
        var gedFileLines = Encoding.UTF8.GetString(Properties.Resources.DeveloperTree).Split(Environment.NewLine);
        var gedcomLines = gedFileLines.Select(GedcomLine.Parse).ToList();
        Gedcom = new Gedcom.Gedcom(gedcomLines);
    }

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndOutput();
        options.RecordType = C.INDI;

        var exporter = new Exporter(Gedcom, options);

        GedcomAssert.ExporterIsValid(exporter);

        var individualsJson = exporter.IndividualRecordsJson();

        Assert.IsTrue(
            individualsJson.Contains("@I1@")
            && individualsJson.Contains("@I2@")
            && individualsJson.Contains("@I3@"));
    }

    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndOutput();
        options.RecordType = C.INDI;
        options.Xref = "@I1@";

        var exporter = new Exporter(Gedcom, options);

        GedcomAssert.ExporterIsValid(exporter);

        var individualJson = exporter.IndividualRecordJson(options.Xref);

        Assert.IsTrue(
            individualJson.Contains("@I1@")
            && !(individualJson.Contains("@I2@") || individualJson.Contains("@I3@")));
    }

    [TestMethod]
    public void NonExistingIndividualJsonTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndOutput();
        options.RecordType = C.INDI;
        options.Xref = "@INVALID_XREF@";

        var exporter = new Exporter(Gedcom, options);

        GedcomAssert.ExporterIsValid(exporter);

        var individualJson = exporter.IndividualRecordJson(options.Xref);

        Assert.IsTrue(string.IsNullOrWhiteSpace(individualJson));
    }

    [TestMethod]
    public void QueryIndividualsJsonTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndOutput();
        options.RecordType = C.INDI;
        options.Query = "Person /Three/";

        var exporter = new Exporter(Gedcom, options);

        GedcomAssert.ExporterIsValid(exporter);

        var individualJson = exporter.IndividualRecordsJson();

        Assert.IsTrue(individualJson.Contains("@I3@"));
    }
}