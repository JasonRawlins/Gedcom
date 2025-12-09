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
            json.Contains(TestTree.Individuals.RobertDavis.Xref)
            && json.Contains(TestTree.Individuals.RosaGarcia.Xref)
            && json.Contains(TestTree.Individuals.MariaDavis.Xref)
            && json.Contains(TestTree.Individuals.DylanLewis.Xref)
            && json.Contains(TestTree.Individuals.MateoDavis.Xref)
            && json.Contains(TestTree.Individuals.GwenLewis.Xref)
            && json.Contains(TestTree.Families.RobertAndRosaDavis.Xref)
            && json.Contains(TestTree.Families.DylanAndMariaLewis.Xref)
            && json.Contains(TestTree.Repositories.GarciaFamilyBookOfRemembrance.Xref)
            && json.Contains(TestTree.Sources.GarciaFamilyBirths.Xref);
    }

    //[TestMethod]
    //public void GetImageBase64String()
    //{
    //    var imageBase64String = TestUtilities.GetImageBase64String();
    //}
}
