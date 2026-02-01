using Gedcom.Core;

namespace Gedcom.GedcomWriters;

public class GedcomWriter
{
    public static IGedcomWriter Create(Core.Gedcom gedcom, string format)
    {
        return format switch
        {
            Constants.Excel => new ExcelGedcomWriter(gedcom),
            Constants.HTML => new HtmlGedcomWriter(gedcom),
            Constants.JSON => new JsonGedcomWriter(gedcom),
            _ => throw new NotSupportedException($"The format '{format}' is not supported."),
        };
    }
}