using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(FamilyEventDetailJsonConverter))]
public class FamilyEventDetail : RecordStructureBase
{
    public FamilyEventDetail() : base() { }
    public FamilyEventDetail(Record record) : base(record) { }

    public EventDetail EventDetail => First<EventDetail>(C.EVEN);
    public FamilyPartner Husband => First<FamilyPartner>(C.HUSB);
    public FamilyPartner Wife => First<FamilyPartner>(C.WIFE);

    public override string ToString() => $"{Record.Value}, {Husband.Name}, {Wife.Name}";
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
        EventDetail = JsonRecord(familyEventDetail.EventDetail);
        Husband = JsonRecord(familyEventDetail.Husband);
        Wife = JsonRecord(familyEventDetail.Wife);
    }

    public EventDetail? EventDetail { get; set; }
    public FamilyPartner? Husband { get; set; }
    public FamilyPartner? Wife { get; set; }
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