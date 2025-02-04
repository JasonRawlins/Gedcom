namespace Gedcom.RecordStructure;

//[JsonConverter(typeof(FamilyRecordJsonConverter))]
public class FamilyRecord : RecordStructureBase
{
    public FamilyRecord(Record record) : base(record) { }

    public List<Record> Partners => List(r => r.Tag.Equals(C.WIFE) || r.Tag.Equals(C.HUSB));
    public override string ToString() => $"({string.Join(',', Partners)})";
}

//public class FamilyRecordJsonConverter : JsonConverter<FamilyRecord>
//{
//    public override FamilyRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

//    public override void Write(Utf8JsonWriter writer, FamilyRecord fam, JsonSerializerOptions options)
//    {
//        var jsonObject = new
//        {
//            fam.Partners
//        };

//        JsonSerializer.Serialize(writer, jsonObject, options);
//    }
//}

#region FAM_RECORD (FAM) p. 24
/*
https://gedcom.io/specifications/ged551.pdf

FAM_RECORD:=

n @<XREF:FAM>@ FAM {1:1}
    +1 RESN <RESTRICTION_NOTICE> {0:1) p.60
    +1 <<FAMILY_EVENT_STRUCTURE>> {0:M} p.32
    +1 HUSB @<XREF:INDI>@ {0:1} p.25
    +1 WIFE @<XREF:INDI>@ {0:1} p.25
    +1 CHIL @<XREF:INDI>@ {0:M} p.25
    +1 NCHI <COUNT_OF_CHILDREN> {0:1} p.44
    +1 SUBM @<XREF:SUBM>@ {0:M} p.28
    +1 <<LDS_SPOUSE_SEALING>> {0:M} p.36
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<CHANGE_DATE>> {0:1} p.31
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<SOURCE_CITATION>> {0:M} p.39
    +1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26

The FAMily record is used to record marriages, common law marriages, and family unions caused by
two people becoming the parents of a child. There can be no more than one HUSB/father and one
WIFE/mother listed in each FAM_RECORD. If, for example, a man participated in more than one
family union, then he would appear in more than one FAM_RECORD. The family record structure
assumes that the HUSB/father is male and WIFE/mother is female.

The preferred order of the CHILdren pointers within a FAMily structure is chronological by birth.
*/
#endregion