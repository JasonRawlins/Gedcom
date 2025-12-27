
using Gedcom.RecordStructures;

namespace GedcomTests;

[TestClass]
public class GedcomDateTests
{
    [TestMethod]
    public void ValidDmyTest()
    {
        var gedcomDate = GedcomDate.Parse("1 Jan 2000");
        AssertGedcomDateIsValid(nameof(ValidDmyTest), gedcomDate, "1 Jan 2000", 1, 1, 2000, "Jan", "1 Jan 2000", "");
    }

    [TestMethod]
    public void InvalidYearOnlyTest()
    {
        /*
        Different cases for month names
        Out of range
            Days, months, years

        Parse-able date
        Just year
        Month and year
        Day month year
        Nonsense

        Days < 1 or > 31
        Months < 1 or > 12
        Years < 0 or > 2026

        1 DATE 27 Dec 2025
        2 DATE ABT 1586
        2 DATE 5 MAY 1850 2ND BAPTISM
        2 DATE AFTER 1830 BUT BEFORE 1844
        2 DATE 1883 4TH QUARTER
        2 DATE 1830
        2 DATE NOV 1900
        2 DATE ADMON DATED 23 SEP 1797
        2 DATE AFT 1870
        2 DATE BET 1810 AND 1820
        2 DATE WP 12 OCT 1769
        2 DATE HARMONY CEMETERY, FULTON COUNTY, KENTUCKY
        2 DATE 9 APR 1798 (WP)
        2 DATE 1715 W.D. 24 MAY 1693
        2 DATE DECEASED
        */
    }

    private void AssertGedcomDateIsValid(string testName, GedcomDate gedcomDate, string dateValue, int? day, int? month, int? year, string? monthName, string? dayMonthYear, string? timeValue)
    {
        Assert.AreEqual(dateValue, gedcomDate.DateValue);
        Assert.AreEqual(day, gedcomDate.Day);
        Assert.AreEqual(month, gedcomDate.Month);
        Assert.AreEqual(year, gedcomDate.Year);
        Assert.AreEqual(monthName, gedcomDate.MonthName);
        Assert.AreEqual(dayMonthYear, gedcomDate.DayMonthYear);
        Assert.AreEqual(timeValue, gedcomDate.TimeValue);
    }
}
