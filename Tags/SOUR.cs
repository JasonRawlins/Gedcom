namespace Gedcom.Tags;

public class SOUR : TagBase
{
    public SOUR(Record record) : base(record) { }

    public string ABBR => Value(T.ABBR);
    public string AUTH => Value(T.AUTH);
    public string PUBL => Value(T.PUBL);
    public string REFN => Value(T.REFN);
    public string RIN => Value(T.RIN);
    public string TEXT => Value(T.TEXT);
    public string TITL => Value(T.TITL);
    public string XrefSour => Record.Value;

    public override string ToString() => $"{TITL} ({AUTH})";
}
