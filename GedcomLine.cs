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
        var displayLine = new StringBuilder(Level.ToString());

        if (!String.IsNullOrEmpty(XrefId))
        {
            displayLine.Append(" " + XrefId);
        }

        displayLine.Append(" " + Tag);

        if (!String.IsNullOrEmpty(Value))
        {
            displayLine.Append(" " + Value);
        }

        return displayLine.ToString();
    }
}