using Gedcom.Core;

namespace Gedcom.GedcomWriters;

public class GedcomWriter
{
    public static IGedcomWriter Create(Core.Gedcom gedcom, string format)
    {
        return format switch
        {
            C.Excel => new ExcelGedcomWriter(gedcom),
            C.HTML => new HtmlGedcomWriter(gedcom),
            C.JSON => new JsonGedcomWriter(gedcom),
            _ => throw new NotSupportedException($"The format '{format}' is not supported."),
        };
    }
}