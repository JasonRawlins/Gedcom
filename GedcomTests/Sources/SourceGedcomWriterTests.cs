using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Sources;

[TestClass]
public class SourceGedcomWriterTests
{
    private static IGedcomWriter ExcelGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Excel);
    private static IGedcomWriter HtmlGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Html);
    private static IGedcomWriter JsonGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Json);
    private static IGedcomWriter TextGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Text);

    #region Excel

    [TestMethod]
    public void ExportSourceExcelTest()
    {
        var sourceExcel = ExcelGedcomWriter.GetSources(TestSources.VitalRecords.Xref);
        var sharedStrings = TestUtilities.GetSharedStringsFromExcel(sourceExcel);

        Assert.IsTrue(sharedStrings.Contains(TestSources.VitalRecords.Xref));

        AssertUnexpectedSourcesAreAbsent(sharedStrings);
    }

    [TestMethod]
    public void ExportSourcesExcelTest()
    {
        var sourceExcel = ExcelGedcomWriter.GetSources();
        var sharedStrings = TestUtilities.GetSharedStringsFromExcel(sourceExcel);

        AssertExpectedSourcesArePresent(sharedStrings);
    }

    [TestMethod]
    public void NonexistentSourceExcelTest()
    {
        var sourceExcel = ExcelGedcomWriter.GetSources(TestConstants.InvalidXref);
        var sharedStrings = TestUtilities.GetSharedStringsFromExcel(sourceExcel);

        AssertNoSources(sharedStrings);
    }

    [TestMethod]
    public void WriteSourcesExcelTest()
    {
        string excelSourcesFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Sources.xlsx");

        File.WriteAllBytes(excelSourcesFullName, ExcelGedcomWriter.GetSources());
    }

    #endregion

    #region Html

    [TestMethod]
    public void ExportSourceHtmlTest()
    {
        var sourceHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetSources(TestSources.VitalRecords.Xref));

        Assert.IsTrue(sourceHtml.Contains(TestSources.VitalRecords.Xref));
        AssertUnexpectedSourcesAreAbsent(sourceHtml);
    }

    [TestMethod]
    public void ExportSourcesHtmlTest()
    {
        var sourcesHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetSources());

        AssertExpectedSourcesArePresent(sourcesHtml);
    }

    [TestMethod]
    public void NonexistentSourceHtmlTest()
    {
        var sourceJson = Encoding.UTF8.GetString(HtmlGedcomWriter.GetSources(TestConstants.InvalidXref));

        Assert.IsFalse(sourceJson.Contains("<ul class='sources'>"));
    }

    [TestMethod]
    public void WriteSourcesHtmlTest()
    {
        var sourcesHtmlFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Sources.html");
        var sourcesHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetSources());

        File.WriteAllText(sourcesHtmlFullName, sourcesHtml);
    }

    #endregion

    #region Json

    [TestMethod]
    public void ExportSourceJsonTest()
    {
        var sourceJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetSources(TestSources.VitalRecords.Xref));

        Assert.IsTrue(sourceJson.Contains(TestSources.VitalRecords.Xref));
        AssertUnexpectedSourcesAreAbsent(sourceJson);
    }

    [TestMethod]
    public void ExportSourcesJsonTest()
    {
        var sourcesJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetSources());

        AssertExpectedSourcesArePresent(sourcesJson);
    }

    [TestMethod]
    public void NonexistentSourceJsonTest()
    {
        var nonExistentSourceJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetSources(TestConstants.InvalidXref));

        Assert.AreEqual("[]", nonExistentSourceJson);
    }

    [TestMethod]
    public void WriteSourcesJsonTest()
    {
        var sourcesJsonFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Sources.json");
        var sourcesJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetSources());

        File.WriteAllText(sourcesJsonFullName, sourcesJson);
    }

    #endregion

    #region Text

    [TestMethod]
    public void ExportSourceTextTest()
    {
        var sourceText = Encoding.UTF8.GetString(TextGedcomWriter.GetSources(TestSources.VitalRecords.Xref));

        Assert.IsTrue(sourceText.Contains(TestSources.VitalRecords.Xref));
        AssertUnexpectedSourcesAreAbsent(sourceText);
    }

    [TestMethod]
    public void ExportSourcesTextTest()
    {
        var sourcesText = Encoding.UTF8.GetString(TextGedcomWriter.GetSources());

        AssertExpectedSourcesArePresent(sourcesText);
    }

    [TestMethod]
    public void NonexistentSourceTextTest()
    {
        var sourceText = Encoding.UTF8.GetString(TextGedcomWriter.GetSources(TestConstants.InvalidXref));

        Assert.IsEmpty(sourceText);
    }

    [TestMethod]
    public void WriteSourcesTextTest()
    {
        var sourcesTextFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Sources.txt");
        var sourcesText = Encoding.UTF8.GetString(TextGedcomWriter.GetSources());

        File.WriteAllText(sourcesTextFullName, sourcesText);
    }

    #endregion

    #region Assertions

    private static void AssertExpectedSourcesArePresent(string sourcesContent)
    {
        foreach (var expectedSource in TestSources.All)
        {
            Assert.IsTrue(sourcesContent.Contains(expectedSource.Xref));
        }
    }

    private static void AssertUnexpectedSourcesAreAbsent(string sourcesContent)
    {
        var unexpectedSources = TestSources.All.Where(r => r.Xref != TestSources.VitalRecords.Xref);

        foreach (var unexpectedSource in unexpectedSources)
        {
            Assert.IsFalse(sourcesContent.Contains(unexpectedSource.Xref));
        }
    }

    private static void AssertNoSources(string sourcesContent)
    {
        foreach (var unexpectedSource in TestSources.All)
        {
            Assert.IsFalse(sourcesContent.Contains(unexpectedSource.Xref));
        }
    }

    #endregion 
}