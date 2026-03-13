using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.IO.Compression;
using System.Text;

namespace GedcomTests.Individuals;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualExcelTests
{
    private static IGedcomWriter ExcelGedcomWriter => GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Excel);

    [TestMethod]
    public void ExportIndividualExcelTest()
    {
        var individualExcel = ExcelGedcomWriter.GetIndividuals(TestIndividuals.DylanDavis.Xref);
        var sharedStrings = GetSharedStringsFromExcel(individualExcel);
        
        Assert.IsTrue(sharedStrings.Contains(TestIndividuals.DylanDavis.Xref));

        var unexpectedIndividuals = TestIndividuals.All.Where(i => i.Xref != TestIndividuals.DylanDavis.Xref);

        foreach (var unexpectedIndividual in unexpectedIndividuals)
        {
            Assert.IsFalse(sharedStrings.Contains(unexpectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void ExportIndividualsExcelTest()
    {
        var individualExcel = ExcelGedcomWriter.GetIndividuals();
        var sharedStrings = GetSharedStringsFromExcel(individualExcel);

        foreach (var expectedIndividual in TestIndividuals.All)
        {
            Assert.IsTrue(sharedStrings.Contains(expectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void NonExistingIndividualJsonTest()
    {
        var individualExcel = ExcelGedcomWriter.GetIndividuals(TestConstants.InvalidXref);
        var sharedStrings = GetSharedStringsFromExcel(individualExcel);

        foreach (var unexpectedIndividual in TestIndividuals.All)
        {
            Assert.IsFalse(sharedStrings.Contains(unexpectedIndividual.Xref));
        }
    }

    [TestMethod]
    public void WriteIndividualsExcelTest()
    {
        string excelIndividualsFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Individuals.xlsx");

        File.WriteAllBytes(excelIndividualsFullName, ExcelGedcomWriter.GetIndividuals());
    }

    [TestMethod]
    public void WriteIndividualExcelTest()
    {
        string individualsExcelFullName = Path.Combine(TestUtilities.OutputFilesDirectory, $"{TestIndividuals.DylanDavis.FileName}.xlsx");

        File.WriteAllBytes(individualsExcelFullName, ExcelGedcomWriter.GetIndividuals(TestIndividuals.DylanDavis.Xref));
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
}