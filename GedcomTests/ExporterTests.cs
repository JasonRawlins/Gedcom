using Gedcom;
using Gedcom.CLI;
using System.Text;

namespace GedcomTests;

[TestClass]
public sealed class ExporterTests
{
    private static Gedcom.Gedcom Gedcom { get; set; }

    static ExporterTests()
    {
        Gedcom = TestUtilities.CreateGedcom();
    }

    [TestMethod]
    public void InvalidInputFilePathTest()
    {
        var options = new Options
        {
            OutputFilePath = TestUtilities.GedcomNetTreeOutputJsonFullName,
            RecordType = C.INDI
        };

        var exporter = new Exporter(Gedcom, options);

        Assert.IsTrue(exporter.Errors.Find(e => e.Contains(Exporter.ErrorMessages.InputFilePathIsRequired)) != null, "The input file path was invalid.");
    }

    [TestMethod]
    public void InvalidOutputPathFileTest()
    {
        var options = new Options
        {
            InputFilePath = TestUtilities.GedcomNetTreeFullName,
            RecordType = C.INDI
        };

        var exporter = new Exporter(Gedcom, options);

        Assert.IsTrue(exporter.Errors.Find(e => e.Contains(Exporter.ErrorMessages.OutputFilePathIsRequired)) != null, "The output file path was invalid");
    }

    [TestMethod]
    public void RecordTypeIsValidTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndJsonOutput();

        var exporter = new Exporter(Gedcom, options);

        Assert.IsTrue(exporter.Errors.Find(e => e.Contains(Exporter.ErrorMessages.InvalidRecordType)) != null, "Record type is invalid.");
    }

    [TestMethod]
    public void DefaultFormatTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndJsonOutput();

        Assert.IsTrue(options.Format == C.JSON, $"The default format should be {C.JSON}.");
    }

    [TestMethod]
    public void InvalidFormatTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndJsonOutput();
        options.Format = "INVALID_FORMAT";

        var exporter = new Exporter(Gedcom, options);

        Assert.IsFalse(Exporter.RecordTypes.Contains(options.Format), "Invalid format");
    }
}
