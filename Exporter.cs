using Gedcom.RecordStructures;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom;

public class Exporter
{
    private Options Options { get; set; }
    private Gedcom Gedcom { get; set; }
    public Exporter(Gedcom gedcom, Options options)
    {
        Gedcom = gedcom;
        Options = options ?? new Options();
    }

    public string GedcomJson()
    {
        if (Options.Format.ToUpper().Equals(C.JSON))
        {
            return JsonSerializer.Serialize(Gedcom, JsonSerializerOptions);
        }

        return "";
    }

    public string IndividualRecordsJson()
    {
        if (string.IsNullOrEmpty(Options.Xref))
        {
            // If an xref isn't defined, export all individual records.
            if (Options.Format.ToUpper().Equals(C.JSON))
            {
                var individualRecords = new List<IndividualRecord>();

                if (string.IsNullOrEmpty(Options.Query))
                {
                    individualRecords = Gedcom.GetIndividualRecords();
                }
                else
                {
                    individualRecords = Gedcom.GetIndividualRecords(Options.Query);
                }

                var individualRecordsJson = JsonSerializer.Serialize(individualRecords, JsonSerializerOptions);
                return individualRecordsJson;
            }
        }
        else
        {
            // If an xref is defined, export that individualRecord.
            var individualRecord = Gedcom.GetIndividualRecord(Options.Xref, Options.Query);
            var individualRecordJson = JsonSerializer.Serialize(individualRecord, JsonSerializerOptions);
            return individualRecordJson;
        }

        return "";
    }

    public string GedcomListJson()
    {
        var recordJson = "";

        if (Options.RecordType.Equals(C.INDI))
        {
            var individualRecordGedcomList = Gedcom.GetIndividualRecords(Options.Query).Select(ir => new GedcomListItem(ir.Xref, ir.FullName)).ToList();
            recordJson = JsonSerializer.Serialize(individualRecordGedcomList, JsonSerializerOptions);
        }

        if (Options.RecordType.Equals(C.SOUR))
        {
            var sourceRecordGedcomList = Gedcom.GetSourceRecords().Select(sr => new GedcomListItem(sr.Xref, sr.TextFromSource.Text)).ToList();
            recordJson = JsonSerializer.Serialize(sourceRecordGedcomList, JsonSerializerOptions);
        }

        return recordJson;
    }

    public string SourceRecordsJson() => JsonSerializer.Serialize(Gedcom.GetSourceRecords(), JsonSerializerOptions);

    public static JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}

public class GedcomListItem
{
    public GedcomListItem(string xref, string value)
    {
        Xref = xref;
        Value = value;
    }

    public string Xref { get; set; } = "";
    public string Value { get; set; } = "";

    public override string ToString() => $"{Value} ({Xref})";
}
