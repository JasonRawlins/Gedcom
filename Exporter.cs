using Gedcom.RecordStructures;
using System.Diagnostics;
using Newtonsoft.Json;

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
            var json = JsonConvert.SerializeObject(Gedcom, JsonSettings.DefaultOptions);
            return json;
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

                return JsonConvert.SerializeObject(individualRecords);
            }
        }
        else
        {
            // If an xref is defined, export that individualRecord.
            var individualRecord = Gedcom.GetIndividualRecord(Options.Xref, Options.Query);
            return JsonConvert.SerializeObject(individualRecord);
        }

        return "";
    }

    //public string GedcomListJson()
    //{
    //    var recordJson = "";

    //    if (Options.RecordType.Equals(C.INDI))
    //    {
    //        var individualRecordGedcomList = Gedcom.GetIndividualRecords(Options.Query).Select(ir => new GedcomListItem(ir.Xref, ir.FullName)).ToList();
    //        recordJson = JsonConvert.SerializeObject(individualRecordGedcomList);
    //    }

    //    if (Options.RecordType.Equals(C.SOUR))
    //    {
    //        var sourceRecordGedcomList = Gedcom.GetSourceRecords().Select(sr => new GedcomListItem(sr.Xref, sr.TextFromSource.Text)).ToList();
    //        recordJson = JsonConvert.SerializeObject(sourceRecordGedcomList);
    //    }

    //    return recordJson;
    //}

    public string SourceRecordsJson()
    {
        var stopwatch = Stopwatch.StartNew();
        var sourceRecords = Gedcom.GetSourceRecords();
        stopwatch.Stop();
        var ellapsedMS = stopwatch.ElapsedMilliseconds;

        stopwatch.Restart();
        var json = JsonConvert.SerializeObject(sourceRecords);
        stopwatch.Stop();
        var ms = stopwatch.ElapsedMilliseconds;

        return json;
    }
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
