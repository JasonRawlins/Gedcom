using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(IndividualAttributeJsonConverter))]
public class IndividualAttribute : RecordStructureBase
{
    public IndividualAttribute() : base() { }
    public IndividualAttribute(Record record) : base(record) { }

    public IndividualEventDetail IndividualEventDetail => First<IndividualEventDetail>(Tag.EVEN);

    public override string ToString() => $"{Record.Value}";
}

internal class IndividualAttributeJsonConverter : JsonConverter<IndividualAttribute>
{
    public override IndividualAttribute? ReadJson(JsonReader reader, Type objectType, IndividualAttribute? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, IndividualAttribute? individualAttribute, JsonSerializer serializer)
    {
        if (individualAttribute == null) throw new ArgumentNullException(nameof(individualAttribute));

        serializer.Serialize(writer, new IndividualAttributeJson(individualAttribute));
    }
}

internal class IndividualAttributeJson : GedcomJson
{
    public IndividualAttributeJson(IndividualAttribute individualAttribute)
    {
        EventDetail = JsonRecord(individualAttribute.IndividualEventDetail);
    }

    public IndividualEventDetail? EventDetail { get; set; }
}

#region INDIVIDUAL_ATTRIBUTE p. 33
/* 

n TAG <Value_Description> {1:1} p.58
    +1 [CONC | CONT ] <PHYSICAL_DESCRIPTION> {0:M} p.58
    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34

*/
#endregion