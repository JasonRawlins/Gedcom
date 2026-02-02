using System.Text;

namespace Gedcom;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class GedcomLine
{
    public int Level { get; set; } = -1;
    public string Tag { get; set; } = Constants.Empty;

    // See note 2 below about GedcomLine.Value relationship to an GedcomLine.Xref
    // pointer. Specifically, it emphasizes that a GedcomLine can only have a value or
    // a pointer (Xref), not both. However in code there is no difference. Instead, 
    // GedcomLine.Xref will simply return GedcomLine.Value
    public string Value { get; set; } = "";
    public string Xref => Value;

    public GedcomLine()
    {
    }

    public GedcomLine(int level, string tag)
    {
        Level = level;
        Tag = tag;
    }

    public GedcomLine(int level, string tag, string value) : this(level, tag)
    {
        Value = value;
    }

    // This takes the text of a single line and parses it into a GedcomLine.
    // The format of a top-level record (level 0 record) is Level, Xref, and TAG: "0 @I1234567890@ INDI".
    // For all other lines, the order is Level, Tag, Value (optional): (e.g. "1 NAME John /Doe/", "1 BIRT").
    public static GedcomLine Parse(string line)
    {
        ArgumentNullException.ThrowIfNull(line);

        if (line.Length > 256)
        {
            throw new GedcomLineParseException(line, $"The line is longer than 256 characters. Actual length was {line.Length}.");
        }

        if (string.IsNullOrEmpty(line))
        {
            throw new GedcomLineParseException(line, $"The line is empty.");
        }

        if (line.Length > 0 && Char.IsWhiteSpace(line[0]))
        {
            throw new GedcomLineParseException(line, "The line has leading whitespace.");
        }

        var lineParts = line.Split(' ', 3);

        if (lineParts.Length == 1)
        {
            throw new GedcomLineParseException(line, "The line is missing information after the level.");
        }

        var levelText = lineParts[0];

        if (!int.TryParse(levelText, out var level))
        {
            throw new GedcomLineParseException(line, $"Level must be an integer. Actual value was {levelText}.");
        }

        if (level < 0 || level > 99)
        {
            throw new GedcomLineParseException(line, $"Level must be between 0 and 99. Actual value was {level}.");
        }

        // The format of the line is correct at this point. We just need to determine which
        // segments of the line represent the tag and value. 
        string value = "";
        string tag = "";

        if (level == 0)
        {
            if (lineParts.Length == 2)
            {
                // This will be either HEAD or TRLR
                if (lineParts[1] != Gedcom.Tag.Header && lineParts[1] != Gedcom.Tag.Trailer)
                {
                    throw new GedcomLineParseException(line, "A level 0 record with only two parts must be the HEAD or TRLR tag.");
                }

                tag = lineParts[1];
            }
         
            if (lineParts.Length == 3)
            {
                // The reamining level 0 tags should have a level, xref, and tag).
                // Example: "0 @I1234567890@ INDI".

                value = lineParts[1];
                tag = lineParts[2];
            }
        }
        else
        {
            tag = lineParts[1];

            if (lineParts.Length > 2)
            {
                value = lineParts[2];
            }
        }

        return new GedcomLine
        {
            Level = level,
            Tag = tag,
            Value = value
        };
    }

    public override string ToString()
    {
        return ToString(0);
    }

    // This will not produce a valid Gedcom line if indentLevel is > 0 because it adds 
    // leading whitespace, which is invalid in The Gedcom Standard 5.5.1. It displays
    // the gedcom in a human-readable format.
    public string ToString(int indentLevel)
    {
        var displayLine = new StringBuilder();
        displayLine.Append(Level);

        if (!string.IsNullOrEmpty(Xref))
        {
            displayLine.Append(" " + Xref);
        }

        displayLine.Append(" " + Tag);

        if (!string.IsNullOrEmpty(Value))
        {
            displayLine.Append(" " + Value);
        }

        return new string(' ', Level * indentLevel) + displayLine.ToString();
    }
}

#region Data Representation Grammar (GedcomLine) p. 10
/* 

A GEDCOM transmission consists of a sequence of logical records, each of which consists of a
sequence of gedcom_lines, all contained in a sequential file or stream of characters. The following
rules pertain to the gedcom_line:

Grammar Rules
----------
* Long values can be broken into shorter GEDCOM lines by using a subordinate CONC or CONT
tag. The CONC tag assumes that the accompanying subordinate value is concatenated to the
previous line value without saving the carriage return prior to the line terminator. If a
concatenated line is broken at a space, then the space must be carried over to the next line. The
CONT assumes that the subordinate line value is concatenated to the previous line, after inserting
a carriage return.

* The beginning of a new logical record is designated by a line whose level number is 0 (zero).

* Level numbers must be between 0 to 99 and must not contain leading zeroes, for example, level
one must be 1, not 01.

* Each new level number must be no higher than the previous line plus 1.

* All GEDCOM lines have either a value or a pointer unless the line contains subordinate
GEDCOM lines. The presence of a level number and a tag alone should not be used to assert data
(i.e. 1 FLAG Y not just 1 FLAG to imply that the flag is set).

* Logical GEDCOM record sizes should be constrained so that they will fit in a memory buffer of
less than 32K. GEDCOM files with records sizes greater than 32K run the risk of not being able
to be loaded in some programs. Use of pointers to records, particularly NOTE records, should
ensure that this limit will be sufficient.

* Any length constraints are given in characters, not bytes. When wide characters (characters
11 wider than 8 bits) are used, byte buffer lengths should be adjusted accordingly.

* The cross-reference ID has a maximum of 22 characters, including the enclosing ‘at’ signs (@),
and it must be unique within the GEDCOM transmission.

* Pointers to records imply that the record pointed to does actually exists within the transmission.
Future pointer structures may allow pointing to records within a public accessible database as an
alternative.

* The length of the GEDCOM TAG is a maximum of 31 characters, with the first 15 characters
being unique.

* The total length of a GEDCOM line, including level number, cross-reference number, tag, value,
delimiters, and terminator, must not exceed 255 (wide) characters.

* Leading white space (tabs, spaces, and extra line terminators) preceding a GEDCOM line should
be ignored by the reading system. Systems generating GEDCOM should not place any white
space in front of the GEDCOM line.

Grammar Syntax
-------
A gedcom_line has the following syntax:

gedcom_line:=

level + delim + [optional_xref_ID] + tag + [optional_line_value] + terminator

for example:
1 NAME Will /Rogers/

The components used in the pattern above are defined below in alphabetical order. Some of the
components are defined in terms of other primitive patterns. The spaces used in the patterns below
are only to set them apart and are not a part of the resulting pattern. Character constants are specified
in the hex form (0x20) which is the ASCII hex value of a space character. Character constants that
are separated by a (-) dash represent any character with in that range from the first constant shown to
and including the second constant shown.

Note 2

All GEDCOM lines have either a value or a pointer unless the line contains subordinate
GEDCOM lines. In other words the presence of a level number and a tag alone should not be
used to assert data (i.e. 1 DEAT Y should be used to imply a death known to have happened but
date and place are unknown, not 1 DEAT ). The Lineage-linked form does not allow a
GEDCOM line with both a value and a pointer on the same line.

*/
#endregion