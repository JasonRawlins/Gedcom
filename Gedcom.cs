using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gedcom.RecordStructures;

namespace Gedcom;

[JsonConverter(typeof(GedcomJsonConverter))]
public class Gedcom
{
    [JsonIgnore]
    public List<Record> Records { get; } = [];
    public Gedcom(List<GedcomLine> gedcomLines)
    {
        foreach (var level0Record in GetGedcomLinesForLevel(0, gedcomLines))
        {
            Records.Add(new Record(level0Record));
        }
    }

    public FamilyRecord GetFAM(string xrefFAM) => new(Records.First(r => r.Tag.Equals(C.FAM) && r.Value.Equals(xrefFAM)));
    public List<FamilyRecord> GetFAMs() => Records.Where(r => r.Tag.Equals(C.FAM)).Select(r => new FamilyRecord(r)).ToList();
    public IndividualRecord GetINDI(string xrefINDI) => new(Records.First(r => r.Tag.Equals(C.INDI) && r.Value.Equals(xrefINDI)));
    public List<IndividualRecord> GetINDIs() => Records.Where(r => r.Tag.Equals(C.INDI)).Select(r => new IndividualRecord(r)).ToList();
    public SourceCitation GetSOUR(string xrefSOUR) => new(Records.First(r => r.Tag.Equals(C.SOUR) && r.Value.Equals(xrefSOUR)));
    public List<SourceCitation> GetSOURs() => Records.Where(r => r.Tag.Equals(C.SOUR)).Select(r => new SourceCitation(r)).ToList();
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

    public Record CORP => SOUR.Records.First(r => r.Tag.Equals(C.CORP));
    public Record HEAD => Records.First(r => r.Tag.Equals(C.HEAD));
    public Record RIN => _TREE.Records.First(r => r.Tag.Equals(C.RIN));
    public Record SOUR => HEAD.Records.First(r => r.Tag.Equals(C.SOUR));
    public Record _TREE => SOUR.Records.First(r => r.Tag.Equals(_TREE.Tag));

    public override string ToString() => $"{_TREE.Value} ({RIN.Value})";

    public string ToGed() 
    {
        var gedStringBuilder = new StringBuilder();
        foreach (var record in Records)
        {
            gedStringBuilder.Append(GetGedcomLinesText(record));
        }

        return gedStringBuilder.ToString();
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
}

public class GedcomJsonConverter : JsonConverter<Gedcom>
{
    public override Gedcom? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, Gedcom gedcom, JsonSerializerOptions options)
    {
        var jsonObject = new
        {
            Head = new
            {
                Subm = V(gedcom.HEAD[C.SUBM]),
                Source = new
                {
                    Name = V(gedcom.HEAD[C.SOUR]?[C.NAME]),
                    Version = V(gedcom.HEAD[C.VERS]),
                    _Tree = new
                    {
                        rin = V(gedcom.HEAD[C.RIN]),
                        _env = V(gedcom.HEAD[C._ENV]),
                    },
                    Corporation = new
                    {
                        gedcom.CORP.Value,
                        Phone = V(gedcom.CORP[C.PHON]),
                        Www = V(gedcom.CORP[C.WWW]),
                        Address = V(gedcom.CORP[C.ADDR])
                    }
                },
                Date = V(gedcom.HEAD[C.DATE]),
                Gedc = V(gedcom.HEAD[C.GEDC]),
                Char_ = V(gedcom.HEAD[C.CHAR])
            },
            Individuals = gedcom.GetINDIs()
            //Sources = gedcom.GetSOURs(),
            //Families = gedcom.GetFAMs()
        };

        JsonSerializer.Serialize(writer, jsonObject, jsonObject.GetType(), options);
    }

    private static string V(Record? record) => record?.Value ?? "";
}

#region LINEAGE_LINKED_GEDCOM (Structures) p. 23
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