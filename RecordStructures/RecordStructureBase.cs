using Gedcom.RecordStructures;
using System.Text;

namespace Gedcom;

public class RecordStructureBase
{
    protected Record Record { get; private set; } = Record.Default;

    public RecordStructureBase() { }
    public RecordStructureBase(Record record) => Record = record;
    internal void SetRecord(Record record) => Record = record;

    // The method "_" finds a child record value by tag name. 
    protected string _(string tag) => Record.Records.FirstOrDefault(r => r.Tag.Equals(tag))?.Value ?? "";
    protected Record First(string tag) => Record.Records.FirstOrDefault(r => r.Tag.Equals(tag)) ?? Record.Default;
    protected List<Record> List(Func<Record, bool> predicate) => Record.Records.Where(predicate).ToList();
    protected T FirstOrDefault<T>(string tag) where T : RecordStructureBase, new() => CreateRecordStructureList<T>(tag, Record).FirstOrDefault() ?? Default<T>();
    protected List<T> List<T>(string tag) where T : RecordStructureBase, new() => CreateRecordStructureList<T>(tag, Record);
    public static T Default<T>() where T : RecordStructureBase, new() => CreateRecordStructure<T>(Record.Default);
    private static List<T> CreateRecordStructureList<T>(string tag, Record record) where T : RecordStructureBase, new()
    {
        var records = record.Records.Where(r => r.Tag.Equals(tag));
        if (records.Any())
        {
            return records.Select(CreateRecordStructure<T>).ToList();
        }

        return [];
    }

    private static T CreateRecordStructure<T>(Record record) where T : RecordStructureBase, new()
    {
        dynamic dynamicRecord = new T();
        dynamicRecord.SetRecord(record);
        return (T)dynamicRecord;
    }

    public override string ToString() => $"{Record.Level} {Record.Tag} {Record.Value}";
}

