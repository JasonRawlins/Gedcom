using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(IndividualEventDetailJsonConverter))]
public class IndividualEventDetail : RecordStructureBase
{
    public IndividualEventDetail() : base() { }
    public IndividualEventDetail(Record record) : base(record) { }

    public string AgeAtEvent => _(Tag.AGE);

    public override string ToString() => $"{Record.Value}, {AgeAtEvent}";
}

internal class IndividualEventDetailJsonConverter : JsonConverter<IndividualEventDetail>
{
    public override IndividualEventDetail? ReadJson(JsonReader reader, Type objectType, IndividualEventDetail? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, IndividualEventDetail? individualEventDetail, JsonSerializer serializer)
    {
        if (individualEventDetail == null) throw new ArgumentNullException(nameof(individualEventDetail));

        serializer.Serialize(writer, new IndividualEventDetailJson(individualEventDetail));
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