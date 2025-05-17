using Gedcom.RecordStructures;

public interface IEventDetail
{
    string EventOrFactClassification { get; }
    GedcomDate GedcomDate { get; }
    PlaceStructure PlaceStructure { get; }
    AddressStructure AddressStructure { get; }
    string ResponsibleAgency { get; }
    string ReligiousAffiliation { get; }
    string CauseOfEvent { get; }
    string RestrictionNotice { get; }
    List<NoteStructure> NoteStructures { get; }
    List<SourceCitation> SourceCitations { get; }
    List<MultimediaLink> MultimediaLinks { get; }
}