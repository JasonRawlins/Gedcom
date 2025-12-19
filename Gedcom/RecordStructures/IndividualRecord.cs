using Newtonsoft.Json;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(IndividualRecordJsonConverter))]
public class IndividualRecord : RecordStructureBase
{
    public IndividualRecord() { }
    public IndividualRecord(Record record) : base(record) { }

    public List<string> Aliases => List(r => r.Tag.Equals(Tag.Alias)).Select(r => r.Value).ToList();
    public List<string> AncestorInterests => List(r => r.Tag.Equals(Tag.AncesInterest)).Select(r => r.Value).ToList();
    public string AncestralFileNumber => _(Tag.AncestralFileNumber);
    public List<AssociationStructure> AssociationStructures => List<AssociationStructure>(Tag.Associates);
    public string AutomatedRecordId => _(Tag.RecordIdNumber);
    public ChangeDate ChangeDate => First<ChangeDate>(Tag.Change);
    public List<ChildToFamilyLink> ChildToFamilyLinks => List<ChildToFamilyLink>(Tag.FamilyChild);
    public List<string> DescendantInterests => List(r => r.Tag.Equals(Tag.DescendantInterest)).Select(r => r.Value).ToList();
    public List<IndividualAttributeStructure> IndividualAttributeStructures => List<IndividualAttributeStructure>(Record.Tag);
    public List<IndividualEventStructure> IndividualEventStructures => List(IndividualEventStructure.IsIndividualEventStructure).Select(r => new IndividualEventStructure(r)).ToList();
    public List<LdsIndividualOrdinance> LdsIndividualOrdinances => List<LdsIndividualOrdinance>(Tag.Ordinance);
    public List<MultimediaLink> MultimediaLinks => List<MultimediaLink>(Tag.Object);
    public List<NoteStructure> NoteStructures => List<NoteStructure>(Tag.Note);
    public string PermanentRecordFileNumber => _(Tag.RecordFileNumber);
    public List<PersonalNameStructure> PersonalNameStructures => List<PersonalNameStructure>(Tag.Name);
    public string RestrictionNotice => _(Tag.Restriction);
    public string SexValue => _(Tag.Sex);
    public List<SourceCitation> SourceCitations => List<SourceCitation>(Tag.Source);
    public List<SpouseToFamilyLink> SpouseToFamilyLinks => List<SpouseToFamilyLink>(Tag.FamilySpouse);
    public string Submitter => _(Tag.Submission);
    public List<UserReferenceNumber> UserReferenceNumbers => List<UserReferenceNumber>(Tag.Reference);
    public string Xref => Record.Value;

    #region Convenience properties

    public string FullName => $"{Given}, {Surname}";
    public string Given => $"{PersonalNameStructures[0].Given}";
    public string Surname => $"{PersonalNameStructures[0].Surname}";

    #endregion

    #region Strongly-typed IndividualEventStructures

    public IndividualEventStructure Birth => First<IndividualEventStructure>(Tag.Birth);
    public IndividualEventStructure Death => First<IndividualEventStructure>(Tag.Death);

    #endregion

    private bool IsWeaklyTyped(Record record)
    {
        // By weakly-typed, I mean there isn't a property that specifically
        // exposes the event. For example, there is a property named "Birth"
        // that exposes only the BIRT IndividualEventStructure.
        return !(new string[]
        {
            "BIRT", "DEAT"
        }.Contains(record.Tag));
    }

    public override string ToString() => $"{Record.Value}, {PersonalNameStructures.First().NamePersonal}, {SexValue}";
}

internal class IndividualRecordJsonConverter : JsonConverter<IndividualRecord>
{
    public override IndividualRecord? ReadJson(JsonReader reader, Type objectType, IndividualRecord? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

    public override void WriteJson(JsonWriter writer, IndividualRecord? individualRecord, JsonSerializer serializer)
    {
        if (individualRecord == null) throw new ArgumentNullException(nameof(individualRecord));

        serializer.Serialize(writer, new IndividualRecordJson(individualRecord));
    }
}

internal class IndividualRecordJson : GedcomJson
{
    public IndividualRecordJson(IndividualRecord individualRecord)
    {
        Aliases = JsonList(individualRecord.Aliases);
        AncestorInterests = JsonList(individualRecord.AncestorInterests);
        AncestralFileNumber = JsonString(individualRecord.AncestralFileNumber);
        Associations = JsonList(individualRecord.AssociationStructures);
        AutomatedRecordId = JsonString(individualRecord.AutomatedRecordId);
        Birth = JsonString($"{individualRecord.Birth.DateValue} at {individualRecord.Birth.PlaceStructure.PlaceName}");
        ChangeDate = JsonRecord(individualRecord.ChangeDate);
        ChildToFamilyLinks = JsonList(individualRecord.ChildToFamilyLinks);
        Death = JsonString($"{individualRecord.Death.DateValue} at {individualRecord.Death.PlaceStructure.PlaceName}");
        DescendantInterests = JsonList(individualRecord.DescendantInterests);
        IndividualAttributes = JsonList(individualRecord.IndividualAttributeStructures);
        IndividualEvents = JsonList(individualRecord.IndividualEventStructures);
        LdsIndividualOrdinances = JsonList(individualRecord.LdsIndividualOrdinances);
        MultimediaLinks = JsonList(individualRecord.MultimediaLinks);
        Notes = JsonList(individualRecord.NoteStructures);
        PermanentRecordFileNumber = JsonString(individualRecord.PermanentRecordFileNumber);
        PersonalNames = JsonList(individualRecord.PersonalNameStructures);
        RestrictionNotice = JsonString(individualRecord.RestrictionNotice);
        Sex = JsonString(individualRecord.SexValue);
        SourceCitations = JsonList(individualRecord.SourceCitations);
        SpouseToFamilyLinks = JsonList(individualRecord.SpouseToFamilyLinks);
        Submitter = JsonString(individualRecord.Submitter);
        UserReferenceNumbers = JsonList(individualRecord.UserReferenceNumbers);
        Xref = individualRecord.Xref;
    }

    public List<string>? Aliases { get; set; }
    public List<string>? AncestorInterests { get; set; }
    public string? AncestralFileNumber { get; set; }
    public List<AssociationStructure>? Associations { get; set; }
    public string? AutomatedRecordId { get; set; }
    public string? Birth { get; set; }
    public ChangeDate? ChangeDate { get; set; }
    public List<ChildToFamilyLink>? ChildToFamilyLinks { get; set; }
    public string? Death { get; set; }
    public List<string>? DescendantInterests { get; set; }
    public string Given => PersonalNames == null ? "" : PersonalNames[0].Given;
    public List<IndividualAttributeStructure>? IndividualAttributes { get; set; }
    public List<IndividualEventStructure>? IndividualEvents { get; set; }
    public List<LdsIndividualOrdinance>? LdsIndividualOrdinances { get; set; }
    public List<MultimediaLink>? MultimediaLinks { get; set; }
    public List<NoteStructure>? Notes { get; set; }
    public string? PermanentRecordFileNumber { get; set; }
    public List<PersonalNameStructure>? PersonalNames { get; set; }
    public string? RestrictionNotice { get; set; }
    public string? Sex { get; set; }
    public List<SourceCitation>? SourceCitations { get; set; }
    public List<SpouseToFamilyLink>? SpouseToFamilyLinks { get; set; }
    public string? Submitter { get; set; }
    public string Surname => PersonalNames == null ? "" : PersonalNames[0].Surname;
    public List<UserReferenceNumber>? UserReferenceNumbers { get; set; }
    public string? Xref { get; set; }
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