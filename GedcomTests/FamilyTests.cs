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
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.FAM });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.FamilyRecordsJson, AssertFunction);

        static bool AssertFunction(string json) =>
            json.Contains(TestTree.Families.DylanDavisAndFionaDouglas.Xref)
            && json.Contains(TestTree.Families.JamesSmithAndSaraDavis.Xref)
            && json.Contains(TestTree.Families.OwenDavisAndGwenJones.Xref);
    }

    [TestMethod]
    public void ExportFamilyJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options()
        {
            RecordType = Tag.FAM,
            Xref = TestTree.Families.DylanDavisAndFionaDouglas.Xref
        });

        GedcomAssert.RecordJsonIsValid(exporter, exporter.FamilyRecordJson, AssertFunction);

        static bool AssertFunction(string json) =>
            json.Contains(TestTree.Families.DylanDavisAndFionaDouglas.Xref)
            && !json.Contains(TestTree.Families.JamesSmithAndSaraDavis.Xref)
            && !json.Contains(TestTree.Families.OwenDavisAndGwenJones.Xref);
    }

    [TestMethod]
    public void ExportNonExistingFamilyJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.FAM, Xref = "INVALID_XREF" });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.FamilyRecordJson, AssertFunction, false);

        static bool AssertFunction(string json) => string.IsNullOrWhiteSpace(json);
    }
}