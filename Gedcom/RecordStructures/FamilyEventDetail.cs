using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.1.1 documentation is at the end of this file.
[JsonConverter(typeof(FamilyEventDetailJsonConverter))]
public class FamilyEventDetail : RecordStructureBase
{
    public FamilyEventDetail() : base() { }
    public FamilyEventDetail(Record record) : base(record) { }

    public EventDetail EventDetail => First<EventDetail>(Tag.EVEN);

    /*
    The FAM record was originally structured to represent families where a male HUSB (p.75) (husband or
    father) and female WIFE (p.91) (wife or mother) produce CHIL (p.68) (children). The FAM record may
    also be used for cultural parallels to this, including nuclear families, marriage, cohabitation, fostering,
    adoption, and so on, regardless of the gender of the partners. 
    
    I named these properties Husband and Wife because that is what the Gedcom Standard 5.1.1 calls them. 
    In Gedcom 7, Sex, gender, titles, and roles of partners should not be inferred based on the partner
    that the HUSB or WIFE structure points to. The individuals pointed to by the HUSB and WIFE are
    collectively referred to as "partners", "parents" or "spouses".
    */
    public FamilyPartner Husband => First<FamilyPartner>(Tag.HUSB);
    public FamilyPartner Wife => First<FamilyPartner>(Tag.WIFE);

    public override string ToString() => $"{Record.Value}, {Husband.Name}, {Wife.Name}";
}

internal class FamilyEventDetailJsonConverter : JsonConverter<FamilyEventDetail>
{
    public override FamilyEventDetail? ReadJson(JsonReader reader, Type objectType, FamilyEventDetail? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, FamilyEventDetail? familyEventDetail, JsonSerializer serializer)
    {
        if (familyEventDetail == null) throw new ArgumentNullException(nameof(familyEventDetail));

        serializer.Serialize(writer, new FamilyEventDetailJson(familyEventDetail));
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