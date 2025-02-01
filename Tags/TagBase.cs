namespace Gedcom.Tags;

public class TagBase
{
    protected Record Record { get; private set; }

    public TagBase() => Record = new Record([]);
    public TagBase(Record record) => Record = record;

    public string Value => Record.Value;

    public void SetRecord(Record record)
    {
        Record = record;
    }

    public static T? GetSubrecord<T>(TagBase tagBase, string tag) where T : TagBase, new()
    {
        var record = tagBase.FirstOrDefault(tag);
        if (record != null)
        {
            dynamic dynamicRecord = new T();
            dynamicRecord.SetRecord(dynamicRecord);
            return dynamicRecord;
        }

        return null;
    }

    protected Record? FirstOrDefault(string tag) => Record.Records.FirstOrDefault(r => r.Tag.Equals(tag));
    protected List<Record> List(string tag) => Record.Records.Where(r => r.Tag.Equals(tag)).ToList();
    protected List<Record> List(Func<Record, bool> predicate) => Record.Records.Where(predicate).ToList();
    protected string RecordValue(Record? record, string tag) => record?.Records.SingleOrDefault(r => r.Tag.Equals(tag))?.Value ?? "";
    // The method name "V" stands for Value. It's used so often that I shortened it to make the code cleaner.
    protected string V(string tag) => Record.Records.SingleOrDefault(r => r.Tag.Equals(tag))?.Value ?? "";
    public override string ToString() => $"{Record.Level} {Record.Tag} {Record.Value.Substring(0, 10)}";
}