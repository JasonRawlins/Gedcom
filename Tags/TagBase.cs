namespace Gedcom.Tags;

public class TagBase
{
    protected Record Record { get; }
    public TagBase(Record record) => Record = record;

    protected List<Record> GetList(string tag) => Record.Records.Where(r => r.Tag.Equals(tag)).ToList();
    protected List<Record> GetList(Func<Record, bool> predicate) => Record.Records.Where(predicate).ToList();
    protected string RecordValue(Record? record, string tag) => record?.Records.SingleOrDefault(r => r.Tag.Equals(tag))?.Value ?? "";
    protected Record? FirstOrDefault(string tag) => Record.Records.FirstOrDefault(r => r.Tag.Equals(tag));
    protected string SingleValue(string tag) => Record.Records.SingleOrDefault(r => r.Tag.Equals(tag))?.Value ?? "";
}
