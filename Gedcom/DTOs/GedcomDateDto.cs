using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

public class GedcomDateDto(GedcomDate gedcomDate) : GedcomDto, IComparable<GedcomDateDto>
{
    public string DateValue { get; set; } = gedcomDate.DateValue;
    public int? Day { get; set; } = gedcomDate.Day;
    public string? DayMonthYear { get; set; } = gedcomDate.DayMonthYear;
    public int? Month { get; set; } = gedcomDate.Month;
    public string? MonthName { get; set; } = gedcomDate.MonthName;
    public string? Time { get; set; } = gedcomDate.TimeValue;
    public int? Year { get; set; } = gedcomDate.Year;

    public int CompareTo(GedcomDateDto? other)
    {
        if (other == null) return 1;

        int yearComparison = Nullable.Compare(Year, other.Year);
        if (yearComparison != 0) return yearComparison;

        int monthComparison = Nullable.Compare(Month, other.Month);
        if (monthComparison != 0) return monthComparison;

        return Nullable.Compare(Day, other.Day);
    }

    public override string ToString() => $"{DayMonthYear ?? DateValue}";
}