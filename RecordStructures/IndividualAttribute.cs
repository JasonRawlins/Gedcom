using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(IndividualAttributeJsonConverter))]
public class IndividualAttribute : RecordStructureBase
{
    public IndividualAttribute() : base() { }
    public IndividualAttribute(Record record) : base(record) { }

    public IndividualEventDetail IndividualEventDetail => First<IndividualEventDetail>(C.EVEN);

    public override string ToString() => $"{Record.Value}";
}

internal class IndividualAttributeJsonConverter : JsonConverter<IndividualAttribute>
{
    public override IndividualAttribute? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, IndividualAttribute individualAttribute, JsonSerializerOptions options)
    {
        var individualAttributeJson = new IndividualAttributeJson(individualAttribute);
        JsonSerializer.Serialize(writer, individualAttributeJson, individualAttributeJson.GetType(), options);
    }
}

internal class IndividualAttributeJson : GedcomJson
{
    public IndividualAttributeJson(IndividualAttribute individualAttribute)
    {
        IndividualEventDetail = JsonRecord(individualAttribute.IndividualEventDetail);
    }

    public IndividualEventDetail? IndividualEventDetail { get; set; }
}

#region INDIVIDUAL_ATTRIBUTE p. 33
/* 

n TAG <Value_Description> {1:1} p.58
    +1 [CONC | CONT ] <PHYSICAL_DESCRIPTION> {0:M} p.58
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34

*/
#endregion