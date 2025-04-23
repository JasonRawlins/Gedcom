using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(GedcomDateJsonConverter))]
public class GedcomDate : RecordStructureBase, IComparable<GedcomDate>
{
    public GedcomDate() : base() { }
    public GedcomDate(Record record) : base(record) { }

    public string DateValue => Record.Value;
    public int Day { get; set; }
    public string DayMonthYear
    {
        get
        {
            var stringBuilder = new StringBuilder();

            if (Day > 0)
            {
                stringBuilder.Append(Day);
            }

            if (Month > 0)
            {
                if (Day > 0) { stringBuilder.Append(" "); }
                stringBuilder.Append(MonthName);
            }

            if (Year > 0)
            {
                if (Month > 0) { stringBuilder.Append(" "); }
                stringBuilder.Append(Year);
            }

            return stringBuilder.ToString();
        }
    }
    public int Month { get; set; }
    public string MonthName { get; set; } = "";
    public string TimeValue => _(C.TIME);
    public int Year { get; set; }

    public int CompareTo(GedcomDate? other)
    {
        if (other == null) return 1;

        // Compare by year first, then month, then day
        int yearComparison = Year.CompareTo(other.Year);
        if (yearComparison != 0) return yearComparison;

        int monthComparison = Month.CompareTo(other.Month);
        if (monthComparison != 0) return monthComparison;

        return Day.CompareTo(other.Day);
    }
    private static int GetMonthNumber(string monthName)
    {
        return monthName.ToUpper() switch
        {
            "JANUARY" => 1,
            "JAN" => 1,
            "FEBRUARY" => 2,
            "FEB" => 2,
            "MARCH" => 3,
            "MAR" => 3,
            "APRIL" => 4,
            "APR" => 4,
            "MAY" => 5,
            "JUNE" => 6,
            "JUN" => 6,
            "JULY" => 7,
            "JUL" => 7,
            "AUGUST" => 8,
            "AUG" => 8,
            "SEPTEMBER" => 9,
            "SEP" => 9,
            "OCTOBER" => 10,
            "OCT" => 10,
            "NOVEMBER" => 11,
            "NOV" => 11,
            "DECEMBER" => 12,
            "DEC" => 12,
            _ => 0
        };
    }
    private static bool IsMonth(string possibleMonthName)
    {
        var monthNameVariations = new string[]
        {
            "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER",
            "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"
        };

        return monthNameVariations.Contains(possibleMonthName.ToUpper());
    }
    public static GedcomDate Parse(string date, string time = "")
    {
        // Dynamically constructing a record like this is pretty sketchy. Make sure it's a valid use case.
        var dateGedcomLine = GedcomLine.Parse($"1 DATE {date}");
        var gedcomLines = new List<GedcomLine> { dateGedcomLine };
        if (!string.IsNullOrWhiteSpace(time))
        {
            var timeGedcomLine = GedcomLine.Parse($"2 TIME {time}");
            gedcomLines.Add(timeGedcomLine);
        }

        var gedcomDate = new GedcomDate(new Record(gedcomLines));
        var dateParts = date.Split(" ");

        if (dateParts.Length == 3 && DateTime.TryParse(gedcomDate.DateValue, out var parsedDate))
        {
            gedcomDate.Day = parsedDate.Day;
            gedcomDate.Month = parsedDate.Month;
            gedcomDate.MonthName = parsedDate.ToString("MMM");
            gedcomDate.Year = parsedDate.Year;

            return gedcomDate;
        }

        // The date cannot be parsed by DateTime.Parse() at this point and needs custom parsing.


        foreach (var datePart in dateParts)
        {
            if (IsMonth(datePart))
            {
                gedcomDate.Month = GetMonthNumber(datePart);
                gedcomDate.MonthName = datePart;
            }

            if (int.TryParse(datePart, out var intDatePart))
            {
                if (datePart.Length == 4)
                {
                    gedcomDate.Year = intDatePart;
                }

                if ((datePart.Length == 1 || datePart.Length == 2) && (intDatePart > 0 && intDatePart <= 31))
                {
                    gedcomDate.Day = intDatePart;
                }
            }
        }

        return gedcomDate;
    }

    public override string ToString() => $"{Record.Value}, {DayMonthYear}, (Raw: {DateValue})";
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
        Day = gedcomDate.Day;
        DayMonthYear = gedcomDate.DayMonthYear;
        Month = gedcomDate.Month;
        MonthName = gedcomDate.MonthName;
        Time = gedcomDate.TimeValue;
        Year = gedcomDate.Year;
    }

    public int? Day { get; set; }
    public string? DayMonthYear { get; set; }
    public int? Month { get; set; }
    public string? MonthName { get; set; } = "";
    public string? Time { get; set; }
    public int? Year { get; set; }
}

#region DATE p. 45
/* 

+1 DATE <TRANSMISSION_DATE> {0:1} p.63
    +2 TIME <TIME_VALUE> {0:1} p.63

*/
#endregion