using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Families;

[TestClass]
public class FamilyHtmlTests
{
    [TestMethod]
    public void ExportFamilyJsonTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var htmlGedcomWriter = GedcomWriter.Create(gedcom, Constants.Html);
        var familyHtml = htmlGedcomWriter.GetFamilies(TestFamilies.JamesSmithAndSaraDavis.Xref);

        //Assert.IsTrue(familyHtml.Contains(TestFamilies.JamesSmithAndSaraDavis.Xref) &&
        //        !(familyHtml.Contains(TestFamilies.DylanDavisAndFionaDouglas.Xref) ||
        //        familyHtml.Contains(TestFamilies.OwenDavisAndGwenJones.Xref)));
    }

    [TestMethod]
    public void ExportFamiliesJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Html);
        var familiesHtml = htmlGedcomWriter.GetFamilies();

        //Assert.IsTrue(familiesHtml.Contains(TestFamilies.JamesSmithAndSaraDavis.Xref) &&
        //        familiesHtml.Contains(TestFamilies.OwenDavisAndGwenJones.Xref) &&
        //        familiesHtml.Contains(TestFamilies.DylanDavisAndFionaDouglas.Xref));
    }

    [TestMethod]
    public void NonExistingFamilyJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Html);
        var familyJson = Encoding.UTF8.GetString(htmlGedcomWriter.GetFamilies(TestConstants.InvalidXref));

        //Assert.IsTrue(familyJson.Equals(""));
    }

    [TestMethod]
    public void QueryFamiliesJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Html);
        var familesHtml = htmlGedcomWriter.GetFamilies(TestFamilies.JamesSmithAndSaraDavis.Xref);

        //Assert.IsTrue(familesHtml.Contains(TestFamilies.JamesSmithAndSaraDavis.Xref));
    }

    //[TestMethod]
    public static void WriteFamiliesHtmlTest()
    {
        // This is an integration test. Figure that out later
        var gedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Html);

        //File.WriteAllText(TestUtilities.HtmlFullName, gedcomWriter.GetFamilies());
    }
}