using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.IndividualRecord;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualJsonTests
{
    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.JSON);
        var individualJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetIndividual(TestIndividuals.SaraDavis.Xref));

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
        var jsonGedcomWriter =  GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.JSON);
        var individualsJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetIndividuals());

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
        var jsonGedcomWriter =  GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.JSON);
        var individualJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetIndividual(TestConstants.InvalidXref));

        Assert.IsTrue(individualJson.Equals("{}"));
    }

    [TestMethod]
    public void QueryIndividualsJsonTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var jsonGedcomWriter = new JsonGedcomWriter(gedcom);
        var individualsJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetIndividuals("Davis"));

        Assert.IsTrue(individualsJson.Contains(TestIndividuals.DylanDavis.Xref)
            && individualsJson.Contains(TestIndividuals.OwenDavis.Xref)
            && individualsJson.Contains(TestIndividuals.SaraDavis.Xref));
    }

    //[TestMethod]
    public void WriteIndividualsJsonTest()
    {
        // This is an integration test. Figure that out later
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.JSON);

        File.WriteAllText(TestUtilities.JsonFullName, Encoding.UTF8.GetString(jsonGedcomWriter.GetIndividuals()));
    }
}