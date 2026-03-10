using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Individuals;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualTextTests
{
    [TestMethod]
    public void ExportIndividualTextTest()
    {
        var textGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Text);
        var individualText = Encoding.UTF8.GetString(textGedcomWriter.GetIndividuals(TestIndividuals.SarahDavis.Xref));
        var unexpectedIndividuals = TestIndividuals.All.Where(i => i.Xref != TestIndividuals.SarahDavis.Xref);

        Assert.IsTrue(individualText.Contains(TestIndividuals.SarahDavis.Xref));
        foreach (var unexpectedIndividual in unexpectedIndividuals)
        {
            Assert.IsFalse(individualText.Contains(unexpectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void ExportIndividualsTextTest()
    {
        var textGedcomWriter =  GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Text);
        var individualsText = Encoding.UTF8.GetString(textGedcomWriter.GetIndividuals());

        foreach (var expectedIndividual in TestIndividuals.All)
        {
            Assert.IsTrue(individualsText.Contains(expectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void NonExistingIndividualTextTest()
    {
        var textGedcomWriter =  GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Text);
        var individualsText = Encoding.UTF8.GetString(textGedcomWriter.GetIndividuals(TestConstants.InvalidXref));

        Assert.AreEqual($"Unknown xref: {TestConstants.InvalidXref}.", individualsText);
    }

    [TestMethod]
    public void WriteIndividualTextTest()
    {
        // This is an integration test. Figure that out later
        var textGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Text);
        var individualsText = Encoding.UTF8.GetString(textGedcomWriter.GetIndividuals(TestIndividuals.SarahDavis.Xref));

        File.WriteAllText(Path.Combine(TestUtilities.OutputFilesDirectory, "Individual.txt"), individualsText);
    }

    //[TestMethod]
    public void WriteIndividualsTextTest()
    {
        // This is an integration test. Figure that out later
        var textGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Text);
        var individualsText = Encoding.UTF8.GetString(textGedcomWriter.GetIndividuals());

        File.WriteAllText(Path.Combine(TestUtilities.OutputFilesDirectory, "Individuals.txt"), individualsText);
    }
}