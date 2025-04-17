using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gedcom.RecordStructures;

namespace Gedcom;

[JsonConverter(typeof(GedcomJsonConverter))]
public class Gedcom : RecordStructureBase
{
    public Gedcom(List<GedcomLine> gedcomLines)
    {
        foreach (var level0Record in GetGedcomLinesForLevel(0, gedcomLines))
        {
            Record.Records.Add(new Record(level0Record));
        }
    }

    public Header Header => First<Header>(C.HEAD);
   
    public FamilyRecord GetFamilyRecord(string xrefFAM) => new(Record.Records.First(r => r.Tag.Equals(C.FAM) && r.Value.Equals(xrefFAM)));
    public List<FamilyRecord> GetFamilyRecords() => Record.Records.Where(r => r.Tag.Equals(C.FAM)).Select(r => new FamilyRecord(r)).ToList();

    public IndividualRecord GetIndividualRecord(string xrefINDI)
    {
        var individualRecord = Single(r => r.Tag.Equals(C.INDI) && r.Value.Equals(xrefINDI));

        if (individualRecord.IsEmpty)
        {
            return Empty<IndividualRecord>();
        }

        return new IndividualRecord(individualRecord);
    }

    public List<IndividualRecord> GetIndividualRecords() => Record.Records.Where(r => r.Tag.Equals(C.INDI)).Select(r => new IndividualRecord(r)).ToList();
    
    public MultimediaRecord GetMultimediaRecord(string xrefOBJE) => new(Record.Records.First(r => r.Tag.Equals(C.OBJE) && r.Value.Equals(xrefOBJE)));
    public List<MultimediaRecord> GetMultimediaRecords() => Record.Records.Where(r => r.Tag.Equals(C.OBJE)).Select(r => new MultimediaRecord(r)).ToList();

    public NoteRecord GetNoteRecord(string xrefNOTE) => new(Record.Records.First(r => r.Tag.Equals(C.NOTE) && r.Value.Equals(xrefNOTE)));
    public List<NoteRecord> GetNoteRecords() => Record.Records.Where(r => r.Tag.Equals(C.NOTE)).Select(r => new NoteRecord(r)).ToList();
    
    public RepositoryRecord GetRepositoryRecord(string xrefREPO) => new(Record.Records.First(r => r.Tag.Equals(C.REPO) && r.Value.Equals(xrefREPO)));
    public List<RepositoryRecord> GetRepositoryRecords() => Record.Records.Where(r => r.Tag.Equals(C.REPO)).Select(r => new RepositoryRecord(r)).ToList();
   
    public SourceRecord GetSourceRecord(string xrefSOUR) => new(Record.Records.First(r => r.Tag.Equals(C.SOUR) && r.Value.Equals(xrefSOUR)));
    public List<SourceRecord> GetSourceRecords() => Record.Records.Where(r => r.Tag.Equals(C.SOUR)).Select(r => new SourceRecord(r)).ToList();

    public SubmitterRecord GetSubmitterRecord(string xrefSUBM) => new(Record.Records.First(r => r.Tag.Equals(C.SUBM) && r.Value.Equals(xrefSUBM)));
    public List<SubmitterRecord> GetSubmitterRecords() => Record.Records.Where(r => r.Tag.Equals(C.SUBM)).Select(r => new SubmitterRecord(r)).ToList();

    public static List<List<GedcomLine>> GetGedcomLinesForLevel(int level, List<GedcomLine> gedcomLines)
    {
        var gedcomLinesAtThisLevel = new List<List<GedcomLine>>();
        var currentGedcomLines = new List<GedcomLine>();

        foreach (var gedcomLine in gedcomLines)
        {
            if (gedcomLine.Level == level)
            {
                gedcomLinesAtThisLevel.Add(currentGedcomLines);
                currentGedcomLines = [gedcomLine];                
            }
            else
            {
                currentGedcomLines.Add(gedcomLine);
            }
        }

        gedcomLinesAtThisLevel.Add(currentGedcomLines);

        return gedcomLinesAtThisLevel.Skip(1).ToList();
    }

    private static string GetGedcomLinesText(Record record)
    {
        var gedcomLinesStringBuilder = new StringBuilder();
        gedcomLinesStringBuilder.AppendLine(new string(' ', record.Level * 1) + record);

        foreach (var recursiveRecord in record.Records)
        {
            gedcomLinesStringBuilder.Append(GetGedcomLinesText(recursiveRecord));
        }

        return gedcomLinesStringBuilder.ToString();
    }

    public string ToGed()
    {
        var gedStringBuilder = new StringBuilder();
        foreach (var record in Record.Records)
        {
            gedStringBuilder.Append(GetGedcomLinesText(record));
        }

        return gedStringBuilder.ToString();
    }

    public override string ToString() => "Gedcom.ToString()"; // $"{_TREE.Value} ({RIN.Value})";
}

internal class GedcomJsonConverter : JsonConverter<Gedcom>
{
    public override Gedcom? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, Gedcom gedcom, JsonSerializerOptions options)
    {
        //var individual1 = gedcom.GetIndividualRecord("@I1@");
        //var source1 = gedcom.GetSourceRecord("@S1@");
        var allIndividualRecords = gedcom.GetIndividualRecords();

        var jsonObject = new
        {
            Individuals = allIndividualRecords
        };

        JsonSerializer.Serialize(writer, jsonObject, jsonObject.GetType(), options);
    }
}

#region LINEAGE_LINKED_GEDCOM p. 23
/*

LINEAGE_LINKED_GEDCOM:=

0 <<HEADER>> {1:1} p.23
0 <<SUBMISSION_RECORD>> {0:1} p.28
0 <<RECORD>> {1:M} p.24
0 TRLR {1:1}

This is a model of the lineage-linked GEDCOM structure for submitting data to other lineage-linked
GEDCOM processing systems. A header and a trailer record are required, and they can enclose any
number of data records. Tags from Appendix A (see page 83) must be used in the same context as
shown in the following form. User defined tags (see <NEW_TAG> on page 56) are discouraged but
when used must begin with an under-score. Tags that are required within a desired context have been
bolded. Note that some contexts are not required but if they are used then the bolded tags are
required.

*/
#endregion