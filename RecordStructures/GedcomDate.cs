using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(GedcomDateJsonConverter))]
public class GedcomDate : RecordStructureBase
{
    public GedcomDate() : base() { }
    public GedcomDate(Record record) : base(record) { }

    public string DateValue => Record.Value;
    public string TimeValue => _(C.TIME);
    public DateTime DateTime => Parse(DateValue);

    public static DateTime Parse(string date)
    {
        if (DateTime.TryParse(date, out var parsedDate))
        {
            return parsedDate;
        }
        
        //var day = "";
        //var month = "";
        //var year = "";

        //var dateParts = date.Split(' ');

        //foreach (var datePart in dateParts)
        //{
        //    if (IsMonth(datePart))
        //    {
        //        // Possible text month name.
        //        month = datePart;
        //    }

        //    if (int.TryParse(datePart, out var possibleDay))
        //    {
        //    }
        //}

        return DateTime.MinValue;
    }

    private static bool IsMonth(string datePart)
    {
        return new string[]
        {
            "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december",
            "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec"
        }.Contains(datePart.ToLower());
    }

    private enum DatePartType
    {
        Day, Month, Year
    }
}

internal class GedcomDateJsonConverter : JsonConverter<GedcomDate>
{
    public override GedcomDate? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, GedcomDate gedcomDate, JsonSerializerOptions options)
    {
        var gedcomDateJson = new GedcomDateJson(gedcomDate);
        JsonSerializer.Serialize(writer, gedcomDateJson, gedcomDateJson.GetType(), options);
    }
}

internal class GedcomDateJson : GedcomJson
{
    public GedcomDateJson(GedcomDate gedcomDate)
    {
        DateValue = JsonString(gedcomDate.DateValue);
        TimeValue = JsonString(gedcomDate.TimeValue);
        DateTime = gedcomDate.DateTime;
    }

    public string? DateValue { get; set; }
    public string? TimeValue { get; set; }
    public DateTime? DateTime { get; set; }
}

#region DATE p. 45
/* 

+1 DATE <TRANSMISSION_DATE> {0:1} p.63
    +2 TIME <TIME_VALUE> {0:1} p.63

*/
#endregion