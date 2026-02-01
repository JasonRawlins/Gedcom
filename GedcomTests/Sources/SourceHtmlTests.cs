using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;

namespace GedcomTests.Source;

[TestClass]
public class SourceHtmlTests
{
    [TestMethod]
    public void ExportSourceHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var sourceHtml = htmlGedcomWriter.GetSource(TestSources.VitalRecords.Xref);

        Assert.IsTrue(sourceHtml.Contains(TestSources.VitalRecords.Xref));
    }

    [TestMethod]
    public void ExportSourcesHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var sourcesHtml = htmlGedcomWriter.GetSources();

        Assert.IsTrue(sourcesHtml.Contains(TestSources.VitalRecords.Xref));
    }

    [TestMethod]
    public void NonExistingSourceHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var sourceHtml = htmlGedcomWriter.GetSource(TestConstants.InvalidXref);

        Assert.IsTrue(sourceHtml.Equals(""));
    }

    [TestMethod]
    public void QuerySourcesHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var sourcesHtml = htmlGedcomWriter.GetSources(TestSources.VitalRecords.Xref);

        Assert.IsTrue(sourcesHtml.Contains(TestSources.VitalRecords.Xref));
    }

    //[TestMethod]
    public void WriteSourcesHtmlTest()
    {
        // This is an integration test. Figure that out later
        var gedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);

        File.WriteAllText(TestUtilities.HtmlFullName, gedcomWriter.GetSources());
    }
}