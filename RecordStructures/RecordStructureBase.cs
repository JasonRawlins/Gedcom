using Gedcom.RecordStructures;

namespace Gedcom;

public class RecordStructureBase
{
    protected Record Record { get; private set; } = Record.Empty;
    
    public Record this[string tag] => this[[tag]];
    public Record this[IEnumerable<string> tagPath]
    {
        get
        {
            var currentRecord = First(tagPath.First());

            if (currentRecord.IsEmpty || currentRecord.Records.Count == 1)
            {
                return currentRecord;
            }

            foreach (var tag in tagPath.Skip(1))
            {
                currentRecord = currentRecord.Records.FirstOrDefault(r => r.Tag.Equals(tag)) ?? Record.Empty;
            }

            return currentRecord;
        }
    }

    public RecordStructureBase() { }
    public RecordStructureBase(Record record) => Record = record;
    internal void SetRecord(Record record) => Record = record;

    // The method "_" finds a child record value by tag name. 
    protected string _(string tag) => First(tag).Value;
    protected Record First(string tag) => Record.Records.FirstOrDefault(r => r.Tag.Equals(tag)) ?? Record.Empty;
    protected List<Record> List(Func<Record, bool> predicate) => Record.Records.Where(predicate).ToList();
    protected Record Single(Func<Record, bool> predicate) => Record.Records.SingleOrDefault(predicate) ?? Record.Empty;
    protected List<string> ListValues(string tag) => Record.Records.Where(r => r.Tag.Equals(tag)).Select(r => r.Value).ToList();
    protected T First<T>(string tag) where T : RecordStructureBase, new() => CreateRecordStructureList<T>(tag, Record).FirstOrDefault() ?? Empty<T>();
    protected List<T> List<T>(string tag) where T : RecordStructureBase, new() => CreateRecordStructureList<T>(tag, Record);
    public static T Empty<T>() where T : RecordStructureBase, new() => CreateRecordStructure<T>(Record.Empty);
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

    public override string ToString()
    {
        if (Record.Level == 0)
        {
            return $"{Record.Level} {Record.Value} {Record.Tag}";
        }
        else
        {
            return $"{Record.Level} {Record.Tag} {Record.Value}";
        }
    } 
}

