using Gedcom.RecordStructures;

namespace Gedcom.GedcomWriters;

public class IndividualListItem(IndividualRecord individualRecord) : IComparable<IndividualListItem>
{
    public GedcomDate Birth { get; } = individualRecord.Birth.GedcomDate;
    public string BirthPlace { get; } = individualRecord.Birth.PlaceStructure.PlaceName;
    public GedcomDate Death { get; } = individualRecord.Death.GedcomDate;
    public string DeathPlace { get; } = individualRecord.Death.PlaceStructure.PlaceName;
    public string FullName { get; } = individualRecord.FullName;
    public string Given { get; } = individualRecord.Given;
    public string Surname { get; } = individualRecord.Surname;
    public string Xref { get; } = individualRecord.Xref;
    public string XrefId => Xref.Replace("@", "").Replace("I", "");

    public int CompareTo(IndividualListItem? other)
    {
        if (other == null) return 1;

        // Sort by BirthDate first
        int birthComparison = Birth.CompareTo(other.Birth);
        if (birthComparison != 0) return birthComparison;

        // Then by Surname
        int surnameComparison = string.Compare(Surname, other.Surname, StringComparison.OrdinalIgnoreCase);
        if (surnameComparison != 0) return surnameComparison;

        // Finally by GivenName
        return string.Compare(Given, other.Given, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return $"{Surname}, {Given} ({Birth.DayMonthYear} — {Death.DayMonthYear}). {Xref}";
    }
}