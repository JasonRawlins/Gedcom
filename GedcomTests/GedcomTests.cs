using Gedcom;
using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

[TestClass]
public sealed class GedcomTests
{
    private static Gedcom.Gedcom Gedcom { get; set; }

    static GedcomTests()
    {
        Gedcom = TestUtilities.CreateGedcom();
    }

    [TestMethod]
    public void ExportGedcomAsJsonTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = Tag.GEDC });
        GedcomAssert.RecordJsonIsValid(exporter, exporter.GedcomJson, AssertFunction);

        bool AssertFunction(string json) => 
            json.Contains(TestTree.Individuals.DylanDavis.Xref)
            && json.Contains(TestTree.Individuals.FionaDouglas.Xref)
            && json.Contains(TestTree.Individuals.GwenJones.Xref)
            && json.Contains(TestTree.Individuals.JamesSmith.Xref)
            && json.Contains(TestTree.Individuals.MarySmith.Xref)
            && json.Contains(TestTree.Individuals.OwenDavis.Xref)
            && json.Contains(TestTree.Individuals.SaraDavis.Xref)
            && json.Contains(TestTree.Families.DylanDavisAndFionaDouglas.Xref)
            && json.Contains(TestTree.Families.JamesSmithAndSaraDavis.Xref)
            && json.Contains(TestTree.Families.OwenDavisAndGwenJones.Xref)
            && json.Contains(TestTree.Repositories.VitalRecordsRepository.Xref)
            && json.Contains(TestTree.Sources.VitalRecords.Xref);
    }

    //[TestMethod]
    // Dummy test method used for generating a Base64 strings of images.
    //public void GetImageBase64String()
    //{
    //    var imageBase64String = TestUtilities.GetImageBase64String();
    //}
}
