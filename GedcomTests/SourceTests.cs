using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

[TestClass]
public sealed class SourceTests
{
    private Exporter? Exporter;
    [TestInitialize]
    public void BeforeEach()
    {
        Exporter = new Exporter(TestUtilities.CreateGedcom());
    }

    [TestMethod]
    public void ExportSourceJsonTest()
    {
        var sourceJson = Exporter!.GetSourceRecordJson(TestTree.Sources.VitalRecords.Xref);

        Assert.IsTrue(sourceJson.Contains(TestTree.Sources.VitalRecords.Xref));
    }

    [TestMethod]
    public void ExportSourcesJsonTest()
    {
        var sourcesJson = Exporter!.GetSourceRecordsJson();

        Assert.IsTrue(sourcesJson.Contains(TestTree.Sources.VitalRecords.Xref));
    }

    [TestMethod]
    public void ExportNonExistingSourceJsonTest()
    {
        var sourceJson = Exporter!.GetSourceRecordJson("INVALID_XREF");

        Assert.IsTrue(sourceJson.Equals("{}"));
    }

    [TestMethod]
    public void QuerySourceJsonTest()
    {
        var sourcesJson = Exporter!.GetSourceRecordsJson("Vital records");

        Assert.IsTrue(sourcesJson.Contains(TestTree.Sources.VitalRecords.Xref));
    }
}