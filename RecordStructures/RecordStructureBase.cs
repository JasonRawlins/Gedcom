using Gedcom.RecordStructures;
using System.Formats.Asn1;

namespace Gedcom;

public class RecordStructureBase
{
    // This probably shouldn't be public. Do I need to make a method that takes a path to the desired record?
    protected Record Record { get; private set; } = Record.Default;

    public Record this[string tag] => this[[tag]];
    public Record this[IEnumerable<string> tagPath]
    {
        get
        {
            Record currentRecord = First(tagPath.First());

            if (currentRecord.IsEmpty) return Record.Default; 
            if (currentRecord.Records.Count == 1) return currentRecord;

            foreach (var tag in tagPath.Skip(1))
            {
                currentRecord = currentRecord.Records.First(r => r.Tag.Equals(tag));
            }

            return currentRecord;
        }
    }

    public RecordStructureBase() { }
    public RecordStructureBase(Record record) => Record = record;
    internal void SetRecord(Record record) => Record = record;

    // The method "_" finds a child record value by tag name. 
    protected string _(string tag) => Record.Records.FirstOrDefault(r => r.Tag.Equals(tag))?.Value ?? "";
    protected Record First(string tag) => Record.Records.FirstOrDefault(r => r.Tag.Equals(tag)) ?? Record.Default;
    protected List<Record> List(Func<Record, bool> predicate) => Record.Records.Where(predicate).ToList();
    protected List<string> ListAsStrings(string tag) => Record.Records.Where(r => r.Tag.Equals(tag)).Select(r => r.Value).ToList();
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

