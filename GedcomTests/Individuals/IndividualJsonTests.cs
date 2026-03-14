using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Individuals;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualJsonTests
{
    private static IGedcomWriter JsonGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Json);

    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var individualJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetIndividuals(TestIndividuals.DylanDavis.Xref));
        var unexpectedIndividuals = TestIndividuals.All.Where(i => i.Xref != TestIndividuals.DylanDavis.Xref);

        Assert.IsTrue(individualJson.Contains(TestIndividuals.DylanDavis.Xref));

        foreach (var unexpectedIndividual in unexpectedIndividuals)
        {
            Assert.IsFalse(individualJson.Contains(unexpectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var individualsJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetIndividuals());

        foreach (var expectedIndividual in TestIndividuals.All)
        {
            Assert.IsTrue(individualsJson.Contains(expectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void NonExistingIndividualJsonTest()
    {
        var individualJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetIndividuals(TestConstants.InvalidXref));

        Assert.AreEqual("[]", individualJson);
    }

    [TestMethod]
    public void WriteIndividualsJsonTest()
    {
        var individualsJsonFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Individuals.json");
        var individualsJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetIndividuals());

        File.WriteAllText(individualsJsonFullName, individualsJson);
    }

    [TestMethod]
    public void WriteIndividualJsonTest()
    {
        var individualJsonFullName = Path.Combine(TestUtilities.OutputFilesDirectory, $"{TestIndividuals.DylanDavis.FileName}.json");
        var individualJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetIndividuals(TestIndividuals.DylanDavis.Xref));

        File.WriteAllText(individualJsonFullName, individualJson);
    }
}