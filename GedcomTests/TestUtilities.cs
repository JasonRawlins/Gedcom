using Gedcom.Core;
using System.Text;

namespace GedcomTests;

public class TestUtilities
{
    private static readonly string BaseDirectory = @"C:\temp\GedcomNET"; //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNET.json");
    public static readonly string ExcelFullName = Path.Combine(BaseDirectory, "TestOutput", "GedcomNET.xlsx");
    public static readonly string HtmlFullName = Path.Combine(BaseDirectory, "TestOutput", "GedcomNET.html");
    public static readonly string JsonFullName = Path.Combine(BaseDirectory, "TestOutput", "GedcomNET.json");
    public static readonly string TextFullName = Path.Combine(BaseDirectory, "TestOutput", "GedcomNET.txt");

    public static Gedcom.Core.Gedcom CreateGedcom()
    {
        var gedFileLines = Encoding.UTF8.GetString(Properties.Resources.GedcomNetTestTree).Split('\n');
        var gedcomLines = gedFileLines.Where(l => !string.IsNullOrEmpty(l)).Select(GedcomLine.Parse).ToList();
        return new Gedcom.Core.Gedcom(gedcomLines);
    }

    public static string GetImageBase64String()
    {
        // This function finds an image on disc and generates its Base64 string. I only use it to generate
        // bytes that I can then embed in the css section of an html page. This has no internal use.
        byte[] imageBytes = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "ancestry-logo-260x50.png"));
        return Convert.ToBase64String(imageBytes);
    }
}