using Gedcom.GedcomWriters;
using GedcomTests.TestData;

namespace GedcomTests;

[TestClass]
public class SourceHtmlTests
{
    [TestMethod]
    public void ExportSourceHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var sourceHtml = htmlGedcomWriter.GetSource(TestTree.Sources.VitalRecords.Xref);

        Assert.IsTrue(sourceHtml.Contains(TestTree.Sources.VitalRecords.Xref));
    }

    [TestMethod]
    public void ExportSourcesHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var sourcesHtml = htmlGedcomWriter.GetSources();

        Assert.IsTrue(sourcesHtml.Contains(TestTree.Sources.VitalRecords.Xref));
    }

    [TestMethod]
    public void NonExistingSourceHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var sourceHtml = htmlGedcomWriter.GetSource(TestTree.InvalidXref);

        Assert.IsTrue(sourceHtml.Equals(""));
    }

    [TestMethod]
    public void QuerySourcesHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var sourcesHtml = htmlGedcomWriter.GetSources(TestTree.Sources.VitalRecords.Xref);

        Assert.IsTrue(sourcesHtml.Contains(TestTree.Sources.VitalRecords.Xref));
    }

    [TestMethod]
    public void WriteSourcesHtmlTest()
    {
        // This is an integration test. Figure that out later
        var gedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);

        File.WriteAllText(TestUtilities.HtmlFullName, gedcomWriter.GetSources());
    }
}