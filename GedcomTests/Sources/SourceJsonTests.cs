using Gedcom.Core;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;

namespace GedcomTests.Source;

[TestClass]
public class SourceJsonTests
{
    [TestMethod]
    public void ExportSourceJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), C.JSON);
        var sourceJson = jsonGedcomWriter.GetSource(TestSources.VitalRecords.Xref);

        Assert.IsTrue(sourceJson.Contains(TestSources.VitalRecords.Xref));
    }

    [TestMethod]
    public void ExportSourcesJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), C.JSON);
        var sourcesJson = jsonGedcomWriter.GetSources();

        Assert.IsTrue(sourcesJson.Contains(TestSources.VitalRecords.Xref));
    }

    [TestMethod]
    public void ExportNonExistingSourceJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), C.JSON);
        var sourcesJson = jsonGedcomWriter.GetSource(TestConstants.InvalidXref);

        Assert.IsTrue(sourcesJson.Equals("{}"));
    }

    //[TestMethod]
    public void WriteSourcesJsonTest()
    {
        // This is an integration test. Figure that out later
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), C.JSON);

        File.WriteAllText(TestUtilities.JsonFullName, jsonGedcomWriter.GetSources());
    }
}

