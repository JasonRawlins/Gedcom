using Gedcom;
using Gedcom.CLI;

namespace GedcomTests;

[TestClass]
public sealed class OptionsTests
{
    private static Gedcom.Gedcom Gedcom { get; set; }

    static OptionsTests()
    {
        Gedcom = TestUtilities.CreateGedcom();
    }

    [TestMethod]
    public void InvalidInputFilePathTest()
    {
        var options = new Options
        {
            OutputFilePath = TestUtilities.GedcomNetTreeOutputJsonFullName,
            RecordType = Tag.INDI
        };
        
        Assert.IsNotNull(options.Errors.Find(e => e.Contains(CliErrorMessages.InputFilePathIsRequired)), "The input file path was invalid.");
    }

    [TestMethod]
    public void InvalidOutputPathFileTest()
    {
        var options = new Options
        {
            InputFilePath = TestUtilities.GedcomNetTreeFullName,
            RecordType = Tag.INDI
        };

        Assert.IsNotNull(options.Errors.Find(e => e.Contains(CliErrorMessages.OutputFilePathIsRequired)), "The output file path was invalid");
    }

    [TestMethod]
    public void InvalidRecordTypeTest()
    {
        var options = new Options() 
        { 
            RecordType = "INVALID_RECORD_TYPE"
        };

        Assert.IsNotNull(options.Errors.Find(e => e.Contains(CliErrorMessages.RecordTypeIsInvalid)), "Record type is invalid.");
    }

    [TestMethod]
    public void DefaultFormatTest()
    {
        var options = new Options();

        Assert.AreEqual(C.JSON, options.Format, $"The default format should be {C.JSON}.");
    }
}
