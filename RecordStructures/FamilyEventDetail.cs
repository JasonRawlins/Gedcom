using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(FamilyEventDetail))]
public class FamilyEventDetail : RecordStructureBase
{
    public FamilyEventDetail() : base() { }
    public FamilyEventDetail(Record record) : base(record) { }

    public FamilyPartner Husband => First<FamilyPartner>(C.HUSB);
    public FamilyPartner Wife => First<FamilyPartner>(C.WIFE);
    public EventDetail EventDetail => First<EventDetail>(C.EVEN);
}

internal class FamilyEventDetailJsonConverter : JsonConverter<FamilyEventDetail>
{
    public override FamilyEventDetail? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, FamilyEventDetail familyEventDetail, JsonSerializerOptions options)
    {
        var familyEventDetailJson = new FamilyEventDetailJson(familyEventDetail);
        JsonSerializer.Serialize(writer, familyEventDetailJson, familyEventDetailJson.GetType(), options);
    }
}

internal class FamilyEventDetailJson : GedcomJson
{
    public FamilyEventDetailJson(FamilyEventDetail familyEventDetail)
    {
        Husband = JsonRecord(familyEventDetail.Husband);
        Wife = JsonRecord(familyEventDetail.Wife);
        EventDetail = JsonRecord(familyEventDetail.EventDetail);
    }

    public FamilyPartner? Husband { get; set; }
    public FamilyPartner? Wife { get; set; }
    public EventDetail? EventDetail { get; set; }
}

#region FAMILY_EVENT_DETAIL p. 32
/* 

FAMILY_EVENT_DETAIL:=

n HUSB {0:1}
    +1 AGE <AGE_AT_EVENT> {1:1} p.42
n WIFE {0:1}
    +1 AGE <AGE_AT_EVENT> {1:1} p.42
n <<EVENT_DETAIL>> {0:1} p.32

*/
#endregion