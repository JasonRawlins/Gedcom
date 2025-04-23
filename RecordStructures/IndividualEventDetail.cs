using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(IndividualEventDetailJsonConverter))]
public class IndividualEventDetail : RecordStructureBase
{
    public IndividualEventDetail() : base() { }
    public IndividualEventDetail(Record record) : base(record) { }

    public string AgeAtEvent => _(C.AGE);

    public override string ToString() => $"{Record.Value}, {AgeAtEvent}";
}

internal class IndividualEventDetailJsonConverter : JsonConverter<IndividualEventDetail>
{
    public override IndividualEventDetail? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, IndividualEventDetail individualEventDetail, JsonSerializerOptions options)
    {
        var individualEventDetailJson = new IndividualEventDetailJson(individualEventDetail);
        JsonSerializer.Serialize(writer, individualEventDetailJson, individualEventDetailJson.GetType(), options);
    }
}

internal class IndividualEventDetailJson : GedcomJson
{
    public IndividualEventDetailJson(IndividualEventDetail individualEventDetail)
    {
        AgeAtEvent = JsonString(individualEventDetail.AgeAtEvent);
    }

    public string? AgeAtEvent { get; set; }

}

#region INDIVIDUAL_EVENT_DETAIL p. 34
/* 

INDIVIDUAL_EVENT_DETAIL:=

n <<EVENT_DETAIL>> {1:1} p.32
n AGE <AGE_AT_EVENT> {0:1} p.42

*/
#endregion