using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;

namespace GedcomTests.Family;

[TestClass]
public class FamilyJsonTests
{
    [TestMethod]
    public void ExportFamilyJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.JSON);
        var familyJson = jsonGedcomWriter.GetFamily(TestFamilies.DylanDavisAndFionaDouglas.Xref);

        Assert.IsTrue(familyJson.Contains(TestFamilies.DylanDavisAndFionaDouglas.Xref)
            && !familyJson.Contains(TestFamilies.JamesSmithAndSaraDavis.Xref)
            && !familyJson.Contains(TestFamilies.OwenDavisAndGwenJones.Xref));
    }

    [TestMethod]
    public void ExportFamiliesJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.JSON);
        var familiesJson = jsonGedcomWriter.GetFamilies();

        Assert.IsTrue(familiesJson.Contains(TestFamilies.DylanDavisAndFionaDouglas.Xref)
            && familiesJson.Contains(TestFamilies.JamesSmithAndSaraDavis.Xref)
            && familiesJson.Contains(TestFamilies.OwenDavisAndGwenJones.Xref));
    }

    [TestMethod]
    public void ExportNonExistingFamilyJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.JSON);
        var familiesJson = jsonGedcomWriter.GetFamily(TestConstants.InvalidXref);

        Assert.IsTrue(familiesJson.Equals("{}"));
    }

    //[TestMethod]
    public void WriteFamiliesJsonTest()
    {
        // This is an integration test. Figure that out later
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.JSON);

        File.WriteAllText(TestUtilities.JsonFullName, jsonGedcomWriter.GetFamilies());
    }
}

