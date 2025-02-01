using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.Tags;

[JsonConverter(typeof(INDIJsonConverter))]
public class INDI : TagBase
{
    public INDI(Record record) : base(record) { }

    private Record? BIRTRecord => FirstOrDefault(C.BIRT);
    public string Birthdate => RecordValue(BIRTRecord, C.DATE);
    public string Birthplace => RecordValue(BIRTRecord, C.PLAC);
    private Record? DEATRecord => FirstOrDefault(C.DEAT);
    public string Deathdate => RecordValue(DEATRecord, C.DATE);
    public string Deathplace => RecordValue(DEATRecord, C.PLAC);
    public string ExtIndi => Record.Value;
    public List<Record> FAMSs => List(C.FAMS);
    public NAME? NAME
    {
        get
        {
            var nameRecord = FirstOrDefault(C.NAME);
            if (nameRecord != null)
            {
                return new NAME(nameRecord);
            }

            return null;
        }
    }
    public string RESN => V(C.RESN);
    public string RIN => V(C.RIN);
    public string SEX => V(C.SEX);
    // SUBM
    public string XRef => Record.Value;

    // ASSOCIATION_STRUCTURE
    // INDIVIDUAL_EVENT_STRUCTURE
    // INDIVIDUAL_ATTRIBUTE_STRUCTURE
    // LDS_INDIVIDUAL_ORDINANCE
    // CHILD_TO_FAMILY_LINK
    // SPOUSE_TO_FAMILY_LINK
    public string SUBM => V(C.SUBM);
    public string ALIA => V(C.ALIA);
    public string ANCI => V(C.ANCI);
    public string DESI => V(C.DESI);
    public string RFN => V(C.RFN);
    public string AFN => V(C.AFN);
    public REFN? REFN
    {
        get
        {
            var refnRecord = FirstOrDefault(C.REFN);
            if (refnRecord != null)
            {
                return new REFN(refnRecord);
            }

            return null;
        }
    }
    public CHAN? CHAN
    {
        get
        {
            var chanRecord = FirstOrDefault(C.CHAN);
            if (chanRecord != null)
            {
                return new CHAN(chanRecord);
            }

            return null;
        }
    }

    public NOTE_STRUCTURE? NOTE
    {
        get
        {
            var noteRecord = FirstOrDefault(C.NOTE);
            if (noteRecord != null)
            {
                return new NOTE_STRUCTURE(noteRecord);
            }

            return null;
        }
    }

    // SOURCE_CITATION
    public SOUR? SOUR => GetSubrecord<SOUR>(this, C.SOUR);

    public OBJE? OBJE
    {
        get
        {
            var objeRecord = FirstOrDefault(C.OBJE);
            if (objeRecord != null)
            {
                return new OBJE(objeRecord);
            }

            return null;
        }
    }

    public override string ToString()
    {
        var birthdateText = Birthdate;
        if (DateTime.TryParse(Birthdate, out var birthdate))
        {
            birthdateText = birthdate.Year.ToString();
        }

        var deathdateText = Deathdate;
        if (DateTime.TryParse(Deathdate, out var deathdate))
        {
            deathdateText = deathdate.Year.ToString();
        }

        return $"{NAME} ({birthdateText} - {deathdateText}) {SEX} ({ExtIndi})";
    }
}

public class INDIJsonConverter : JsonConverter<INDI>
{
    public override INDI? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
    public override void Write(Utf8JsonWriter writer, INDI indi, JsonSerializerOptions options)
    {
        var jsonObject = new
        {
            Id = indi.ExtIndi,
            Name = indi.NAME,
            Given = indi.NAME?.GIVN ?? "",
            Surname = indi.NAME?.SURN ?? "",
            indi.Birthdate,
            indi.Deathdate,
            Rin = indi.RIN,
            Sex = indi.SEX,
        };

        JsonSerializer.Serialize(writer, jsonObject, options);
    }
}

#region INDIVIDUAL_RECORD (INDI) p. 25
/* 
https://gedcom.io/specifications/ged551.pdf

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
    1 NAME Fred/Jones/
    1 ASSO @I2@
        2 RELA Godfather
*/
#endregion