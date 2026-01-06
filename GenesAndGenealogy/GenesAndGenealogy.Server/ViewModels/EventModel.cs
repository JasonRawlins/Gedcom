using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class EventModel : IComparable<EventModel>
{
    public EventModel(IndividualEventStructure individualEventStructure)
    {
        // AddressStructure
        AgeAtEvent = individualEventStructure.AgeAtEvent;
        CauseOfEvent = individualEventStructure.CauseOfEvent;
        Date = new DateModel(GedcomDate.Parse(individualEventStructure.DateValue));
        EventOrFactClassification = individualEventStructure.EventOrFactClassification;
        // MultimediaLinks
        Name = individualEventStructure.Name;
        Notes = individualEventStructure.NoteStructures.Select(ns => ns.Text).ToList();
        Place = new PlaceModel(individualEventStructure.PlaceStructure);
        ReligiousAffiliation = individualEventStructure.ReligiousAffiliation;
        ResponsibleAgency = individualEventStructure.ResponsibleAgency;
        RestrictionNotice = individualEventStructure.RestrictionNotice;
        SourceCitations = individualEventStructure.SourceCitations.Select(sc => new SourceCitationModel(sc)).ToList();
        Type = EventModelType.Individual;
    }

    public EventModel(FamilyEventStructure familyEventStructure)
    {
        // AddressStructure
        AgeAtEvent = familyEventStructure.AgeAtEvent;
        CauseOfEvent = familyEventStructure.CauseOfEvent;
        Date = new DateModel(GedcomDate.Parse(familyEventStructure.DateValue));
        EventOrFactClassification = familyEventStructure.EventOrFactClassification;
        // MultimediaLinks
        Name = familyEventStructure.Name;
        Notes = familyEventStructure.NoteStructures.Select(ns => ns.Text).ToList();
        Place = new PlaceModel(familyEventStructure.PlaceStructure);
        ReligiousAffiliation = familyEventStructure.ReligiousAffiliation;
        ResponsibleAgency = familyEventStructure.ResponsibleAgency;
        RestrictionNotice = familyEventStructure.RestrictionNotice;
        SourceCitations = familyEventStructure.SourceCitations.Select(sc => new SourceCitationModel(sc)).ToList();
        Type = EventModelType.Family;
    }

    //public AddressStructure AddressStructure { get; set; }
    public string AgeAtEvent { get; set; }
    public string CauseOfEvent { get; set; }
    public string EventOrFactClassification { get; set; }
    public DateModel Date { get; set; }
    //public List<MultimediaLink> MultimediaLinks { get; set; }
    public string Name { get; set; }
    public List<string> Notes { get; set; }
    public PlaceModel Place { get; set; }
    public string ReligiousAffiliation { get; set; }
    public string ResponsibleAgency { get; set; }
    public string RestrictionNotice { get; set; }
    public List<SourceCitationModel> SourceCitations { get; set; }
    public EventModelType Type { get; set; }

    // Sorts by year, then month, then day.
    public int CompareTo(EventModel? other)
    {
        if (other == null) return 1;

        // Compare by year first, then month, then day
        int yearComparison = Date.Year.CompareTo(other.Date.Year);
        if (yearComparison != 0) return yearComparison;

        int monthComparison = Date.Month.CompareTo(other.Date.Month);
        if (monthComparison != 0) return monthComparison;

        return Date.Day.CompareTo(other.Date.Day);
    }

    public override string ToString() => Name;
}

public enum EventModelType
{
    Family,
    Individual
}