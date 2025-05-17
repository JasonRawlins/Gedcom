using Gedcom;
using System.Text;

namespace GedcomTests;

[TestClass]
public sealed class ExporterTests
{
    private static Gedcom.Gedcom Gedcom { get; set; }

    static ExporterTests()
    {
        var gedFileLines = Encoding.UTF8.GetString(Properties.Resources.DeveloperTree).Split(Environment.NewLine);
        var gedcomLines = gedFileLines.Select(GedcomLine.Parse).ToList();
        Gedcom = new Gedcom.Gedcom(gedcomLines);
    }

    [TestMethod]
    public void InvalidInputFilePathTest()
    {
        var options = new Options
        {
            OutputFilePath = TestUtilities.TestTreeOutputFullName,
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
            InputFilePath = TestUtilities.TestTreeFullName,
            RecordType = C.INDI
        };

        var exporter = new Exporter(Gedcom, options);

        Assert.IsTrue(exporter.Errors.Find(e => e.Contains(Exporter.ErrorMessages.OutputFilePathIsRequired)) != null, "The output file path was invalid");
    }

    [TestMethod]
    public void RecordTypeIsValidTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndOutput();

        var exporter = new Exporter(Gedcom, options);

        Assert.IsTrue(exporter.Errors.Find(e => e.Contains(Exporter.ErrorMessages.InvalidRecordType)) != null, "Record type is invalid.");
    }

    [TestMethod]
    public void DefaultFormatTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndOutput();

        Assert.IsTrue(options.Format == C.JSON, $"The default format should be {C.JSON}.");
    }

    [TestMethod]
    public void InvalidFormatTest()
    {
        var options = TestUtilities.CreateOptionsWithInputAndOutput();
        options.Format = "INVALID_FORMAT";

        var exporter = new Exporter(Gedcom, options);

        Assert.IsFalse(Exporter.RecordTypes.Contains(options.Format), "Invalid format");
    }
}


//public const string MissingInputFilePath = "Could not find the input file:";
//public const string MissingOutputFilePath = "The output file path must refer to an existing directory.";
//public const string RecordTypeIsRequired = "Record type is required.";
//public const string InvalidRecordType = "is not a valid record type. (e.g. FAM, INDI, OBJE, NOTE, REPO, SOUR, SUBM)";
//public const string InvalidFormat = "is not a valid export format. (e.g. GEDC, JSON)";
//public const string InvalidXref = "is not a valid xref.";
