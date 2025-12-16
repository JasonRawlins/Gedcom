using Gedcom.RecordStructures;

namespace Gedcom.CLI;

public class GedcomWriter
{
    public static IGedcomWriter Create(Gedcom gedcom, string format)
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