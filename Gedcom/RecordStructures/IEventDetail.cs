using Gedcom.Entities;

namespace Gedcom.RecordStructures;

public interface IEventDetail
{
    AddressStructure AddressStructure { get; }
    // AgeAtEvent is only found in an IndividualEventStructure in the specification
    // But for ease of coding, it is included in FamilyEventStructures also.
    string AgeAtEvent { get; }
    string CauseOfEvent { get; }
    string EventOrFactClassification { get; }
    GedcomDate GedcomDate { get; }
    List<MultimediaLink> MultimediaLinks { get; }
    string Name { get; }
    List<NoteStructure> NoteStructures { get; }
    PlaceStructure PlaceStructure { get; }
    string ReligiousAffiliation { get; }
    string ResponsibleAgency { get; }
    string RestrictionNotice { get; }
    List<SourceCitation> SourceCitations { get; }
    EventType EventType { get; }
}