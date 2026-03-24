using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Repositories;

// The use of the word "Repository" in this class refers to a Gedcom "Repository" (REPO) record,
// not its meaning as used in Git.
[TestClass]
public class RepositoryGedcomWriterTests
{
    private static IGedcomWriter ExcelGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Excel);
    private static IGedcomWriter HtmlGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Html);
    private static IGedcomWriter JsonGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Json);
    private static IGedcomWriter TextGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Text);

    #region Excel

    [TestMethod]
    public void ExportRepositoryExcelTest()
    {
        var repositoryExcel = ExcelGedcomWriter.GetRepositories(TestRepositories.VitalRecordsRepository.Xref);
        var sharedStrings = TestUtilities.GetSharedStringsFromExcel(repositoryExcel);

        Assert.IsTrue(sharedStrings.Contains(TestRepositories.VitalRecordsRepository.Xref));

        AssertUnexpectedRepositoriesAreAbsent(sharedStrings);
    }

    [TestMethod]
    public void ExportRepositoriesExcelTest()
    {
        var repositoryExcel = ExcelGedcomWriter.GetRepositories();
        var sharedStrings = TestUtilities.GetSharedStringsFromExcel(repositoryExcel);

        AssertExpectedRepositoriesArePresent(sharedStrings);
    }

    [TestMethod]
    public void NonexistentRepositoryExcelTest()
    {
        var repositoryExcel = ExcelGedcomWriter.GetRepositories(TestConstants.InvalidXref);
        var sharedStrings = TestUtilities.GetSharedStringsFromExcel(repositoryExcel);

        AssertNoRepositories(sharedStrings);
    }

    [TestMethod]
    public void WriteRepositoriesExcelTest()
    {
        string excelRepositoriesFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Repositories.xlsx");

        File.WriteAllBytes(excelRepositoriesFullName, ExcelGedcomWriter.GetRepositories());
    }

    #endregion

    #region Html

    [TestMethod]
    public void ExportRepositoryHtmlTest()
    {
        var repositoryHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetRepositories(TestRepositories.VitalRecordsRepository.Xref));

        Assert.IsTrue(repositoryHtml.Contains(TestRepositories.VitalRecordsRepository.Xref));
        AssertUnexpectedRepositoriesAreAbsent(repositoryHtml);
    }

    [TestMethod]
    public void ExportRepositoriesHtmlTest()
    {
        var repositoriesHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetRepositories());

        AssertExpectedRepositoriesArePresent(repositoriesHtml);
    }

    [TestMethod]
    public void NonexistentRepositoryHtmlTest()
    {
        var repositoryJson = Encoding.UTF8.GetString(HtmlGedcomWriter.GetRepositories(TestConstants.InvalidXref));

        Assert.IsFalse(repositoryJson.Contains("<ul class='repositories'>"));
    }

    [TestMethod]
    public void WriteRepositoriesHtmlTest()
    {
        var repositoriesHtmlFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Repositories.html");
        var repositoriesHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetRepositories());

        File.WriteAllText(repositoriesHtmlFullName, repositoriesHtml);
    }

    #endregion

    #region Json

    [TestMethod]
    public void ExportRepositoryJsonTest()
    {
        var repositoryJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetRepositories(TestRepositories.VitalRecordsRepository.Xref));

        Assert.IsTrue(repositoryJson.Contains(TestRepositories.VitalRecordsRepository.Xref));
        AssertUnexpectedRepositoriesAreAbsent(repositoryJson);
    }

    [TestMethod]
    public void ExportRepositoriesJsonTest()
    {
        var repositoriesJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetRepositories());

        AssertExpectedRepositoriesArePresent(repositoriesJson);
    }

    [TestMethod]
    public void NonexistentRepositoryJsonTest()
    {
        var nonExistentRepositoryJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetRepositories(TestConstants.InvalidXref));

        Assert.AreEqual("[]", nonExistentRepositoryJson);
    }

    [TestMethod]
    public void WriteRepositoriesJsonTest()
    {
        var repositoriesJsonFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Repositories.json");
        var repositoriesJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetRepositories());

        File.WriteAllText(repositoriesJsonFullName, repositoriesJson);
    }

    #endregion

    #region Text

    [TestMethod]
    public void ExportRepositoryTextTest()
    {
        var repositoryText = Encoding.UTF8.GetString(TextGedcomWriter.GetRepositories(TestRepositories.VitalRecordsRepository.Xref));

        Assert.IsTrue(repositoryText.Contains(TestRepositories.VitalRecordsRepository.Xref));
        AssertUnexpectedRepositoriesAreAbsent(repositoryText);
    }

    [TestMethod]
    public void ExportRepositoriesTextTest()
    {
        var repositoriesText = Encoding.UTF8.GetString(TextGedcomWriter.GetRepositories());

        AssertExpectedRepositoriesArePresent(repositoriesText);
    }

    [TestMethod]
    public void NonexistentRepositoryTextTest()
    {
        var repositoryText = Encoding.UTF8.GetString(TextGedcomWriter.GetRepositories(TestConstants.InvalidXref));

        Assert.IsEmpty(repositoryText);
    }

    [TestMethod]
    public void WriteRepositoriesTextTest()
    {
        var repositoriesTextFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Repositories.txt");
        var repositoriesText = Encoding.UTF8.GetString(TextGedcomWriter.GetRepositories());

        File.WriteAllText(repositoriesTextFullName, repositoriesText);
    }

    #endregion

    #region Assertions

    private static void AssertExpectedRepositoriesArePresent(string repositoriesContent)
    {
        foreach (var expectedRepository in TestRepositories.All)
        {
            Assert.IsTrue(repositoriesContent.Contains(expectedRepository.Xref));
        }
    }

    private static void AssertUnexpectedRepositoriesAreAbsent(string repositoriesContent)
    {
        var unexpectedRepositories = TestRepositories.All.Where(r => r.Xref != TestRepositories.VitalRecordsRepository.Xref);

        foreach (var unexpectedRepository in unexpectedRepositories)
        {
            Assert.IsFalse(repositoriesContent.Contains(unexpectedRepository.Xref));
        }
    }

    private static void AssertNoRepositories(string repositoriesContent)
    {
        foreach (var unexpectedRepository in TestRepositories.All)
        {
            Assert.IsFalse(repositoriesContent.Contains(unexpectedRepository.Xref));
        }
    }

    #endregion 
}