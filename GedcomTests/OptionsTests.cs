using Gedcom;
using Gedcom.CLI;

namespace GedcomTests;

[TestClass]
public sealed class OptionsTests
{
    private static GedcomDocument Gedcom { get; set; }

    static OptionsTests()
    {
        Gedcom = TestUtilities.CreateGedcom();
    }

    [TestMethod]
    public void InvalidInputFilePathTest()
    {
        var options = new CliOptions
        {
            OutputFilePath = TestUtilities.OutputFilesDirectory,
            RecordType = Tag.Individual
        };
        
        Assert.IsNotNull(options.Errors.Find(e => e.Contains(CliErrorMessages.InputFilePathIsRequired)), "The input file path was invalid.");
    }

    [TestMethod]
    public void InvalidOutputPathFileTest()
    {
        var options = new CliOptions
        {
            InputFilePath = TestUtilities.OutputFilesDirectory,
            RecordType = Tag.Individual
        };

        Assert.IsNotNull(options.Errors.Find(e => e.Contains(CliErrorMessages.OutputFilePathIsRequired)), "The output file path was invalid");
    }

    [TestMethod]
    public void InvalidRecordTypeTest()
    {
        var options = new CliOptions() 
        { 
            RecordType = "INVALID_RECORD_TYPE"
        };

        Assert.IsNotNull(options.Errors.Find(e => e.Contains(CliErrorMessages.RecordTypeIsInvalid)), "Record type is invalid.");
    }

    [TestMethod]
    public void DefaultFormatTest()
    {
        var options = new CliOptions();

        Assert.AreEqual(Constants.Json, options.Format, $"The default format should be {Constants.Json}.");
    }
}
