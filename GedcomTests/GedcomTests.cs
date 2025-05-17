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
        var options = new Options
        {
            RecordType = C.GEDC
        };

        var exporter = new Exporter(Gedcom, options);
        var gedcomJson = exporter.GedcomJson();

        Assert.IsTrue(
            gedcomJson.Contains(TestTree.Individuals.RobertDavis.Xref)
            && gedcomJson.Contains(TestTree.Individuals.RosaGarcia.Xref)
            && gedcomJson.Contains(TestTree.Individuals.MariaDavis.Xref)
            && gedcomJson.Contains(TestTree.Individuals.DylanLewis.Xref)
            && gedcomJson.Contains(TestTree.Individuals.MateoDavis.Xref)
            && gedcomJson.Contains(TestTree.Individuals.GwenLewis.Xref)
            && gedcomJson.Contains(TestTree.Families.RobertAndRosaDavis.Xref)
            && gedcomJson.Contains(TestTree.Families.DylanAndMariaLewis.Xref));
    }
}
