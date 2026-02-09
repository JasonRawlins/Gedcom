using Gedcom.RecordStructures;

namespace Gedcom.GedcomWriters;

public class IndividualListItem : IComparable<IndividualListItem>
{
    public string Birthdate { get; }
    public string BirthPlace { get; }
    public string DeathDate { get; }
    public string DeathPlace { get; }
    public string FullName { get; }
    public string Given { get; }
    public string Surname { get; }
    public string Xref { get; }
    public string XrefId => Xref.Replace("@", "").Replace("I", "");

    public IndividualListItem(IndividualRecord individualRecord)
    {
        var individualDto = new IndividualDto(individualRecord);

        Birthdate = individualDto.Birth?.Date.DayMonthYear ?? "Unknown birthdate";
        BirthPlace = individualDto.Birth?.Place?.Name ?? "Unknown birth place";
        DeathDate = individualDto.Death?.Date.DayMonthYear ?? "Unknown death date";
        DeathPlace = individualDto.Death?.Place?.Name ?? "Unknown death place";
        FullName = individualDto.FullName;
        Given = individualDto?.Given ?? "";
        Surname = individualDto?.Surname ?? "";
        Xref = individualDto?.Xref ?? "";
    }

    public int CompareTo(IndividualListItem? other)
    {
        if (other == null) return 1;

        // Sort by BirthDate first
        int birthComparison = Birthdate.CompareTo(other.Birthdate);
        if (birthComparison != 0) return birthComparison;

        // Then by Surname
        int surnameComparison = string.Compare(Surname, other.Surname, StringComparison.OrdinalIgnoreCase);
        if (surnameComparison != 0) return surnameComparison;

        // Finally by GivenName
        return string.Compare(Given, other.Given, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return $"{Surname}, {Given} ({Birthdate} — {DeathDate}). {Xref}";
    }
}