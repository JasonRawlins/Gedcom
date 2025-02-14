namespace Gedcom.RecordStructures;

public class EventTypeCitedFrom : RecordStructureBase
{
    public EventTypeCitedFrom() : base() { }
    public EventTypeCitedFrom(Record record) : base(record) { }

    public string RoleInEvent => _(C.ROLE);
}

#region EVENT_TYPE_CITED_FROM p. 49
/* 

EVENT_TYPE_CITED_FROM:= {SIZE=1:15}

[<EVENT_ATTRIBUTE_TYPE>]

A code that indicates the type of event which was responsible for the source entry being recorded. For
example, if the entry was created to record a birth of a child, then the type would be BIRT regardless
of the assertions made from that record, such as the mother's name or mother's birth date. This will
allow a prioritized best view choice

*/
#endregion