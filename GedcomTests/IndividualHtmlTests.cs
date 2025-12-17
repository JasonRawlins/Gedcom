using Gedcom.GedcomWriters;
using GedcomTests.TestData;
using System.Text;

namespace GedcomTests;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualHtmlTests
{
    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var individualHtml = htmlGedcomWriter.GetIndividual(TestTree.Individuals.SaraDavis.Xref);

        Assert.IsTrue(individualHtml.Contains(TestTree.Individuals.SaraDavis.XrefId) &&
                !(individualHtml.Contains(TestTree.Individuals.DylanDavis.XrefId) ||
                individualHtml.Contains(TestTree.Individuals.FionaDouglas.XrefId) ||
                individualHtml.Contains(TestTree.Individuals.GwenJones.XrefId) ||
                individualHtml.Contains(TestTree.Individuals.JamesSmith.XrefId) ||
                individualHtml.Contains(TestTree.Individuals.MarySmith.XrefId) ||
                individualHtml.Contains(TestTree.Individuals.OwenDavis.XrefId)));
    }

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var individualsHtml = htmlGedcomWriter.GetIndividuals();

        Assert.IsTrue(individualsHtml.Contains(TestTree.Individuals.SaraDavis.XrefId) &&
                individualsHtml.Contains(TestTree.Individuals.DylanDavis.XrefId) &&
                individualsHtml.Contains(TestTree.Individuals.FionaDouglas.XrefId) &&
                individualsHtml.Contains(TestTree.Individuals.GwenJones.XrefId) &&
                individualsHtml.Contains(TestTree.Individuals.JamesSmith.XrefId) &&
                individualsHtml.Contains(TestTree.Individuals.MarySmith.XrefId) &&
                individualsHtml.Contains(TestTree.Individuals.OwenDavis.XrefId));
    }

    [TestMethod]
    public void NonExistingIndividualJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var individualJson = htmlGedcomWriter.GetIndividual(TestTree.InvalidXref);

        Assert.IsTrue(individualJson.Equals(""));
    }

    [TestMethod]
    public void QueryIndividualsJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var individualsHtml = htmlGedcomWriter.GetIndividuals("Davis");

        Assert.IsTrue(individualsHtml.Contains(TestTree.Individuals.DylanDavis.XrefId)
            && individualsHtml.Contains(TestTree.Individuals.OwenDavis.XrefId)
            && individualsHtml.Contains(TestTree.Individuals.SaraDavis.XrefId));
    }

    [TestMethod]
    public void WriteIndividualsHtmlTest()
    {
        // This is an integration test. Figure that out later
        var gedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var htmlBytes = gedcomWriter.GetAsByteArray();

        File.WriteAllText(TestUtilities.HtmlFullName, Encoding.UTF8.GetString(htmlBytes));
    }
}