using Gedcom.GedcomWriters;

namespace GedcomTests.Individual;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualExcelTests
{
    //[TestMethod]
    //public void WriteIndividualExcelTest()
    //{
    //    // This is an integration test. Figure that out later
    //    var gedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.Excel);
    //    var excelSheetBytes = gedcomWriter.GetAsByteArray();

    //    File.WriteAllBytes(TestUtilities.ExcelFullName, excelSheetBytes);
    //}
}