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
    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var individualHtml = Encoding.UTF8.GetString(htmlGedcomWriter.GetIndividuals(TestIndividuals.SarahDavis.Xref));

        Assert.IsTrue(individualHtml.Contains(TestIndividuals.SarahDavis.XrefId) &&
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
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var individualsHtml = Encoding.UTF8.GetString(htmlGedcomWriter.GetIndividuals());

        Assert.IsTrue(individualsHtml.Contains(TestIndividuals.SarahDavis.XrefId) &&
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
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var individualHtml = Encoding.UTF8.GetString(htmlGedcomWriter.GetIndividuals(TestConstants.InvalidXref));

        Assert.IsFalse(individualHtml.Contains("<ul>"));
    }
}