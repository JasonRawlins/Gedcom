namespace Gedcom.Tags;

public class SOUR
{
    private Record Record { get; }
    public SOUR(Record record)
    {
        Record = record;
    }

    public string TITL => Record.Records.Single(r => r.Tag.Equals(Tag.TITL)).Value;

    public override string ToString()
    {
        return base.ToString();
    }
}
