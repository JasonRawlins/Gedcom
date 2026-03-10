using Gedcom.GedcomWriters;

namespace GedcomTests.Individuals;

// The use of the word "Individual" in this class refers to a Gedcom "Individual" (INDI) record,
// not its normal meaning of "singular," "each," "one," etc. 
[TestClass]
public class IndividualExcelTests
{

    //[TestMethod]
    public void WriteIndividualsJsonTest()
    {
        var excelGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.Constants.Excel);

        string excelIndividualsFullName = Path.Combine(TestUtilities.OutputFilesDirectory, "Individuals.xlsx");

        File.WriteAllBytes(excelIndividualsFullName, excelGedcomWriter.GetIndividuals());
    }
}