using Gedcom;
using Gedcom.CLI;
using System.Text;

namespace GedcomTests;

public class TestUtilities
{
    public static string GedcomNetTreeFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNET.ged");
    public static string GedcomNetTreeDirectory = Path.GetDirectoryName(GedcomNetTreeFullName) ?? "";
    public static string GedcomNetTreeOutputJsonFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNET.json");
    public static string GedcomNetTreeOutputTextFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNET.txt");

    public static Gedcom.Gedcom CreateGedcom()
    {
        var gedFileLines = Encoding.UTF8.GetString(Properties.Resources.GedcomNET).Split('\n');
        var gedcomLines = gedFileLines.Select(GedcomLine.Parse).ToList();
        return new Gedcom.Gedcom(gedcomLines);
    }

    public static Options CreateOptionsWithInputAndJsonOutput()
    {
        return new Options
        {
            InputFilePath = GedcomNetTreeFullName,
            OutputFilePath = GedcomNetTreeOutputJsonFullName,
        };
    }

    public static Options CreateOptionsWithInputAndTextOutput()
    {
        return new Options
        {
            InputFilePath = GedcomNetTreeFullName,
            OutputFilePath = GedcomNetTreeOutputTextFullName
        };
    }
}

public class GedcomAssert
{
    public static void ExporterIsValid(Exporter exporter)
    {
        if (exporter.Errors.Count > 0)
        {
            Assert.Fail("There were errors in the Exporter: ", string.Join(Environment.NewLine, exporter.Errors));
        }
    }
    
    public static void RecordJsonIsValid(Exporter exporter, Func<string> JsonExportFunction, Func<string, bool> AssertFunction, bool assertValidExporter = true)
    {
        exporter.Options.InputFilePath = TestUtilities.GedcomNetTreeFullName;
        exporter.Options.OutputFilePath = TestUtilities.GedcomNetTreeOutputJsonFullName;

        if (assertValidExporter)
        {
            ExporterIsValid(exporter);
        }

        var exportedJson = JsonExportFunction();

        Assert.IsTrue(AssertFunction(exportedJson));
    }
}

