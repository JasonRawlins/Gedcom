using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Individuals;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualHtmlTests
{
    private static IGedcomWriter HtmlGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Html);

    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var individualHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetIndividuals(TestIndividuals.DylanDavis.Xref));
        var unexpectedIndividuals = TestIndividuals.All.Where(i => i.Xref != TestIndividuals.DylanDavis.Xref);

        Assert.IsTrue(individualHtml.Contains(TestIndividuals.DylanDavis.Xref));

        foreach (var unexpectedIndividual in unexpectedIndividuals)
        {
            Assert.IsFalse(individualHtml.Contains(unexpectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var individualsHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetIndividuals());

        foreach (var expectedIndividual in TestIndividuals.All)
        {
            Assert.IsTrue(individualsHtml.Contains(expectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void NonExistingIndividualHtmlTest()
    {
        var individualJson = Encoding.UTF8.GetString(HtmlGedcomWriter.GetIndividuals(TestConstants.InvalidXref));

        Assert.IsFalse(individualJson.Contains("<ul class='individuals'>"));
    }

    [TestMethod]
    public void WriteIndividualsHtmlTest()
    {
        var individualsHtmlFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Individuals.html");
        var individualsHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetIndividuals());

        File.WriteAllText(individualsHtmlFullName, individualsHtml);
    }

    [TestMethod]
    public void WriteIndividualHtmlTest()
    {
        var individualHtmlFullName = Path.Combine(TestUtilities.OutputFilesDirectory, $"{TestIndividuals.DylanDavis.FileName}.html");
        var individualHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetIndividuals(TestIndividuals.DylanDavis.Xref));

        File.WriteAllText(individualHtmlFullName, individualHtml);
    }
}