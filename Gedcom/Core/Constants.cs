using System.Text.RegularExpressions;

namespace Gedcom;

public static partial class Constants // Constants
{
    public const string Empty = "EMPTY"; // For the empty Record
    public const string Excel = "EXCEL";
    public const string HTML = "HTML";
    public const string JSON = "JSON";
    public const string Text = "TEXT";
    public const string XREF = "Xref";

    [GeneratedRegex("^@[0-9a-zA-Z_]{1,22}@$")]
    public static partial Regex XrefRegex();
}