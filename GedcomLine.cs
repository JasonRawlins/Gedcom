using System.Text;

namespace Gedcom;

public class GedcomLine
{
    public int Level { get; set; } = -1;
    public string XrefId { get; set; } = "";
    public string Tag { get; set; } = "";
    public string Value { get; set; } = "";

    public override string ToString()
    {
        return ToString(0);
    }

    public string ToString(int indentLevel)
    {
        var displayLine = new StringBuilder();
        displayLine.Append(Level);

        if (!string.IsNullOrEmpty(XrefId))
        {
            displayLine.Append(" " + XrefId);
        }

        displayLine.Append(" " + Tag);

        if (!string.IsNullOrEmpty(Value))
        {
            displayLine.Append(" " + Value);
        }

        return new string(' ', Level * indentLevel) + displayLine.ToString();
    }
}