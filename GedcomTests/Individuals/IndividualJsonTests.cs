using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Individuals;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualJsonTests
{
    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Json);
        var individualJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetIndividuals(TestIndividuals.SarahDavis.Xref));
        var unexpectedIndividuals = TestIndividuals.All.Where(i => i.Xref != TestIndividuals.SarahDavis.Xref);

        Assert.IsTrue(individualJson.Contains(TestIndividuals.SarahDavis.Xref));

        foreach (var unexpectedIndividual in unexpectedIndividuals)
        {
            Assert.IsFalse(individualJson.Contains(unexpectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Json);
        var individualsJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetIndividuals());

        foreach (var expectedIndividual in TestIndividuals.All)
        {
            Assert.IsTrue(individualsJson.Contains(expectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void NonExistingIndividualJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Json);
        var individualJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetIndividuals(TestConstants.InvalidXref));

        Assert.AreEqual("{}", individualJson);
    }

    [TestMethod]
    public void WriteIndividualJsonTest()
    {
        // This is an integration test. Figure that out later
        var textGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Json);
        var individualsJson = Encoding.UTF8.GetString(textGedcomWriter.GetIndividuals(TestIndividuals.SarahDavis.Xref));

        File.WriteAllText(Path.Combine(TestUtilities.OutputFilesDirectory, "Individual.json"), individualsJson);
    }

    [TestMethod]
    public void WriteIndividualsJsonTest()
    {
        // This is an integration test. Figure that out later
        var textGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Json);
        var individualsJson = Encoding.UTF8.GetString(textGedcomWriter.GetIndividuals());

        File.WriteAllText(Path.Combine(TestUtilities.OutputFilesDirectory, "Individuals.json"), individualsJson);
    }
}