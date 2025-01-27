namespace Gedcom.Tags;

public class SOUR : TagBase
{
    public SOUR(Record record) : base(record) { }

    public string ABBR => Value(Tag.ABBR);
    public string AUTH => Value(Tag.AUTH);
    public string PUBL => Value(Tag.PUBL);
    public string REFN => Value(Tag.REFN);
    public string RIN => Value(Tag.RIN);
    public string TEXT => Value(Tag.TEXT);
    public string TITL => Value(Tag.TITL);
    public string XrefSour => Record.Value;

    public override string ToString() => $"{TITL} ({AUTH})";
}
