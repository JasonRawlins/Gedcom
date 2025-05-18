using Gedcom.RecordStructures;

namespace Gedcom.CLI;

public class IndividualListItem : IComparable<IndividualListItem>
{
    public IndividualListItem(IndividualRecord individualRecord)
    {
        Birth = individualRecord.Birth.GedcomDate;
        BirthPlace = individualRecord.Birth.PlaceStructure.PlaceName;
        Death = individualRecord.Death.GedcomDate;
        DeathPlace = individualRecord.Death.PlaceStructure.PlaceName;
        FullName = individualRecord.FullName;
        Given = individualRecord.Given;
        Surname = individualRecord.Surname;
        Xref = individualRecord.Xref;
    }

    public GedcomDate Birth { get; set; }
    public string BirthPlace { get; set; }
    public GedcomDate Death { get; set; }
    public string DeathPlace { get; set; }
    public string FullName { get; set; }
    public string Given { get; set; }
    public string Surname { get; set; }
    public string Xref { get; set; }
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