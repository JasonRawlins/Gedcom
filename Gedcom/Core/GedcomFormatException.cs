namespace Gedcom;

public class GedcomFormatException : Exception
{
    public GedcomLine GedcomLine { get; set; } = new GedcomLine();

    public GedcomFormatException()
    {
    }

    public GedcomFormatException(string message) : base(message)
    {
    }

    public GedcomFormatException(GedcomLine gedcomLine) : base($"Failed to parse GEDCOM line: '{gedcomLine}'")
    {
        GedcomLine = gedcomLine;
    }

    public GedcomFormatException(GedcomLine gedcomLine, string message) : base(message)
    {
        GedcomLine = gedcomLine;
    }

    public GedcomFormatException(GedcomLine gedcomLine, string message, Exception innerException) : base(message, innerException)
    {
        GedcomLine = gedcomLine;
    }
}