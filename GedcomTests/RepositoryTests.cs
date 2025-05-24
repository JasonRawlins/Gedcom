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
        var exporter = new Exporter(Gedcom, new Options() { RecordType = C.REPO });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.RepositoryRecordsJson, AssertFunction);

        bool AssertFunction(string json) =>
            json.Contains(TestTree.Repositories.GarciaFamilyBookOfRemembrance.Xref)
            && json.Contains(TestTree.Repositories.FamilySearchLibrary.Xref);
    }

    [TestMethod]
    public void ExportRepositoryJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() 
        { 
            RecordType = C.REPO,
            Xref = TestTree.Repositories.GarciaFamilyBookOfRemembrance.Xref
        });

        GedcomAssert.RecordJsonIsValid(exporter, exporter.RepositoryRecordJson, AssertFunction);

        bool AssertFunction(string json) =>
            json.Contains(TestTree.Repositories.GarciaFamilyBookOfRemembrance.Xref)
            && !json.Contains(TestTree.Repositories.FamilySearchLibrary.Xref);
    }

    [TestMethod]
    public void ExportNonExistingRepositoryJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = C.REPO, Xref = "INVALID_XREF"});
        GedcomAssert.RecordJsonIsValid(exporter, exporter.RepositoryRecordJson, AssertFunction, false);

        bool AssertFunction(string json) => string.IsNullOrWhiteSpace(json);
    }

    [TestMethod]
    public void QueryRepositoryJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = C.REPO, Query = "FamilySearch" });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.RepositoryRecordsJson, AssertFunction);

        bool AssertFunction(string json) => 
            json.Contains(TestTree.Repositories.FamilySearchLibrary.Xref)
            && !json.Contains(TestTree.Repositories.GarciaFamilyBookOfRemembrance.Xref);
    }
}