using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Individual;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualHtmlTests
{
    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var individualHtml = htmlGedcomWriter.GetIndividual(TestIndividuals.SaraDavis.Xref);

        Assert.IsTrue(individualHtml.Contains(TestIndividuals.SaraDavis.XrefId) &&
                !(individualHtml.Contains(TestIndividuals.DylanDavis.XrefId) ||
                individualHtml.Contains(TestIndividuals.FionaDouglas.XrefId) ||
                individualHtml.Contains(TestIndividuals.GwenJones.XrefId) ||
                individualHtml.Contains(TestIndividuals.JamesSmith.XrefId) ||
                individualHtml.Contains(TestIndividuals.MarySmith.XrefId) ||
                individualHtml.Contains(TestIndividuals.OwenDavis.XrefId)));
    }

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var individualsHtml = htmlGedcomWriter.GetIndividuals();

        Assert.IsTrue(individualsHtml.Contains(TestIndividuals.SaraDavis.XrefId) &&
                individualsHtml.Contains(TestIndividuals.DylanDavis.XrefId) &&
                individualsHtml.Contains(TestIndividuals.FionaDouglas.XrefId) &&
                individualsHtml.Contains(TestIndividuals.GwenJones.XrefId) &&
                individualsHtml.Contains(TestIndividuals.JamesSmith.XrefId) &&
                individualsHtml.Contains(TestIndividuals.MarySmith.XrefId) &&
                individualsHtml.Contains(TestIndividuals.OwenDavis.XrefId));
    }

    [TestMethod]
    public void NonExistingIndividualJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var individualJson = htmlGedcomWriter.GetIndividual(TestConstants.InvalidXref);

        Assert.IsTrue(individualJson.Equals(""));
    }

    [TestMethod]
    public void QueryIndividualsJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var individualsHtml = htmlGedcomWriter.GetIndividuals("Davis");

        Assert.IsTrue(individualsHtml.Contains(TestIndividuals.DylanDavis.XrefId)
            && individualsHtml.Contains(TestIndividuals.OwenDavis.XrefId)
            && individualsHtml.Contains(TestIndividuals.SaraDavis.XrefId));
    }

    //[TestMethod]
    public void WriteIndividualsHtmlTest()
    {
        // This is an integration test. Figure that out later
        var gedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var htmlBytes = gedcomWriter.GetAsByteArray();

        File.WriteAllText(TestUtilities.HtmlFullName, Encoding.UTF8.GetString(htmlBytes));
    }
}