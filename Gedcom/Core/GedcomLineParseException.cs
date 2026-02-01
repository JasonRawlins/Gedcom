namespace Gedcom;

public class GedcomLineParseException : Exception
{
    public string Line { get; set; } = "";

    public GedcomLineParseException()
    {
    }

    public GedcomLineParseException(string line) : base($"Failed to parse GEDCOM line: '{line}'")
    {
        Line = line;
    }

    public GedcomLineParseException(string line, string message) : base(message)
    {
        Line = line;
    }

    public GedcomLineParseException(string line, string message, Exception innerException) : base(message, innerException)
    {
        Line = line;
    }
}