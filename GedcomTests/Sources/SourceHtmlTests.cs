using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Sources;

[TestClass]
public class SourceHtmlTests
{
    [TestMethod]
    public void ExportSourceHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Html);
        var sourceHtml = Encoding.UTF8.GetString(htmlGedcomWriter.GetSources(TestSources.VitalRecords.Xref));

        //Assert.IsTrue(sourceHtml.Contains(TestSources.VitalRecords.Xref));
    }

    [TestMethod]
    public void ExportSourcesHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Html);
        var sourcesHtml = Encoding.UTF8.GetString(htmlGedcomWriter.GetSources());

        //Assert.IsTrue(sourcesHtml.Contains(TestSources.VitalRecords.Xref));
    }

    [TestMethod]
    public void NonExistingSourceHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Html);
        var sourceHtml = Encoding.UTF8.GetString(htmlGedcomWriter.GetSources(TestConstants.InvalidXref));

        //Assert.IsTrue(sourceHtml == "");
    }

    //[TestMethod]
    public void WriteSourcesHtmlTest()
    {
        // This is an integration test. Figure that out later
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Html);
        var sourcesHtml = Encoding.UTF8.GetString(htmlGedcomWriter.GetSources(TestSources.VitalRecords.Xref));

        File.WriteAllText(Path.Combine(TestUtilities.OutputFilesDirectory, "Sources.html"), sourcesHtml);
    }
}