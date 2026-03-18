using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Families;

[TestClass]
public class FamilyHtmlTests
{
    private static IGedcomWriter HtmlGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Html);
    
    [TestMethod]
    public void ExportFamilyJsonTest()
    {
        var familyHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetFamilies(TestFamilies.DylanDavisAndFionaDouglas.Xref));
        var unexpectedFamilies = TestIndividuals.All.Where(i => i.Xref != TestFamilies.DylanDavisAndFionaDouglas.Xref);

        Assert.IsTrue(familyHtml.Contains(TestFamilies.DylanDavisAndFionaDouglas.Xref));

        foreach (var unexpectedFamily in unexpectedFamilies)
        {
            Assert.IsFalse(familyHtml.Contains(unexpectedFamily.Xref));
        }
    }

    [TestMethod]
    public void ExportFamiliesJsonTest()
    {
        var familiesHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetFamilies());

        foreach (var expectedFamily in TestFamilies.All)
        {
            Assert.IsTrue(familiesHtml.Contains(expectedFamily.Xref));
        }
    }

    [TestMethod]
    public void NonExistingFamilyJsonTest()
    {
        var familyJson = Encoding.UTF8.GetString(HtmlGedcomWriter.GetFamilies(TestConstants.InvalidXref));

        Assert.IsFalse(familyJson.Contains("TODO"));
    }

    [TestMethod]
    public void WriteFamiliesHtmlTest()
    {
        var familiesHtmlFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Families.html");
        var familiesHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetFamilies());

        File.WriteAllText(familiesHtmlFullName, familiesHtml);
    }
}