using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class DateModel
{
    public DateModel(GedcomDate gedcomDate)
    {
        Day = gedcomDate.Day;
        DayMonthYear = gedcomDate.DayMonthYear;
        Month = gedcomDate.Month;
        MonthName = gedcomDate.MonthName;
        Time = gedcomDate.TimeValue;
        Year = gedcomDate.Year;
    }

    public int Day { get; set; }
    public string DayMonthYear { get; set; }
    public int Month { get; set; }
    public string MonthName { get; set; }
    public string Time { get; set; }
    public int Year { get; set; }

    public override string ToString() => DayMonthYear;
}