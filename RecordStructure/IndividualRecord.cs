using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructure;

// INDI
[JsonConverter(typeof(IndividualRecordJsonConverter))]
public class IndividualRecord : RecordStructureBase
{
    public IndividualRecord(Record record) : base(record) { }

    public string RestrictionNotice => V(C.RESN);
    public List<PersonalNameStructure> PersonalNameStructures => List(C.NAME).Select(r => new PersonalNameStructure(r)).ToList();
            
    public string Sex => V(C.SEX);
    // INDIVIDUAL_EVENT_STRUCTURE

    //public List<IndividualAttributeStructure> IndividualAttributeStructure = GetIndividualAttribute(string tag);

    // LDS_INDIVIDUAL_ORDINANCE
    public List<ChildToFamilyLink> ChildToFamilyLinks
    {
        get
        {
            var childToFamilyLinks = List(C.FAMC);
            if (childToFamilyLinks != null)
            {
                return childToFamilyLinks.Select(r => new ChildToFamilyLink(r)).ToList();
            }

            return new List<ChildToFamilyLink>();
        }
    }
    public List<SpouseToFamilyLink>? SpouseToFamilyLinks
    {
        get
        {
            var spouseToFamilyLinks = List(C.ASSO);
            if (spouseToFamilyLinks != null)
            {
                return spouseToFamilyLinks.Select(r => new SpouseToFamilyLink(r)).ToList();
            }

            return null;
        }
    }
    public string Submitter => V(C.SUBM);
    public List<AssociationStructure>? AssociationStructures
    {
        get
        {
            var associationStructures = List(C.ASSO);
            if (associationStructures != null)
            {
                return associationStructures.Select(r => new AssociationStructure(r)).ToList();
            }

            return null;
        }
    }
    public List<string> Aliases => List(C.ALIA).Select(r => r.Value).ToList();
    public List<string> AncestorInterests => List(C.ANCI).Select(r => r.Value).ToList();
    public List<string> DescendantInterests => List(C.DESI).Select(r => r.Value).ToList();
    public string PermanentRecordFileNumber => V(C.RFN);
    public string AncestralFileNumber => V(C.AFN);
    public List<UserReferenceNumber> UserReferenceNumbers => List(C.REFN).Select(r => new UserReferenceNumber(r)).ToList();
    public string Rin => V(C.RIN);
    public ChangeDate? ChangeDate
    {
        get
        {
            var chanRecord = FirstOrDefault(C.CHAN);
            if (chanRecord != null)
            {
                return new ChangeDate(chanRecord);
            }

            return null;
        }
    }
    public List<NoteStructure> NoteStructure
    {
        get
        {
            var noteRecords = List(C.NOTE);
            if (noteRecords != null)
            {
                return noteRecords.Select(r => new NoteStructure(r)).ToList();
            }

            return null;
        }
    }
    public List<SourceCitation> SourceCitation => CreateRecordStructures<SourceCitation>(C.SOUR);
    public List<MultiMediaLink> MultiMediaLink => CreateRecordStructures<MultiMediaLink>(C.OBJE);

    public override string ToString() => $"{PersonalNameStructures.First().Name} {Sex} ({Xref})";
}

public class IndividualRecordJsonConverter : JsonConverter<IndividualRecord>
{
    public override IndividualRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    
    public override void Write(Utf8JsonWriter writer, IndividualRecord indi, JsonSerializerOptions options)
    {
        var jsonObject = new
        {
            indi.RestrictionNotice,
            indi.PersonalNameStructures,
            indi.Sex,
            // INDIVIDUAL_EVENT_STRUCTURE
            // INDIVIDUAL_ATTRIBUTE_STRUCTURE
            // LDS_INDIVIDUAL_ORDINANCE
            // CHILD_TO_FAMILY_LINK
            // SPOUSE_TO_FAMILY_LINK
            indi.Xref,
            indi.Submitter,
            // ASSOCIATION_STRUCTURE
            indi.Aliases,
            indi.AncestorInterests,
            indi.DescendantInterests,
            indi.PermanentRecordFileNumber,
            indi.AncestralFileNumber,
            indi.UserReferenceNumbers,
            indi.Rin,
            indi.ChangeDate,
            indi.NoteStructure,
            indi.SourceCitation,
            indi.MultiMediaLink
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