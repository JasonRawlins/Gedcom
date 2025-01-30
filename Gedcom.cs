using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gedcom.Tags;

namespace Gedcom;

[JsonConverter(typeof(GedcomJsonConverter))]
public class Gedcom
{
    public List<Record> Records { get; } = [];
    public Gedcom(List<GedcomLine> gedcomLines)
    {
        foreach (var level0Record in GetGedcomLinesForLevel(0, gedcomLines))
        {
            Records.Add(new Record(level0Record));
        }
    }

    public FAM GetFAM(string xrefFAM) => new(Records.First(r => r.Tag.Equals(C.FAM) && r.Value.Equals(xrefFAM)));
    public List<FAM> GetFAMs() => Records.Where(r => r.Tag.Equals(C.FAM)).Select(r => new FAM(r)).ToList();
    public INDI GetINDI(string xrefINDI) => new(Records.First(r => r.Tag.Equals(C.INDI) && r.Value.Equals(xrefINDI)));
    public List<INDI> GetINDIs() => Records.Where(r => r.Tag.Equals(C.INDI)).Select(r => new INDI(r)).ToList();
    public SOUR GetSOUR(string xrefSOUR) => new(Records.First(r => r.Tag.Equals(C.SOUR) && r.Value.Equals(xrefSOUR)));
    public List<SOUR> GetSOURs() => Records.Where(r => r.Tag.Equals(C.SOUR)).Select(r => new SOUR(r)).ToList();
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
    public string Value(Record? record) => record?.Value ?? "";

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

    private string GetGedcomLinesText(Record record)
    {
        var gedcomLinesStringBuilder = new StringBuilder();
        gedcomLinesStringBuilder.AppendLine(new String(' ', record.Level * 1) + record);

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
                Subm = Value(gedcom.HEAD[C.SUBM]),
                Source = new
                {
                    Name = Value(gedcom.HEAD[C.SOUR]?[C.NAME]),
                    Version = Value(gedcom.HEAD[C.VERS]),
                    _Tree = new
                    {
                        rin = Value(gedcom.HEAD[C.RIN]),
                        _env = Value(gedcom.HEAD[C._ENV]),
                    },
                    Corporation = new
                    {
                        gedcom.CORP.Value,
                        Phone = Value(gedcom.CORP[C.PHON]),
                        Www = Value(gedcom.CORP[C.WWW]),
                        Address = Value(gedcom.CORP[C.ADDR])
                    }
                },
                Date = Value(gedcom.HEAD[C.DATE]),
                Gedc = Value(gedcom.HEAD[C.GEDC]),
                Char_ = Value(gedcom.HEAD[C.CHAR])
            },
            Individuals = gedcom.GetINDIs(),
            Sources = gedcom.GetSOURs(),
            Families = gedcom.GetFAMs()
        };

        JsonSerializer.Serialize(writer, jsonObject, jsonObject.GetType(), options);
    }

    private string Value(Record? record) => record?.Value ?? "";
}

#region LINEAGE_LINKED_GEDCOM (Structures) p. 23
/*
https://gedcom.io/specifications/ged551.pdf

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