using Gedcom.DTOs;

namespace Gedcom.GedcomWriters;

public class FamilyListItem(FamilyDto familyDto) : IComparable<FamilyDto>
{
    public string Husband { get; } = familyDto.Husband ?? "Unknown husband";
    public string Wife { get; } = familyDto.Wife ?? "Unknown wife";
    public string Xref { get; } = familyDto.Xref;
    public string XrefId => Xref.Replace("@", "").Replace("I", "");

    public int CompareTo(FamilyDto? other)
    {
        return 0;
        //if (other == null) return 1;

        //// Sort by BirthDate first
        //int birthComparison = Birthdate.CompareTo(other.Birthdate);
        //if (birthComparison != 0) return birthComparison;

        //// Then by Surname
        //int surnameComparison = string.Compare(Surname, other.Surname, StringComparison.OrdinalIgnoreCase);
        //if (surnameComparison != 0) return surnameComparison;

        //// Finally by GivenName
        //return string.Compare(Given, other.Given, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return $"Family {Xref}. Husband {Husband}. Wife {Wife}"; // $"{Surname}, {Given} ({Birthdate} — {DeathDate}). {Xref}";
    }
}