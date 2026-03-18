using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Families;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class FamilyTextTests
{
    private static IGedcomWriter TextGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Text);

    [TestMethod]
    public void ExportFamilyTextTest()
    {
        var familyText = Encoding.UTF8.GetString(TextGedcomWriter.GetFamilies(TestFamilies.DylanDavisAndFionaDouglas.Xref));
        var unexpectedFamilies = TestFamilies.All.Where(i => i.Xref != TestFamilies.DylanDavisAndFionaDouglas.Xref);

        Assert.IsTrue(familyText.Contains(TestFamilies.DylanDavisAndFionaDouglas.Xref));
        Assert.IsTrue(familyText.Contains(TestIndividuals.DylanDavis.Xref));
        Assert.IsTrue(familyText.Contains(TestIndividuals.FionaDouglas.Xref));
        Assert.IsTrue(familyText.Contains(TestIndividuals.SarahDavis.Xref));

        foreach (var unexpectedFamily in unexpectedFamilies)
        {
            Assert.IsFalse(familyText.Contains(unexpectedFamily.Xref));
        }
    }

    [TestMethod]
    public void ExportFamiliesTextTest()
    {
        var familiesText = Encoding.UTF8.GetString(TextGedcomWriter.GetFamilies());

        foreach (var expectedFamily in TestFamilies.All)
        {
            Assert.IsTrue(familiesText.Contains(expectedFamily.Xref));
        }
    }

    [TestMethod]
    public void NonExistingIndividualTextTest()
    {
        var familiesText = Encoding.UTF8.GetString(TextGedcomWriter.GetFamilies(TestConstants.InvalidXref));

        Assert.IsEmpty(familiesText);
    }

    [TestMethod]
    public void WriteIndividualsTextTest()
    {
        var familiesTextFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Families.txt");
        var familiesText = Encoding.UTF8.GetString(TextGedcomWriter.GetFamilies());

        File.WriteAllText(familiesTextFullName, familiesText);
    }

    [TestMethod]
    public void WriteIndividualTextTest()
    {
        var familyTextFullName = Path.Combine(TestUtilities.OutputFilesDirectory, $"{TestFamilies.DylanDavisAndFionaDouglas.FileName}.txt");
        var familyText = Encoding.UTF8.GetString(TextGedcomWriter.GetFamilies(TestFamilies.DylanDavisAndFionaDouglas.Xref));

        File.WriteAllText(familyTextFullName, familyText);
    }
}