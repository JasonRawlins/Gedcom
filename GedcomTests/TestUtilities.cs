using Gedcom;
using System.Text;

namespace GedcomTests;

public class TestUtilities
{
    public static readonly string OutputFilesDirectory = @"C:\temp\GedcomNet\OutputFiles"; //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNet.json");
    public static readonly string HtmlFullName = Path.Combine(OutputFilesDirectory, "GedcomNet.html");
    public static readonly string JsonFullName = Path.Combine(OutputFilesDirectory, "GedcomNet.json");
    public static readonly string TextFullName = Path.Combine(OutputFilesDirectory, "GedcomNet.txt");

    public static GedcomDocument CreateGedcom()
    {
        var gedFileLines = Encoding.UTF8.GetString(Properties.Resources.GedcomNetTestTree).Split('\n');
        var gedcomLines = gedFileLines.Where(l => !string.IsNullOrEmpty(l)).Select(GedcomLine.Parse).ToList();
        return new GedcomDocument(gedcomLines);
    }

    public static string GetImageBase64String()
    {
        // This function finds an image on disc and generates its Base64 string. I only use it to generate
        // bytes that I can then embed in the css section of an html page. This has no internal use.
        byte[] imageBytes = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "ancestry-logo-260x50.png"));
        return Convert.ToBase64String(imageBytes);
    }
}