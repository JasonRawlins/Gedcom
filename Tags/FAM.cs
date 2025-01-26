namespace Gedcom.Tags;

public class FAM : TagBase
{
    public FAM(Record record) : base(record) { }

    public List<Record> Partners => GetList(r => r.Tag.Equals(Tag.WIFE) || r.Tag.Equals(Tag.HUSB));

    public override string ToString() => $"({string.Join(',', Partners)})";
}
