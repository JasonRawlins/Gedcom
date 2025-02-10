using Gedcom.RecordStructures;
using System.Reflection.Metadata.Ecma335;

namespace Gedcom;

public class RecordStructureBase
{
    protected Record Record { get; private set; } = Record.Default;

    public RecordStructureBase() { }
    public RecordStructureBase(Record record) => Record = record;
    internal void SetRecord(Record record) => Record = record;

    protected Record First(string tag) => Record.Records.FirstOrDefault(r => r.Tag.Equals(tag)) ?? Record.Default; 
    //protected List<Record> List(string tag) => Record.Records.Where(r => r.Tag.Equals(tag)).ToList();
    protected List<Record> List(Func<Record, bool> predicate) => Record.Records.Where(predicate).ToList();
    protected T FirstOrDefault<T>(string tag) where T : RecordStructureBase, new() => CreateRecordStructure<T>(tag);
    protected List<T> List<T>(string tag) where T : RecordStructureBase, new() => CreateRecordStructures<T>(tag);
    private T CreateRecordStructure<T>(string tag) where T: RecordStructureBase, new()
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
    protected string RecordValue(Record? record, string tag) => record?.Records.SingleOrDefault(r => r.Tag.Equals(tag))?.Value ?? "";
    protected string Text
    {
        get
        {
            var textRecord = FirstOrDefault<NoteRecord>(C.TEXT);
            var text = "";
            if (textRecord != null)
            {
                var contAndConc = Record.Records.Where(r => r.Tag.Equals(C.CONT) || r.Tag.Equals(C.CONC));

                foreach (var contOrConc in contAndConc)
                {
                    text += contOrConc.Value;
                }
            }

            return text;
        }
    }
    // The method name "V" stands for Value. It's used so often that I shortened it to make the code easier to read.
    protected string V(string tag) => Record?.Records.SingleOrDefault(r => r.Tag.Equals(tag))?.Value ?? "";
    public string Xref => Record.Level == 0 ? Record.Value : "";
    public override string ToString() => $"{Record.Level} {Record.Tag} {Record.Value.Substring(0, 10)}";
}



