using Gedcom.Entities;
using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class EventModel : IComparable<EventModel>
{
    public EventModel(IEventDetail eventDetail)
    {
        // AddressStructure
        AgeAtEvent = eventDetail.AgeAtEvent;
        CauseOfEvent = eventDetail.CauseOfEvent;
        Date = new DateModel(eventDetail.GedcomDate);
        EventOrFactClassification = eventDetail.EventOrFactClassification;
        // MultimediaLinks
        Name = eventDetail.Name;
        Notes = eventDetail.NoteStructures.Select(ns => ns.Text).ToList();
        Place = new PlaceModel(eventDetail.PlaceStructure);
        ReligiousAffiliation = eventDetail.ReligiousAffiliation;
        ResponsibleAgency = eventDetail.ResponsibleAgency;
        RestrictionNotice = eventDetail.RestrictionNotice;
        SourceCitations = eventDetail.SourceCitations.Select(sc => new SourceCitationModel(sc)).ToList();
        Type = eventDetail.EventType;
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
    public EventType Type { get; set; }

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