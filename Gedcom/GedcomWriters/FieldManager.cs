using Gedcom.RecordStructures;

namespace Gedcom.GedcomWriters;

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
        public const string Birthdate = "{{birthdate}}";
        public const string BirthPlace = "{{birth-place}}";
        public const string DeathDate = "{{death-date}}";
        public const string DeathPlace = "{{death-place}}";
        public const string Given = "{{given}}";
        public const string Surname = "{{surname}}";
    }
}