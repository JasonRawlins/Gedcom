namespace Gedcom.RecordStructures;

public interface IEventDetail
{
    AddressStructure AddressStructure { get; }
    string CauseOfEvent { get; }
    string EventOrFactClassification { get; }
    GedcomDate GedcomDate { get; }
    List<MultimediaLink> MultimediaLinks { get; }
    List<NoteStructure> NoteStructures { get; }
    PlaceStructure PlaceStructure { get; }
    string ReligiousAffiliation { get; }
    string ResponsibleAgency { get; }
    string RestrictionNotice { get; }
    List<SourceCitation> SourceCitations { get; }
}