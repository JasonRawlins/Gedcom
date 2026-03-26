using Gedcom.DTOs;

namespace Gedcom.GedcomWriters;

// TODO: 2026-03-24. Is this being used for anything?
public static class FieldManager
{
    public static string GetIndividualField(IndividualDto individualDto, string field)
    {
        return field.ToLower() switch
        {
            Individual.Birthdate => individualDto?.Birth?.Date.DayMonthYear ?? "Unknown",
            Individual.BirthPlace => individualDto?.Birth?.Place?.Name ?? "Unknown",
            Individual.DeathDate => individualDto?.Death?.Place?.Name ?? "Unknown",
            Individual.DeathPlace => individualDto?.Death?.Place?.Name ?? "Unknown",
            Individual.Given => individualDto?.Given ?? "Unknown",
            _ => throw new NotSupportedException($"Unknown field: {field}"),
        };
    }

    public static class Individual
    {
        public const string Birthdate = "{{BIRTHDATE}}";
        public const string BirthPlace = "{{BIRTH_PLACE}}";
        public const string DeathDate = "{{DEATH_DATE}}";
        public const string DeathPlace = "{{DEATH_PLACE}}";
        public const string Given = "{{GIVEN}}";
        public const string Surname = "{{SURNAME}}";
    }
}