using Gedcom;
using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

[TestClass]
public sealed class FamilyTests
{
    public static Gedcom.Gedcom Gedcom { get; set; }

    static FamilyTests()
    {
        Gedcom = TestUtilities.CreateGedcom();
    }

    [TestMethod]
    public void ExportFamiliesJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = C.FAM });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.FamilyRecordsJson, AssertFunction);

        bool AssertFunction(string json) =>
            json.Contains(TestTree.Families.RobertAndRosaDavis.Xref)
            && json.Contains(TestTree.Families.DylanAndMariaLewis.Xref);
    }

    [TestMethod]
    public void ExportFamilyJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options()
        {
            RecordType = C.FAM,
            Xref = TestTree.Families.RobertAndRosaDavis.Xref
        });

        GedcomAssert.RecordJsonIsValid(exporter, exporter.FamilyRecordJson, AssertFunction);

        bool AssertFunction(string json) =>
            json.Contains(TestTree.Families.RobertAndRosaDavis.Xref)
            && !json.Contains(TestTree.Families.DylanAndMariaLewis.Xref);
    }

    [TestMethod]
    public void ExportNonExistingFamilyJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = C.FAM, Xref = "INVALID_XREF" });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.FamilyRecordJson, AssertFunction, false);

        bool AssertFunction(string json) => string.IsNullOrWhiteSpace(json);
    }

    //TODO: This test may not be relevant since a Family(FAM) record only has pointers. 
    //[TestMethod]
    //public void QueryFamilyJsonTest()
    //{
    //    var exporter = new Exporter(Gedcom, new Options() { RecordType = C.FAM, Query = TestTree.Families.DylanAndMariaLewis.Xref });
    //    GedcomAssert.RecordJsonIsValid(exporter, exporter.FamilyRecordsJson, AssertFunction, nameof(QueryFamilyJsonTest));

    //    bool AssertFunction(string json) =>
    //        json.Contains(TestTree.Families.DylanAndMariaLewis.Xref)
    //        && !json.Contains(TestTree.Families.RobertAndRosaDavis.Xref);
    //}
}