using System.Runtime.Intrinsics.X86;
using System;

namespace Gedcom.RecordStructures;

public class IndividualRecord : RecordStructureBase
{
    public IndividualRecord(Record record) : base(record) { }

    public string RestrictionNotice => _(C.RESN);
    public List<PersonalNameStructure> PersonalNameStructures => List<PersonalNameStructure>(C.NAME);
    public string SexValue => _(C.SEX);
    public List<IndividualEventStructure> IndividualEventStructures
    {
        get
        {
            var records = Record.Records.Where(r => IsIndividualEventStructure(r)).Select(r => new IndividualEventStructure(r)).ToList();
            return records;
        }
    }
    public List<IndividualAttributeStructure> IndividualAttributeStructures => List<IndividualAttributeStructure>(Record.Tag);
    public List<LdsIndividualOrdinance> LdsIndividualOrdinances => List<LdsIndividualOrdinance>(C.ORDI);
    public List<ChildToFamilyLink> ChildToFamilyLinks => List<ChildToFamilyLink>(C.FAMC);
    public List<SpouseToFamilyLink>? SpouseToFamilyLinks => List<SpouseToFamilyLink>(C.ASSO);
    public string Submitter => _(C.SUBN);
    public List<AssociationStructure> AssociationStructures => List<AssociationStructure>(C.ASSO);
    public List<string> Aliases => List(r => r.Tag.Equals(C.ALIA)).Select(r => r.Value).ToList();
    public List<string> AncestorInterests => List(r => r.Tag.Equals(C.ANCI)).Select(r => r.Value).ToList();
    public List<string> DescendantInterests => List(r => r.Tag.Equals(C.DESI)).Select(r => r.Value).ToList();
    public string PermanentRecordFileNumber => _(C.RFN);
    public string AncestralFileNumber => _(C.AFN);
    public List<UserReferenceNumber> UserReferenceNumbers => List<UserReferenceNumber>(C.REFN);
    public string AutomatedRecordId => _(C.RIN);
    public ChangeDate? ChangeDate => FirstOrDefault<ChangeDate>(C.CHAN);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(C.NOTE);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(C.SOUR);
    public List<MultimediaLink> MultiMediaLinks => List<MultimediaLink>(C.OBJE);

    private bool IsIndividualEventStructure(Record record)
    {
        return new string[] 
        { 
            "BIRT", "CHR" , "DEAT", "BURI", "CREM", "ADOP", "BAPM",
            "BARM", "BASM", "BLES", "CHRA", "CONF", "FCOM", "ORDN",
            "NATU", "EMIG", "IMMI", "CENS", "PROB", "WILL", "GRAD",
            "RETI", "EVEN"
        }.Contains(record.Tag);
    }

    public override string ToString() => $"{PersonalNameStructures.First().NamePersonal} {SexValue} ({Record.Value})";
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
    1 NAME Fred/Jones/
    1 ASSO @I2@
        2 RELA Godfather
*/
#endregion