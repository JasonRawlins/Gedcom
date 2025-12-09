using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(EventTypeCitedFromJsonConverter))]
public class EventTypeCitedFrom : RecordStructureBase
{
    public EventTypeCitedFrom() : base() { }
    public EventTypeCitedFrom(Record record) : base(record) { }

    public string RoleInEvent => _(Tag.ROLE);

    public override string ToString() => $"{Record.Value}, {RoleInEvent}";
}

internal class EventTypeCitedFromJsonConverter : JsonConverter<EventTypeCitedFrom>
{
    public override EventTypeCitedFrom? ReadJson(JsonReader reader, Type type, EventTypeCitedFrom? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, EventTypeCitedFrom? eventTypeCitedFrom, JsonSerializer serializer)
    {
        if (eventTypeCitedFrom == null) throw new ArgumentNullException(nameof(eventTypeCitedFrom));

        serializer.Serialize(writer, new EventTypeCitedFromJson(eventTypeCitedFrom));
    }
}

internal class EventTypeCitedFromJson : GedcomJson
{
    public EventTypeCitedFromJson(EventTypeCitedFrom eventTypeCitedFrom)
    {
        RoleInEvent = JsonString(eventTypeCitedFrom.RoleInEvent);
    }

    public string? RoleInEvent { get; set; }
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