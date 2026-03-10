namespace Gedcom.GedcomWriters;

public class GedcomWriter
{
    public static IGedcomWriter Create(GedcomDocument gedcom, string format)
    {
        var formatUpperCase = format.ToUpper();
        return format switch
        {
            Constants.Excel => new ExcelGedcomWriter(gedcom),
            Constants.Html => new HtmlGedcomWriter(gedcom),
            Constants.Json => new JsonGedcomWriter(gedcom),
            Constants.Text => new TextGedcomWriter(gedcom),
            _ => throw new NotSupportedException($"The format '{format}' is not supported."),
        };
    }
}