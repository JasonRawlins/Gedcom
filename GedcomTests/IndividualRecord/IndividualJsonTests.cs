using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;

namespace GedcomTests.Individual;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualJsonTests
{
    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var individualJson = jsonGedcomWriter.GetIndividual(TestIndividuals.SaraDavis.Xref);

        Assert.IsTrue(individualJson.Contains(TestIndividuals.SaraDavis.Xref) &&
                !(individualJson.Contains(TestIndividuals.DylanDavis.Xref) ||
                individualJson.Contains(TestIndividuals.FionaDouglas.Xref) ||
                individualJson.Contains(TestIndividuals.GwenJones.Xref) ||
                individualJson.Contains(TestIndividuals.JamesSmith.Xref) ||
                individualJson.Contains(TestIndividuals.MarySmith.Xref) ||
                individualJson.Contains(TestIndividuals.OwenDavis.Xref)));
    }

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var jsonGedcomWriter =  GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var individualsJson = jsonGedcomWriter.GetIndividuals();

        Assert.IsTrue(individualsJson.Contains(TestIndividuals.SaraDavis.Xref) &&
                individualsJson.Contains(TestIndividuals.DylanDavis.Xref) &&
                individualsJson.Contains(TestIndividuals.FionaDouglas.Xref) &&
                individualsJson.Contains(TestIndividuals.GwenJones.Xref) &&
                individualsJson.Contains(TestIndividuals.JamesSmith.Xref) &&
                individualsJson.Contains(TestIndividuals.MarySmith.Xref) &&
                individualsJson.Contains(TestIndividuals.OwenDavis.Xref));
    }

    [TestMethod]
    public void NonExistingIndividualJsonTest()
    {
        var jsonGedcomWriter =  GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var individualJson = jsonGedcomWriter.GetIndividual(TestConstants.InvalidXref);

        Assert.IsTrue(individualJson.Equals("{}"));
    }

    [TestMethod]
    public void QueryIndividualsJsonTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var jsonGedcomWriter = new JsonGedcomWriter(gedcom);
        var individualsJson = jsonGedcomWriter.GetIndividuals("Davis");

        Assert.IsTrue(individualsJson.Contains(TestIndividuals.DylanDavis.Xref)
            && individualsJson.Contains(TestIndividuals.OwenDavis.Xref)
            && individualsJson.Contains(TestIndividuals.SaraDavis.Xref));
    }

    //[TestMethod]
    public void WriteIndividualsJsonTest()
    {
        // This is an integration test. Figure that out later
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);

        File.WriteAllText(TestUtilities.JsonFullName, jsonGedcomWriter.GetIndividuals());
    }
}