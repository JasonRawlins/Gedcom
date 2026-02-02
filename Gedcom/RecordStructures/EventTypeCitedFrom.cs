using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(EventTypeCitedFromJsonConverter))]
public class EventTypeCitedFrom : RecordStructureBase
{
    public EventTypeCitedFrom() : base() { }
    public EventTypeCitedFrom(Record record) : base(record) { }

    private string? _roleInEvent = null;
    public string RoleInEvent => _roleInEvent ??= GetValue(Tag.Role);

    public override string ToString() => $"{Record.Value}, {RoleInEvent}";
}

internal sealed class EventTypeCitedFromJsonConverter : JsonConverter<EventTypeCitedFrom>
{
    public override EventTypeCitedFrom? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, EventTypeCitedFrom value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new EventTypeCitedFromJson(value), options);
    }
}

public class EventTypeCitedFromJson(EventTypeCitedFrom eventTypeCitedFrom) : GedcomJson
{
    public string? RoleInEvent { get; set; } = JsonString(eventTypeCitedFrom.RoleInEvent);
    public override string ToString() => $"{RoleInEvent}";
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