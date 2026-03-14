using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Individuals;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualTextTests
{
    private static IGedcomWriter TextGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Text);

    [TestMethod]
    public void ExportIndividualTextTest()
    {
        var individualText = Encoding.UTF8.GetString(TextGedcomWriter.GetIndividuals(TestIndividuals.DylanDavis.Xref));
        var unexpectedIndividuals = TestIndividuals.All.Where(i => i.Xref != TestIndividuals.DylanDavis.Xref);

        Assert.IsTrue(individualText.Contains(TestIndividuals.DylanDavis.Xref));

        foreach (var unexpectedIndividual in unexpectedIndividuals)
        {
            Assert.IsFalse(individualText.Contains(unexpectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void ExportIndividualsTextTest()
    {
        var individualsText = Encoding.UTF8.GetString(TextGedcomWriter.GetIndividuals());

        foreach (var expectedIndividual in TestIndividuals.All)
        {
            Assert.IsTrue(individualsText.Contains(expectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void NonExistingIndividualTextTest()
    {
        var individualText = Encoding.UTF8.GetString(TextGedcomWriter.GetIndividuals(TestConstants.InvalidXref));

        Assert.IsEmpty(individualText);
    }

    [TestMethod]
    public void WriteIndividualsTextTest()
    {
        var individualsTextFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Individuals.txt");
        var individualsText = Encoding.UTF8.GetString(TextGedcomWriter.GetIndividuals());

        File.WriteAllText(individualsTextFullName, individualsText);
    }

    [TestMethod]
    public void WriteIndividualTextTest()
    {
        var individualTextFullName = Path.Combine(TestUtilities.OutputFilesDirectory, $"{TestIndividuals.DylanDavis.FileName}.txt");
        var individualsText = Encoding.UTF8.GetString(TextGedcomWriter.GetIndividuals(TestIndividuals.DylanDavis.Xref));

        File.WriteAllText(individualTextFullName, individualsText);
    }
}