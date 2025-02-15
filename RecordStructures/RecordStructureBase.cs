using Gedcom.RecordStructures;
using System.Text;

namespace Gedcom;

public class RecordStructureBase
{
    protected Record Record { get; private set; } = Record.Default;

    public RecordStructureBase() { }
    public RecordStructureBase(Record record) => Record = record;
    internal void SetRecord(Record record) => Record = record;

    protected Record First(string tag) => Record.Records.FirstOrDefault(r => r.Tag.Equals(tag)) ?? Record.Default;
    protected List<Record> List(Func<Record, bool> predicate) => Record.Records.Where(predicate).ToList();
    protected T FirstOrDefault<T>(string tag) where T : RecordStructureBase, new() => CreateRecordStructure<T>(tag);
    protected List<T> List<T>(string tag) where T : RecordStructureBase, new() => CreateRecordStructures<T>(tag);
    private T CreateRecordStructure<T>(string tag) where T : RecordStructureBase, new()
    {
        var record = Record.Records.FirstOrDefault(r => r.Tag.Equals(tag));
        if (record != null)
        {
            dynamic dynamicRecord = new T();
            dynamicRecord.SetRecord(record);
            return (T)dynamicRecord;
        }

        var emptyRecord = new T();
        emptyRecord.SetRecord(Record.Default);

        return emptyRecord;
    }
    private List<T> CreateRecordStructures<T>(string tag) where T : RecordStructureBase, new()
    {
        var records = Record.Records.Where(r => r.Tag.Equals(tag));
        if (records.Any())
        {
            return records.Select(r =>
            {
                dynamic dynamicRecord = new T();
                dynamicRecord.SetRecord(r);
                return (T)dynamicRecord;
            }).ToList();
        }

        return [];
    }
    // The method _() finds a child record value by tag name. 
    protected string _(string tag) => Record.Records.SingleOrDefault(r => r.Tag.Equals(tag))?.Value ?? "";
    public override string ToString() => $"{Record.Level} {Record.Tag} {Record.Value}";

    public static T Default<T>() where T : RecordStructureBase, new()
    {
        dynamic dynamicObject = new T();
        dynamicObject.SetRecord(Record.Default);
        return (T)dynamicObject;
    }
}

