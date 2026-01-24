using Gedcom.RecordStructures;

namespace Gedcom.Entities;

public class Event(EventStructure eventStructure)
{
    private EventStructure EventStructure { get; } = eventStructure;
}
