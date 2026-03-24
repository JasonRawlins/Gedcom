using Gedcom.DTOs;

namespace Gedcom.GedcomWriters;

public class IndividualListItem(IndividualDto individualDto) : IComparable<IndividualListItem>
{
    public string Birthdate { get; } = individualDto.Birth?.Date.DayMonthYear ?? "Unknown birthdate";
    public string BirthPlace { get; } = individualDto.Birth?.Place?.Name ?? "Unknown birth place";
    public string DeathDate { get; } = individualDto.Death?.Date.DayMonthYear ?? "Unknown death date";
    public string DeathPlace { get; } = individualDto.Death?.Place?.Name ?? "Unknown death place";
    public string FullName { get; } = individualDto.FullName;
    public string Given { get; } = individualDto?.Given ?? "";
    public string Surname { get; } = individualDto?.Surname ?? "";
    public string Xref { get; } = individualDto?.Xref ?? "";
    public string XrefId => Xref.Replace("@", "").Replace("I", "");

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