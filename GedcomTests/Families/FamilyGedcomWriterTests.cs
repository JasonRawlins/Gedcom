using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.IO.Compression;
using System.Text;

namespace GedcomTests.Families;

[TestClass]
public class FamilyGedcomWriterTests
{
    private static IGedcomWriter ExcelGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Excel);
    private static IGedcomWriter HtmlGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Html);
    private static IGedcomWriter JsonGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Json);
    private static IGedcomWriter TextGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Text);

    #region Excel

    [TestMethod]
    public void ExportFamilyExcelTest()
    {
        //var familyExcel = ExcelGedcomWriter.GetFamilies(TestFamilies.DylanDavisAndFionaDouglas.Xref);
        //var sharedStrings = GetSharedStringsFromExcel(familyExcel);
        
        //Assert.IsTrue(sharedStrings.Contains(TestFamilies.DylanDavisAndFionaDouglas.Xref));

        //AssertUnexpectedFamiliesAreAbsent(sharedStrings);
    }

    [TestMethod]
    public void ExportFamiliesExcelTest()
    {
        //var familiesExcel = ExcelGedcomWriter.GetFamilies();
        //var sharedStrings = GetSharedStringsFromExcel(familiesExcel);

        //AssertExpectedFamiliesArePresent(sharedStrings);
    }

    [TestMethod]
    public void NonexistentFamilyExcelTest()
    {
        //var familyExcel = ExcelGedcomWriter.GetFamilies(TestConstants.InvalidXref);
        //var sharedStrings = GetSharedStringsFromExcel(familyExcel);

        //AssertNoFamilies(sharedStrings);
    }

    [TestMethod]
    public void WriteFamiliesExcelTest()
    {
        //var familiesExcelFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Families.xlsx");
        //var familiesExcel = ExcelGedcomWriter.GetFamilies();

        //File.WriteAllBytes(familiesExcelFullName, familiesExcel);
    }

    public static string GetSharedStringsFromExcel(byte[] xlsxBytes)
    {
        var xmlFiles = new Dictionary<string, string>();

        using var stream = new MemoryStream(xlsxBytes);
        using var zip = new ZipArchive(stream, ZipArchiveMode.Read);

        foreach (var entry in zip.Entries)
        {
            if (entry.Name.EndsWith(".xml") || entry.Name.EndsWith(".rels"))
            {
                using var entryStream = entry.Open();
                using var reader = new StreamReader(entryStream, Encoding.UTF8);
                xmlFiles[entry.FullName] = reader.ReadToEnd();
            }
        }

        return xmlFiles.Single(x => x.Key == "xl/sharedStrings.xml").Value;
    }

    #endregion

    #region Html

    [TestMethod]
    public void ExportFamilyHtmlTest()
    {
        var familyHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetFamilies(TestFamilies.DylanDavisAndFionaDouglas.Xref));

        Assert.IsTrue(familyHtml.Contains(TestFamilies.DylanDavisAndFionaDouglas.Xref));
        Assert.IsTrue(familyHtml.Contains(TestIndividuals.DylanDavis.Xref));
        Assert.IsTrue(familyHtml.Contains(TestIndividuals.FionaDouglas.Xref));
        Assert.IsTrue(familyHtml.Contains(TestIndividuals.SarahDavis.Xref));

        AssertUnexpectedFamiliesAreAbsent(familyHtml);
    }

    [TestMethod]
    public void ExportFamiliesHtmlTest()
    {
        var familiesHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetFamilies());

        AssertExpectedFamiliesArePresent(familiesHtml);
    }

    [TestMethod]
    public void NonexistentFamilyHtmlTest()
    {
        var familyJson = Encoding.UTF8.GetString(HtmlGedcomWriter.GetFamilies(TestConstants.InvalidXref));

        Assert.IsFalse(familyJson.Contains("<ul class='families'>"));
    }

    [TestMethod]
    public void WriteFamiliesHtmlTest()
    {
        var familiesHtmlFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Families.html");
        var familiesHtml = Encoding.UTF8.GetString(HtmlGedcomWriter.GetFamilies());

        File.WriteAllText(familiesHtmlFullName, familiesHtml);
    }

    #endregion

    #region Json

    [TestMethod]
    public void ExportFamilyJsonTest()
    {
        var familyJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetFamilies(TestFamilies.DylanDavisAndFionaDouglas.Xref));

        Assert.IsTrue(familyJson.Contains(TestFamilies.DylanDavisAndFionaDouglas.Xref));
        AssertUnexpectedFamiliesAreAbsent(familyJson);
    }

    [TestMethod]
    public void ExportFamiliesJsonTest()
    {
        var familiesJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetFamilies());

        AssertExpectedFamiliesArePresent(familiesJson);
    }

    [TestMethod]
    public void NonexistentFamilyJsonTest()
    {
        var nonExistentFamilyJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetFamilies(TestConstants.InvalidXref));

        Assert.AreEqual("[]", nonExistentFamilyJson);
    }

    [TestMethod]
    public void WriteFamiliesJsonTest()
    {
        var familiesJsonFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Families.json");
        var familiesJson = Encoding.UTF8.GetString(JsonGedcomWriter.GetFamilies());

        File.WriteAllText(familiesJsonFullName, familiesJson);
    }

    #endregion

    #region Text

    [TestMethod]
    public void ExportFamilyTextTest()
    {
        var familyText = Encoding.UTF8.GetString(TextGedcomWriter.GetFamilies(TestFamilies.DylanDavisAndFionaDouglas.Xref));

        Assert.IsTrue(familyText.Contains(TestFamilies.DylanDavisAndFionaDouglas.Xref));
        AssertUnexpectedFamiliesAreAbsent(familyText);
    }

    [TestMethod]
    public void ExportFamiliesTextTest()
    {
        var familiesText = Encoding.UTF8.GetString(TextGedcomWriter.GetFamilies());
        AssertExpectedFamiliesArePresent(familiesText);
    }

    [TestMethod]
    public void NonexistentFamilyTextTest()
    {
        var familyText = Encoding.UTF8.GetString(TextGedcomWriter.GetFamilies(TestConstants.InvalidXref));

        Assert.IsEmpty(familyText);
    }

    [TestMethod]
    public void WriteFamiliesTextTest()
    {
        var familiesTextFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Families.txt");
        var familiesText = Encoding.UTF8.GetString(TextGedcomWriter.GetFamilies());

        File.WriteAllText(familiesTextFullName, familiesText);
    }

    #endregion

    #region Assertions

    private static void AssertExpectedFamiliesArePresent(string familiesContent)
    {
        foreach (var expectedFamily in TestFamilies.All)
        {
            Assert.IsTrue(familiesContent.Contains(expectedFamily.Xref));
        }
    }

    private static void AssertUnexpectedFamiliesAreAbsent(string familiesContent)
    {
        var unexpectedFamilies = TestFamilies.All.Where(f => f.Xref != TestFamilies.DylanDavisAndFionaDouglas.Xref);

        foreach (var unexpectedFamily in unexpectedFamilies)
        {
            Assert.IsFalse(familiesContent.Contains(unexpectedFamily.Xref));
        }
    }

    private static void AssertNoFamilies(string familiesContent)
    {
        foreach (var unexpectedFamily in TestFamilies.All)
        {
            Assert.IsFalse(familiesContent.Contains(unexpectedFamily.Xref));
        }
    }

    #endregion 
}