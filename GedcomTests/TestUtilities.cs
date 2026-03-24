using Gedcom;
using System.IO.Compression;
using System.Text;

namespace GedcomTests;

public class TestUtilities
{
    public static readonly string InputFilesDirectory = @"C:\temp\GedcomNet\InputFiles"; //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNet.json");
    public static readonly string OutputFilesDirectory = @"C:\temp\GedcomNet\OutputFiles"; //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "GedcomNet.json");

    public static GedcomDocument CreateGedcom()
    {
        var gedFileLines = Encoding.UTF8.GetString(Properties.Resources.GedcomNetTestTree)
            .Split(GedcomDocument.LineEnding, StringSplitOptions.RemoveEmptyEntries);

        var gedcomLines = gedFileLines.Where(l => !string.IsNullOrEmpty(l)).Select(GedcomLine.Parse).ToList();
        return new GedcomDocument(gedcomLines);
    }

    public static string GetSharedStringsFromExcel(byte[] xlsxBytes)
    {
        var xmlFiles = new Dictionary<string, string>();

        using var stream = new MemoryStream(xlsxBytes);
        using var zip = new ZipArchive(stream, ZipArchiveMode.Read);

        foreach (var entry in zip.Entries)
        {
            if (entry.Name.EndsWith(".xml") || entry.Name.EndsWith(".rels"))
            {
                using var entryStream = entry.Open();
                using var reader = new StreamReader(entryStream, Encoding.UTF8);
                xmlFiles[entry.FullName] = reader.ReadToEnd();
            }
        }

        return xmlFiles.Single(x => x.Key == "xl/sharedStrings.xml").Value;
    }

    public static string GetImageBase64String()
    {
        // This function finds an image on disc and generates its Base64 string. I only use it to generate
        // bytes that I can then embed in the css section of an html page. This has no internal use.
        byte[] imageBytes = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "ancestry-logo-260x50.png"));
        return Convert.ToBase64String(imageBytes);
    }
}