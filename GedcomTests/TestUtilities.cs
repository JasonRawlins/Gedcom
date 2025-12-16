using Gedcom;
using Gedcom.CLI;

namespace GedcomTests;

public class TestUtilities
{
    private static readonly string BaseDirectory = @"C:\temp\GedcomNET"; //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNET.json");

    public static readonly string GedcomFullName = Path.Combine(BaseDirectory, "Resources", "GedcomNET.ged");

    public static readonly string ExcelTemplateFullName = Path.Combine(BaseDirectory, "Resources", "GedcomNET-template.xlsx");
    //public static readonly string GedcomDirectory = Path.GetDirectoryName(GedcomFullName) ?? "";

    public static readonly string ExcelFullName = Path.Combine(BaseDirectory, "TestOutput", "GedcomNET.xlsx");
    public static readonly string HtmlFullName = Path.Combine(BaseDirectory, "TestOutput", "GedcomNET.html");
    public static readonly string JsonFullName = Path.Combine(BaseDirectory, "TestOutput", "GedcomNET.json");
    public static readonly string TextFullName = Path.Combine(BaseDirectory, "TestOutput", "GedcomNET.txt");

    public static Gedcom.Gedcom CreateGedcom()
    {
        var gedFileLines = File.ReadAllLines(GedcomFullName); //GedcomNetTreeFullNameEncoding.UTF8.GetString(Properties.Resources.GedcomNET).Split('\n');
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