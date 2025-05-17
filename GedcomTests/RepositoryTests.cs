using Gedcom;
using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

[TestClass]
public sealed class RepositoryTests
{
    public static Gedcom.Gedcom Gedcom { get; set; }
   
    static RepositoryTests()
    {
        Gedcom = TestUtilities.CreateGedcom();
    }

    [TestMethod]
    public void ExportRepositoriesJsonTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndJsonOutput();
        options.RecordType = C.REPO;

        var exporter = new Exporter(Gedcom, options);
        GedcomAssert.ExporterIsValid(exporter);
        var repositoryRecordsJson = exporter.RepositoryRecordsJson();

        Assert.IsTrue(
            repositoryRecordsJson.Contains(TestTree.Repositories.GarciaFamilyBookOfRemembrance.Xref)
            && repositoryRecordsJson.Contains(TestTree.Repositories.FamilySearchLibrary.Xref));
    }

    [TestMethod]
    public void ExportRepositoryJsonTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndJsonOutput();
        options.RecordType = C.REPO;
        options.Xref = TestTree.Repositories.GarciaFamilyBookOfRemembrance.Xref;

        var exporter = new Exporter(Gedcom, options);
        GedcomAssert.ExporterIsValid(exporter);
        var repositoryRecordJson = exporter.RepositoryRecordJson();

        Assert.IsTrue(
            repositoryRecordJson.Contains(TestTree.Repositories.GarciaFamilyBookOfRemembrance.Xref)
            && !repositoryRecordJson.Contains(TestTree.Repositories.FamilySearchLibrary.Xref));
    }

    [TestMethod]
    public void ExportNonExistingRepositoryJsonTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndJsonOutput();
        options.RecordType = C.REPO;
        options.Xref = "INVALID_XREF";

        var exporter = new Exporter(Gedcom, options);
        var repositoryRecordJson = exporter.RepositoryRecordJson();

        Assert.IsTrue(string.IsNullOrWhiteSpace(repositoryRecordJson));
    }

    [TestMethod]
    public void QueryRepositoryJsonTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndJsonOutput();
        options.RecordType = C.REPO;
        options.Query = "FamilySearch";

        var exporter = new Exporter(Gedcom, options);
        GedcomAssert.ExporterIsValid(exporter);
        var repositoryRecordsJson = exporter.RepositoryRecordsJson();

        Assert.IsTrue(
            repositoryRecordsJson.Contains(TestTree.Repositories.FamilySearchLibrary.Xref)
            && !repositoryRecordsJson.Contains(TestTree.Repositories.GarciaFamilyBookOfRemembrance.Xref));
    }
}