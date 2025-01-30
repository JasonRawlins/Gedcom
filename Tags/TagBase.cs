namespace Gedcom.Tags;

public class TagBase
{
    protected Record Record { get; }
    public TagBase(Record record) => Record = record;

    public string Value => Record.Value;

    protected Record? FirstOrDefault(string tag) => Record.Records.FirstOrDefault(r => r.Tag.Equals(tag));
    protected List<Record> List(string tag) => Record.Records.Where(r => r.Tag.Equals(tag)).ToList();
    protected List<Record> List(Func<Record, bool> predicate) => Record.Records.Where(predicate).ToList();
    protected string RecordValue(Record? record, string tag) => record?.Records.SingleOrDefault(r => r.Tag.Equals(tag))?.Value ?? "";
    protected string Val(string tag) => Record.Records.SingleOrDefault(r => r.Tag.Equals(tag))?.Value ?? "";
    public override string ToString() => $"{Record.Level} {Record.Tag} {Record.Value.Substring(0, 10)}";
}