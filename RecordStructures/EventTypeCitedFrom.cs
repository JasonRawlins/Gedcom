using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(EventTypeCitedFromJsonConverter))]
public class EventTypeCitedFrom : RecordStructureBase
{
    public EventTypeCitedFrom() : base() { }
    public EventTypeCitedFrom(Record record) : base(record) { }

    public string RoleInEvent => _(C.ROLE);
}

internal class EventTypeCitedFromJsonConverter : JsonConverter<EventTypeCitedFrom>
{
    public override EventTypeCitedFrom? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, EventTypeCitedFrom eventTypeCitedFrom, JsonSerializerOptions options)
    {
        var eventTypeCitedFromJson = new EventTypeCitedFromJson(eventTypeCitedFrom);
        JsonSerializer.Serialize(writer, eventTypeCitedFromJson, eventTypeCitedFromJson.GetType(), options);
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