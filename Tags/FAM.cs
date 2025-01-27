namespace Gedcom.Tags;

public class FAM : TagBase
{
    public FAM(Record record) : base(record) { }

    public List<Record> Partners => List(r => r.Tag.Equals(T.WIFE) || r.Tag.Equals(T.HUSB));

    public override string ToString() => $"({string.Join(',', Partners)})";
}
