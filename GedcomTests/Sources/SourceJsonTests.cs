using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Sources;

[TestClass]
public class SourceJsonTests
{
    [TestMethod]
    public void ExportSourceJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.JSON);
        var sourceJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetSources(TestSources.VitalRecords.Xref));

        //Assert.IsTrue(sourceJson.Contains(TestSources.VitalRecords.Xref));
    }

    [TestMethod]
    public void ExportSourcesJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.JSON);
        var sourcesJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetSources());

        //Assert.IsTrue(sourcesJson.Contains(TestSources.VitalRecords.Xref));
    }

    [TestMethod]
    public void ExportNonExistingSourceJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.JSON);
        var sourcesJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetSources(TestConstants.InvalidXref));

        //Assert.AreEqual("", sourcesJson);
    }

    //[TestMethod]
    public void WriteSourcesJsonTest()
    {
        // This is an integration test. Figure that out later
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.JSON);
        var sourcesJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetSources());

        File.WriteAllText(Path.Combine(TestUtilities.OutputFilesDirectory, "Sources.json"), sourcesJson);
    }
}

