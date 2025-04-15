using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(IndividualRecordJsonConverter))]
public class IndividualRecord : RecordStructureBase
{
    public IndividualRecord() { }
    public IndividualRecord(Record record) : base(record) { }

    public string RestrictionNotice => _(C.RESN);
    public List<PersonalNameStructure> PersonalNameStructures => List<PersonalNameStructure>(C.NAME);
    public string SexValue => _(C.SEX);
    public List<IndividualEventStructure> IndividualEventStructures => List(IsWeaklyTyped).Select(r => new IndividualEventStructure(r)).ToList();
    public List<IndividualAttributeStructure> IndividualAttributeStructures => List<IndividualAttributeStructure>(Record.Tag);
    public List<IndividualEventStructure> IndividualEventStructures1 => List(IsWeaklyTyped).Select(r => new IndividualEventStructure(r)).ToList();
    public List<LdsIndividualOrdinance> LdsIndividualOrdinances => List<LdsIndividualOrdinance>(C.ORDI);
    public List<ChildToFamilyLink> ChildToFamilyLinks => List<ChildToFamilyLink>(C.FAMC);
    public List<SpouseToFamilyLink> SpouseToFamilyLinks => List<SpouseToFamilyLink>(C.FAMS);
    public string Submitter => _(C.SUBN);
    public List<AssociationStructure> AssociationStructures => List<AssociationStructure>(C.ASSO);
    public List<string> Aliases => List(r => r.Tag.Equals(C.ALIA)).Select(r => r.Value).ToList();
    public List<string> AncestorInterests => List(r => r.Tag.Equals(C.ANCI)).Select(r => r.Value).ToList();
    public List<string> DescendantInterests => List(r => r.Tag.Equals(C.DESI)).Select(r => r.Value).ToList();
    public string PermanentRecordFileNumber => _(C.RFN);
    public string AncestralFileNumber => _(C.AFN);
    public List<UserReferenceNumber> UserReferenceNumbers => List<UserReferenceNumber>(C.REFN);
    public string AutomatedRecordId => _(C.RIN);
    public ChangeDate ChangeDate => First<ChangeDate>(C.CHAN);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(C.SOUR);
    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(C.OBJE);

    #region Strongly-typed IndividualEventStructures

    public IndividualEventStructure Birth => First<IndividualEventStructure>(C.BIRT);
    public IndividualEventStructure Death => First<IndividualEventStructure>(C.DEAT);

    #endregion

    private bool IsIndividualEventStructure(Record record)
    {
        return new string[]
        {
            "BIRT", "CHR" , "DEAT", "BURI", "CREM", "ADOP",
            "BAPM", "BARM", "BASM", "BLES", "CHRA", "CONF",
            "FCOM", "ORDN", "NATU", "EMIG", "IMMI", "CENS",
            "PROB", "WILL", "GRAD", "RETI", "EVEN"
        }.Contains(record.Tag);
    }

    private bool IsWeaklyTyped(Record record)
    {
        // By weakly-typed, I mean there isn't a property that specifically
        // exposes the event. For example, there is a property named "Births"
        // that exposes only the BIRT IndividualEventStructure.
        return !(new string[]
        {
            "BIRT", "DEAT"
        }.Contains(record.Tag));
    }

    public override string ToString() => $"{PersonalNameStructures.First().NamePersonal} {SexValue} ({Record.Value})";
}

internal class IndividualRecordJsonConverter : JsonConverter<IndividualRecord>
{
    public override IndividualRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, IndividualRecord individualRecord, JsonSerializerOptions options)
    {
        var individualRecordJson = new IndividualRecordJson(individualRecord);
        JsonSerializer.Serialize(writer, individualRecordJson, individualRecordJson.GetType(), options);
    }
}

internal class IndividualRecordJson : GedcomJson
{
    public IndividualRecordJson(IndividualRecord individualRecord)
    {
        Xref = individualRecord.Xref;
        RestrictionNotice = JsonString(individualRecord.RestrictionNotice);

        PersonalNameStructures = JsonList(individualRecord.PersonalNameStructures);
        SexValue = JsonString(individualRecord.SexValue);
        IndividualEventStructures = JsonList(individualRecord.IndividualEventStructures);
        IndividualAttributeStructures = JsonList(individualRecord.IndividualAttributeStructures);
        LdsIndividualOrdinances = JsonList(individualRecord.LdsIndividualOrdinances);
        ChildToFamilyLinks = JsonList(individualRecord.ChildToFamilyLinks);
        SpouseToFamilyLinks = JsonList(individualRecord.SpouseToFamilyLinks);
        Submitter = JsonString(individualRecord.Submitter);
        AssociationStructures = JsonList(individualRecord.AssociationStructures);
        Aliases = JsonList(individualRecord.Aliases);
        AncestorInterests = JsonList(individualRecord.AncestorInterests);
        DescendantInterests = JsonList(individualRecord.DescendantInterests);
        PermanentRecordFileNumber = JsonString(individualRecord.PermanentRecordFileNumber);
        AncestralFileNumber = JsonString(individualRecord.AncestralFileNumber);
        UserReferenceNumbers = JsonList(individualRecord.UserReferenceNumbers);
        AutomatedRecordId = JsonString(individualRecord.AutomatedRecordId);
        ChangeDate = JsonRecord(individualRecord.ChangeDate);
        NoteStructures = JsonList(individualRecord.NoteStructures);
        SourceCitations = JsonList(individualRecord.SourceCitations);
        MultimediaLinks = JsonList(individualRecord.MultimediaLinks);

        // Strongly-typed properties
        Birth = JsonString($"{individualRecord.Birth.DateValue} at {individualRecord.Birth.PlaceStructure.PlaceName}");
        Death = JsonString($"{individualRecord.Death.DateValue} at {individualRecord.Death.PlaceStructure.PlaceName}");
    }

    public string? RestrictionNotice { get; set; }
    public List<PersonalNameStructure>? PersonalNameStructures { get; set; }
    public string? SexValue { get; set; }
    public List<IndividualEventStructure>? IndividualEventStructures { get; set; }
    public List<IndividualAttributeStructure>? IndividualAttributeStructures { get; set; }
    public List<LdsIndividualOrdinance>? LdsIndividualOrdinances { get; set; }
    public List<ChildToFamilyLink>? ChildToFamilyLinks { get; set; }
    public List<SpouseToFamilyLink>? SpouseToFamilyLinks { get; set; }
    public string? Submitter { get; set; }
    public List<AssociationStructure>? AssociationStructures { get; set; }
    public List<string>? Aliases { get; set; }
    public List<string>? AncestorInterests { get; set; }
    public List<string>? DescendantInterests { get; set; }
    public string? PermanentRecordFileNumber { get; set; }
    public string? AncestralFileNumber { get; set; }
    public List<UserReferenceNumber>? UserReferenceNumbers { get; set; }
    public string? AutomatedRecordId { get; set; }
    public ChangeDate? ChangeDate { get; set; }
    public List<NoteStructure>? NoteStructures { get; set; }
    public List<SourceCitation>? SourceCitations { get; set; }
    public List<MultimediaLink>? MultimediaLinks { get; set; }

    public string? Birth { get; set; }
    public string? Death { get; set; }
    public string Given => PersonalNameStructures == null ? "" : PersonalNameStructures[0].Given;
    public string Surname => PersonalNameStructures == null ? "" : PersonalNameStructures[0].Surname;
}

#region INDIVIDUAL_RECORD (INDI) p. 25
/* 

INDIVIDUAL_RECORD:=

n @XREF:INDI@ INDI {1:1}
    +1 RESN <RESTRICTION_NOTICE> {0:1} p.60
    +1 <<PERSONAL_NAME_STRUCTURE>> {0:M} p.38
    +1 SEX <SEX_VALUE> {0:1} p.61
    +1 <<INDIVIDUAL_EVENT_STRUCTURE>> {0:M} p.34
    +1 <<INDIVIDUAL_ATTRIBUTE_STRUCTURE>> {0:M} p.33
    +1 <<LDS_INDIVIDUAL_ORDINANCE>> {0:M} p.35, 36
    +1 <<CHILD_TO_FAMILY_LINK>> {0:M} p.31
    +1 <<SPOUSE_TO_FAMILY_LINK>> {0:M} p.40
    +1 SUBM @<XREF:SUBM>@ {0:M} p.28
    +1 <<ASSOCIATION_STRUCTURE>> {0:M} p.31
    +1 ALIA @<XREF:INDI>@ {0:M} p.25
    +1 ANCI @<XREF:SUBM>@ {0:M} p.28
    +1 DESI @<XREF:SUBM>@ {0:M} p.28
    +1 RFN <PERMANENT_RECORD_FILE_NUMBER> {0:1} p.57
    +1 AFN <ANCESTRAL_FILE_NUMBER> {0:1} p.42
    +1 REFN <USER_REFERENCE_NUMBER> {0:M} p.63, 64
        +2 TYPE <USER_REFERENCE_TYPE> {0:1} p.64
    +1 RIN <AUTOMATED_RECORD_ID> {0:1} p.43
    +1 <<CHANGE_DATE>> {0:1} p.31
    +1 <<NOTE_STRUCTURE>> {0:M} p.37
    +1 <<SOURCE_CITATION>> {0:M} p.39
    +1 <<MULTIMEDIA_LINK>> {0:M} p.37, 26

The individual record is a compilation of facts, known or discovered, about an individual. Sometimes
these facts are from different sources. This form allows documentation of the source where each of 
the facts were discovered.

The normal lineage links are shown through the use of pointers from the individual to a family
through either the FAMC tag or the FAMS tag. The FAMC tag provides a pointer to a family where
this person is a child. The FAMS tag provides a pointer to a family where this person is a spouse or
parent. The <<CHILD_TO_FAMILY_LINK>> (see page 31) structure contains a FAMC pointer
which is required to show any child to parent linkage for pedigree navigation. The
<<CHILD_TO_FAMILY_LINK>> structure also indicates whether the pedigree link represents a
birth lineage, an adoption lineage, or a sealing lineage.

Linkage between a child and the family they belonged to at the time of an event can also be shown
by a FAMC pointer subordinate to the appropriate event. For example, a FAMC pointer subordinate
to an adoption event indicates a relationship to family by adoption. Biological parents can be shown
by a FAMC pointer subordinate to the birth event(optional).

Other associations or relationships are represented by the ASSOciation tag. The person's relation
or association is the person being pointed to. The association or relationship is stated by the value
on the subordinate RELA line. For example:

0 @I1@ INDI
    1 NAME Fred /Jones/
    1 ASSO @I2@
        2 RELA Godfather
*/
#endregion