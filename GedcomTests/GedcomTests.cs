using Gedcom;
using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

[TestClass]
public class GedcomTests
{
    private Exporter? Exporter;
    [TestInitialize]
    public void BeforeEach()
    {
        Exporter = new Exporter(TestUtilities.CreateGedcom());
    }

    [TestMethod]
    public void ExportGedcomAsJsonTest()
    {
        var gedcJson = Exporter!.GetGedcomJson();

        Assert.IsTrue(gedcJson.Contains(TestTree.Individuals.DylanDavis.Xref)
            && gedcJson.Contains(TestTree.Individuals.FionaDouglas.Xref)
            && gedcJson.Contains(TestTree.Individuals.GwenJones.Xref)
            && gedcJson.Contains(TestTree.Individuals.JamesSmith.Xref)
            && gedcJson.Contains(TestTree.Individuals.MarySmith.Xref)
            && gedcJson.Contains(TestTree.Individuals.OwenDavis.Xref)
            && gedcJson.Contains(TestTree.Individuals.SaraDavis.Xref)
            && gedcJson.Contains(TestTree.Families.DylanDavisAndFionaDouglas.Xref)
            && gedcJson.Contains(TestTree.Families.JamesSmithAndSaraDavis.Xref)
            && gedcJson.Contains(TestTree.Families.OwenDavisAndGwenJones.Xref)
            && gedcJson.Contains(TestTree.Repositories.VitalRecordsRepository.Xref)
            && gedcJson.Contains(TestTree.Sources.VitalRecords.Xref));
    }

    //[TestMethod]
    // Dummy test method used for generating a Base64 strings of images.
    //public void GetImageBase64String()
    //{
    //    var imageBase64String = TestUtilities.GetImageBase64String();
    //}
}
