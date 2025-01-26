namespace Gedcom.Tags;

public class SOUR : TagBase
{
    public SOUR(Record record) : base(record) { }

    public string TITL => this.SingleValue(Tag.TITL);

    public override string ToString()
    {
        return TITL;
    }
}
