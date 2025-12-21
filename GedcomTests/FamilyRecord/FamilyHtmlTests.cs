using Gedcom.GedcomWriters;

namespace GedcomTests.Family;

[TestClass]
public class FamilyHtmlTests
{
    [TestMethod]
    public void ExportFamilyJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var familyHtml = htmlGedcomWriter.GetFamily(TestTree.Families.JamesSmithAndSaraDavis.Xref);

        Assert.IsTrue(familyHtml.Contains(TestTree.Families.JamesSmithAndSaraDavis.Xref) &&
                !(familyHtml.Contains(TestTree.Families.DylanDavisAndFionaDouglas.Xref) ||
                familyHtml.Contains(TestTree.Families.OwenDavisAndGwenJones.Xref)));
    }

    [TestMethod]
    public void ExportFamiliesJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var familiesHtml = htmlGedcomWriter.GetFamilies();

        Assert.IsTrue(familiesHtml.Contains(TestTree.Families.JamesSmithAndSaraDavis.Xref) &&
                familiesHtml.Contains(TestTree.Families.OwenDavisAndGwenJones.Xref) &&
                familiesHtml.Contains(TestTree.Families.DylanDavisAndFionaDouglas.Xref));
    }

    [TestMethod]
    public void NonExistingFamilyJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var familyJson = htmlGedcomWriter.GetFamily(TestTree.InvalidXref);

        Assert.IsTrue(familyJson.Equals(""));
    }

    [TestMethod]
    public void QueryFamiliesJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var familesHtml = htmlGedcomWriter.GetFamilies(TestTree.Families.JamesSmithAndSaraDavis.Xref);

        Assert.IsTrue(familesHtml.Contains(TestTree.Families.JamesSmithAndSaraDavis.Xref));
    }

    //[TestMethod]
    public void WriteFamiliesHtmlTest()
    {
        // This is an integration test. Figure that out later
        var gedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);

        File.WriteAllText(TestUtilities.HtmlFullName, gedcomWriter.GetFamilies());
    }
}