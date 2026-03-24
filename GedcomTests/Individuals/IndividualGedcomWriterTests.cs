using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Individuals;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualGedcomWriterTests
{
    private static IGedcomWriter ExcelGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Excel);
    private static IGedcomWriter HtmlGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Html);
    private static IGedcomWriter JsonGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Json);
    private static IGedcomWriter TextGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Text);

    #region Excel

    [TestMethod]
    public void ExportIndividualExcelTest()
    {
        var individualExcel = ExcelGedcomWriter.GetIndividuals(TestIndividuals.DylanDavis.Xref);
        var sharedStrings = TestUtilities.GetSharedStringsFromExcel(individualExcel);
        
        Assert.IsTrue(sharedStrings.Contains(TestIndividuals.DylanDavis.Xref));

        AssertUnexpectedIndividualsAreAbsent(sharedStrings);
    }

    [TestMethod]
    public void ExportIndividualsExcelTest()
    {
        var individualExcel = ExcelGedcomWriter.GetIndividuals();
        var sharedStrings = TestUtilities.GetSharedStringsFromExcel(individualExcel);

        AssertExpectedIndividualsArePresent(sharedStrings);
    }

    [TestMethod]
    public void NonexistentIndividualExcelTest()
    {
        var individualExcel = ExcelGedcomWriter.GetIndividuals(TestConstants.InvalidXref);
        var sharedStrings = TestUtilities.GetSharedStringsFromExcel(individualExcel);

        AssertNoIndividuals(sharedStrings);
    }

    [TestMethod]
    public void WriteIndividualsExcelTest()
    {
        string excelIndividualsFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Individuals.xlsx");

        File.WriteAllBytes(excelIndividualsFullName, ExcelGedcomWriter.GetIndividuals());
    }

    #endregion

    #region Html

    [TestMethod]
    public void ExportIndividualHtmlTest()
    {
        var individualHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetIndividuals(TestIndividuals.DylanDavis.Xref));

        Assert.IsTrue(individualHtml.Contains(TestIndividuals.DylanDavis.Xref));
        AssertUnexpectedIndividualsAreAbsent(individualHtml);
    }

    [TestMethod]
    public void ExportIndividualsHtmlTest()
    {
        var individualsHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetIndividuals());

        AssertExpectedIndividualsArePresent(individualsHtml);
    }

    [TestMethod]
    public void NonexistentIndividualHtmlTest()
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

    #endregion

    #region Json

    [TestMethod]
    public void ExportIndividualJsonTest()
    {
        var individualJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetIndividuals(TestIndividuals.DylanDavis.Xref));

        Assert.IsTrue(individualJson.Contains(TestIndividuals.DylanDavis.Xref));
        AssertUnexpectedIndividualsAreAbsent(individualJson);
    }

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var individualsJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetIndividuals());

        AssertExpectedIndividualsArePresent(individualsJson);
    }

    [TestMethod]
    public void NonexistentIndividualJsonTest()
    {
        var nonExistentIndividualJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetIndividuals(TestConstants.InvalidXref));

        Assert.AreEqual("[]", nonExistentIndividualJson);
    }

    [TestMethod]
    public void WriteIndividualsJsonTest()
    {
        var individualsJsonFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Individuals.json");
        var individualsJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetIndividuals());

        File.WriteAllText(individualsJsonFullName, individualsJson);
    }

    #endregion

    #region Text

    [TestMethod]
    public void ExportIndividualTextTest()
    {
        var individualText = Encoding.UTF8.GetString(TextGedcomWriter.GetIndividuals(TestIndividuals.DylanDavis.Xref));

        Assert.IsTrue(individualText.Contains(TestIndividuals.DylanDavis.Xref));
        AssertUnexpectedIndividualsAreAbsent(individualText);
    }

    [TestMethod]
    public void ExportIndividualsTextTest()
    {
        var individualsText = Encoding.UTF8.GetString(TextGedcomWriter.GetIndividuals());
        AssertExpectedIndividualsArePresent(individualsText);
    }

    [TestMethod]
    public void NonexistentIndividualTextTest()
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

    #endregion

    #region Assertions

    private static void AssertExpectedIndividualsArePresent(string individualsContent)
    {
        foreach (var expectedIndividual in TestIndividuals.All)
        {
            Assert.IsTrue(individualsContent.Contains(expectedIndividual.Xref));
        }
    }

    private static void AssertUnexpectedIndividualsAreAbsent(string individualsContent)
    {
        var unexpectedIndividuals = TestIndividuals.All.Where(i => i.Xref != TestIndividuals.DylanDavis.Xref);

        foreach (var unexpectedIndividual in unexpectedIndividuals)
        {
            Assert.IsFalse(individualsContent.Contains(unexpectedIndividual.Xref));
        }
    }

    private static void AssertNoIndividuals(string individualsContent)
    {
        foreach (var unexpectedIndividual in TestIndividuals.All)
        {
            Assert.IsFalse(individualsContent.Contains(unexpectedIndividual.Xref));
        }
    }

    #endregion 
}