using Gedcom.RecordStructures;

namespace Gedcom.DTOs;

// This is the base DTO class for IndividualEventStructure and FamilyEventStructure. They are
// almost always used identically. See either of these classes to view their documentation.
public class EventDto(EventStructure eventStructure) : GedcomDto, IComparable<EventDto>
{
    public AddressDto? Address { get; set; } = GetRecord(new AddressDto(eventStructure.AddressStructure));
    public string? AgeAtEvent { get; set; } = GetString(eventStructure.AgeAtEvent);
    public string? CauseOfEvent { get; set; } = GetString(eventStructure.CauseOfEvent);
    public string? EventOrFactClassification { get; set; } = GetString(eventStructure.EventOrFactClassification);
    public GedcomDateDto Date { get; set; } = new GedcomDateDto(eventStructure.GedcomDate);
    public List<MultimediaLinkDto>? MultimediaLinks { get; set; } = GetList(eventStructure.MultimediaLinks.Select(ml => new MultimediaLinkDto(ml)).ToList());
    public string? Name { get; set; } = GetString(eventStructure.Name);
    public List<string>? Notes { get; set; } = GetList(eventStructure.NoteStructures.Select(ns => ns.Text).ToList());
    public PlaceDto? Place { get; set; } = GetRecord(new PlaceDto(eventStructure.PlaceStructure));
    public string? ReligiousAffiliation { get; set; } = GetString(eventStructure.ReligiousAffiliation);
    public string? ResponsibleAgency { get; set; } = GetString(eventStructure.ResponsibleAgency);
    public string? RestrictionNotice { get; set; } = GetString(eventStructure.RestrictionNotice);
    public List<SourceCitationDto>? SourceCitations { get; set; } = GetList(eventStructure.SourceCitations.Select(sc => new SourceCitationDto(sc)).ToList());

    public int CompareTo(EventDto? other)
    {
        if (other == null) return 1;

        // Compare by year first, then month, then day
        int yearComparison = Nullable.Compare(Date.Year, other.Date.Year);
        if (yearComparison != 0) return yearComparison;

        int monthComparison = Nullable.Compare(Date.Month, other.Date.Month);
        if (monthComparison != 0) return monthComparison;

        return Nullable.Compare(Date.Day, other.Date.Day);
    }

    public override string ToString() => $"{Name} {Date}";
}