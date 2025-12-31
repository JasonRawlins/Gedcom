using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class EventModel
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

    public override string ToString() => Name;
}