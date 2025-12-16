using Gedcom.CLI;
using GedcomTests.TestData;
using System.Text;

namespace GedcomTests;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualJsonTests
{
    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var individualJson = jsonGedcomWriter.GetIndividual(TestTree.Individuals.SaraDavis.Xref);

        Assert.IsTrue(individualJson.Contains(TestTree.Individuals.SaraDavis.Xref) &&
                !(individualJson.Contains(TestTree.Individuals.DylanDavis.Xref) ||
                individualJson.Contains(TestTree.Individuals.FionaDouglas.Xref) ||
                individualJson.Contains(TestTree.Individuals.GwenJones.Xref) ||
                individualJson.Contains(TestTree.Individuals.JamesSmith.Xref) ||
                individualJson.Contains(TestTree.Individuals.MarySmith.Xref) ||
                individualJson.Contains(TestTree.Individuals.OwenDavis.Xref)));
    }

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var jsonGedcomWriter =  GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var individualsJson = jsonGedcomWriter.GetIndividuals();

        Assert.IsTrue(individualsJson.Contains(TestTree.Individuals.SaraDavis.Xref) &&
                individualsJson.Contains(TestTree.Individuals.DylanDavis.Xref) &&
                individualsJson.Contains(TestTree.Individuals.FionaDouglas.Xref) &&
                individualsJson.Contains(TestTree.Individuals.GwenJones.Xref) &&
                individualsJson.Contains(TestTree.Individuals.JamesSmith.Xref) &&
                individualsJson.Contains(TestTree.Individuals.MarySmith.Xref) &&
                individualsJson.Contains(TestTree.Individuals.OwenDavis.Xref));
    }

    [TestMethod]
    public void NonExistingIndividualJsonTest()
    {
        var jsonGedcomWriter =  GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var individualJson = jsonGedcomWriter.GetIndividual(TestTree.InvalidXref);

        Assert.IsTrue(individualJson.Equals("{}"));
    }

    [TestMethod]
    public void QueryIndividualsJsonTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var jsonGedcomWriter = new JsonGedcomWriter(gedcom);
        var individualsJson = jsonGedcomWriter.GetIndividuals("Davis");

        Assert.IsTrue(individualsJson.Contains(TestTree.Individuals.DylanDavis.Xref)
            && individualsJson.Contains(TestTree.Individuals.OwenDavis.Xref)
            && individualsJson.Contains(TestTree.Individuals.SaraDavis.Xref));
    }

    [TestMethod]
    public void WriteIndividualsJsonTest()
    {
        // This is an integration test. Figure that out later
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);

        File.WriteAllText(TestUtilities.JsonFullName, jsonGedcomWriter.GetIndividuals());
    }
}