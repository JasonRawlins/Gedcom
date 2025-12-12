using Gedcom;
using Gedcom.CLI;

namespace GedcomTests;

public class TestUtilities
{
    public static readonly string GedcomNetTreeFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNET.ged");
    public static readonly string GedcomNetTreeDirectory = Path.GetDirectoryName(GedcomNetTreeFullName) ?? "";
    public static readonly string GedcomNetTreeOutputJsonFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNET.json");
    public static readonly string GedcomNetTreeOutputTextFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNET.txt");
    public static readonly string GedcomNetTreeOutputHtmlFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNET.html");
    public static readonly string GedcomNetTreeOutputXslxFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNET-template.xslx");

    public static Gedcom.Gedcom CreateGedcom()
    {
        var gedFileLines = File.ReadAllLines(GedcomNetTreeFullName); // Encoding.UTF8.GetString(Properties.Resources.GedcomNET).Split('\n');
        var gedcomLines = gedFileLines.Select(GedcomLine.Parse).ToList();
        return new Gedcom.Gedcom(gedcomLines);
    }

    public static string GetImageBase64String()
    {
        // This function finds an image on disc and generates its Base64 string. I only use it to generate
        // bytes that I can then embed in the css section of an html page. This has no internal use.
        byte[] imageBytes = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "ancestry-logo-260x50.png"));
        return Convert.ToBase64String(imageBytes);
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
        exporter.Options.GedPath = TestUtilities.GedcomNetTreeFullName;
        exporter.Options.OutputFilePath = TestUtilities.GedcomNetTreeOutputJsonFullName;

        if (assertValidExporter)
        {
            ExporterIsValid(exporter);
        }

        var json = JsonExportFunction();

        Assert.IsTrue(AssertFunction(json));
    }

    public static void RecordHtmlIsValid(Exporter exporter, Func<string> HtmlExportFunction, Func<string, bool> AssertFunction, bool assertValidExporter = true)
    {
        exporter.Options.GedPath = TestUtilities.GedcomNetTreeFullName;
        exporter.Options.OutputFilePath = TestUtilities.GedcomNetTreeOutputHtmlFullName;

        if (assertValidExporter)
        {
            ExporterIsValid(exporter);
        }

        var html = HtmlExportFunction();

        Assert.IsTrue(AssertFunction(html));
    }
}