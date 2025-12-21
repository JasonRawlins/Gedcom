using Gedcom.GedcomWriters;

namespace GedcomTests.Source;

[TestClass]
public class SourceJsonTests
{
    [TestMethod]
    public void ExportSourceJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var sourceJson = jsonGedcomWriter.GetSource(TestTree.Sources.VitalRecords.Xref);

        Assert.IsTrue(sourceJson.Contains(TestTree.Sources.VitalRecords.Xref));
    }

    [TestMethod]
    public void ExportSourcesJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var sourcesJson = jsonGedcomWriter.GetSources();

        Assert.IsTrue(sourcesJson.Contains(TestTree.Sources.VitalRecords.Xref));
    }

    [TestMethod]
    public void ExportNonExistingSourceJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var sourcesJson = jsonGedcomWriter.GetSource(TestTree.InvalidXref);

        Assert.IsTrue(sourcesJson.Equals("{}"));
    }

    //[TestMethod]
    public void WriteSourcesJsonTest()
    {
        // This is an integration test. Figure that out later
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);

        File.WriteAllText(TestUtilities.JsonFullName, jsonGedcomWriter.GetSources());
    }
}

