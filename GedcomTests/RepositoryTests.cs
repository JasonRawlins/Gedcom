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
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.REPO });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.RepositoryRecordsJson, AssertFunction);

        bool AssertFunction(string json) => json.Contains(TestTree.Repositories.VitalRecordsRepository.Xref);
    }

    [TestMethod]
    public void ExportRepositoryJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() 
        { 
            RecordType = Tag.REPO,
            Xref = TestTree.Repositories.VitalRecordsRepository.Xref
        });

        GedcomAssert.RecordJsonIsValid(exporter, exporter.RepositoryRecordJson, AssertFunction);

        static bool AssertFunction(string json) => json.Contains(TestTree.Repositories.VitalRecordsRepository.Xref);
    }

    [TestMethod]
    public void ExportNonExistingRepositoryJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.REPO, Xref = "INVALID_XREF"});
        GedcomAssert.RecordJsonIsValid(exporter, exporter.RepositoryRecordJson, AssertFunction, false);

        static bool AssertFunction(string json) => string.IsNullOrWhiteSpace(json);
    }

    [TestMethod]
    public void QueryRepositoryJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.REPO, Query = "Vital records" });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.RepositoryRecordsJson, AssertFunction);

        static bool AssertFunction(string json) => json.Contains(TestTree.Repositories.VitalRecordsRepository.Xref);
    }
}