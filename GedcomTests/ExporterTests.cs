using Gedcom;
using Gedcom.CLI;

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
    public void InvalidRecordTypeTest()
    {
        var exporter = new Exporter(Gedcom, new Options() { RecordType = "INVALID_RECORD_TYPE" });

        Assert.IsTrue(exporter.Errors.Find(e => e.Contains(Exporter.ErrorMessages.InvalidRecordType)) != null, "Record type is invalid.");
    }

    [TestMethod]
    public void DefaultFormatTest()
    {
        var options = new Options();

        Assert.IsTrue(options.Format == C.JSON, $"The default format should be {C.JSON}.");
    }
}
