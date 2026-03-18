using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Families;

[TestClass]
public class FamilyJsonTests
{
    private static IGedcomWriter JsonGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Json);

    [TestMethod]
    public void ExportFamilyJsonTest()
    {
        var familyJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetFamilies(TestFamilies.DylanDavisAndFionaDouglas.Xref));
        var unexpectedFamilies = TestFamilies.All.Where(i => i.Xref != TestFamilies.DylanDavisAndFionaDouglas.Xref);

        Assert.IsTrue(familyJson.Contains(TestFamilies.DylanDavisAndFionaDouglas.Xref));

        foreach (var unexpectedFamily in unexpectedFamilies)
        {
            Assert.IsFalse(familyJson.Contains(unexpectedFamily.Xref));
        }
    }

    [TestMethod]
    public void ExportFamiliesJsonTest()
    {
        var familiesJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetFamilies());

        foreach (var expectedFamily in TestFamilies.All)
        {
            Assert.IsTrue(familiesJson.Contains(expectedFamily.Xref));
        }
    }

    [TestMethod]
    public void ExportNonExistingFamilyJsonTest()
    {
        var nonExistentFamilyJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetFamilies(TestConstants.InvalidXref));

        Assert.AreEqual("[]", nonExistentFamilyJson);
    }

    [TestMethod]
    public void WriteFamiliesJsonTest()
    {
        var familiesJsonFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Families.json");
        var familiesJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetFamilies());

        File.WriteAllText(familiesJsonFullName, familiesJson);
    }
}

