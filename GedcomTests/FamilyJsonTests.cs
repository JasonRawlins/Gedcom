using Gedcom.GedcomWriters;
using GedcomTests.TestData;

namespace GedcomTests;

[TestClass]
public class FamilyJsonTests
{
    [TestMethod]
    public void ExportFamilyJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var familyJson = jsonGedcomWriter.GetFamily(TestTree.Families.DylanDavisAndFionaDouglas.Xref);

        Assert.IsTrue(familyJson.Contains(TestTree.Families.DylanDavisAndFionaDouglas.Xref)
            && !familyJson.Contains(TestTree.Families.JamesSmithAndSaraDavis.Xref)
            && !familyJson.Contains(TestTree.Families.OwenDavisAndGwenJones.Xref));
    }

    [TestMethod]
    public void ExportFamiliesJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var familiesJson = jsonGedcomWriter.GetFamilies();

        Assert.IsTrue(familiesJson.Contains(TestTree.Families.DylanDavisAndFionaDouglas.Xref)
            && familiesJson.Contains(TestTree.Families.JamesSmithAndSaraDavis.Xref)
            && familiesJson.Contains(TestTree.Families.OwenDavisAndGwenJones.Xref));
    }

    [TestMethod]
    public void ExportNonExistingFamilyJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var familiesJson = jsonGedcomWriter.GetFamily(TestTree.InvalidXref);

        Assert.IsTrue(familiesJson.Equals("{}"));
    }

    [TestMethod]
    public void WriteFamiliesJsonTest()
    {
        // This is an integration test. Figure that out later
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);

        File.WriteAllText(TestUtilities.JsonFullName, jsonGedcomWriter.GetFamilies());
    }
}

