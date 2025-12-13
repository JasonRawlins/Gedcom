using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

[TestClass]
public class FamilyTests
{
    private Exporter? Exporter;
    [TestInitialize]
    public void BeforeEach()
    {
        Exporter = new Exporter(TestUtilities.CreateGedcom());
    }

    [TestMethod]
    public void ExportFamilyJsonTest()
    {
        var familyJson = Exporter!.GetFamilyRecordJson(TestTree.Families.DylanDavisAndFionaDouglas.Xref);

        Assert.IsTrue(familyJson.Contains(TestTree.Families.DylanDavisAndFionaDouglas.Xref)
            && !familyJson.Contains(TestTree.Families.JamesSmithAndSaraDavis.Xref)
            && !familyJson.Contains(TestTree.Families.OwenDavisAndGwenJones.Xref));
    }

    [TestMethod]
    public void ExportFamiliesJsonTest()
    {
        var familiesJson = Exporter!.GetFamilyRecordsJson();

        Assert.IsTrue(familiesJson.Contains(TestTree.Families.DylanDavisAndFionaDouglas.Xref)
            && familiesJson.Contains(TestTree.Families.JamesSmithAndSaraDavis.Xref)
            && familiesJson.Contains(TestTree.Families.OwenDavisAndGwenJones.Xref));
    }

    [TestMethod]
    public void ExportNonExistingFamilyJsonTest()
    {
        var familyRecordJson = Exporter!.GetFamilyRecordJson("INVALID_XREF");

        Assert.IsTrue(familyRecordJson.Equals("{}"));
    }
}