using Gedcom;
using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

[TestClass]
public sealed class SourceTests
{
    public static Gedcom.Gedcom Gedcom { get; set; }
   
    static SourceTests()
    {
        Gedcom = TestUtilities.CreateGedcom();
    }

    [TestMethod]
    public void ExportSourcesJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.SOUR });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.SourceRecordsJson, AssertFunction);

        static bool AssertFunction(string json) => json.Contains(TestTree.Sources.VitalRecords.Xref);
    }

    [TestMethod]
    public void ExportSourceJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options()
        {
            RecordType = Tag.SOUR,
            Xref = TestTree.Sources.VitalRecords.Xref
        });

        GedcomAssert.RecordJsonIsValid(exporter, exporter.SourceRecordJson, AssertFunction);

        static bool AssertFunction(string json) => json.Contains(TestTree.Sources.VitalRecords.Xref);
    }

    [TestMethod]
    public void ExportNonExistingSourceJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.SOUR, Xref = "INVALID_XREF"});
        GedcomAssert.RecordJsonIsValid(exporter, exporter.SourceRecordJson, AssertFunction, false);

        static bool AssertFunction(string json) => string.IsNullOrWhiteSpace(json);
    }

    [TestMethod]
    public void QuerySourceJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.SOUR, Query = "Vital records" });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.SourceRecordsJson, AssertFunction);

        static bool AssertFunction(string json) => json.Contains(TestTree.Sources.VitalRecords.Xref);
    }
}