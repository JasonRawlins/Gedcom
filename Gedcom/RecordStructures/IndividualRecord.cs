using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
[JsonConverter(typeof(IndividualJsonConverter))]
public class IndividualRecord : RecordStructureBase
{
    public IndividualRecord() { }
    public IndividualRecord(Record record) : base(record) { }

    private List<string>? _aliases = null;
    public List<string> Aliases => _aliases ??= GetStringList(Tag.Alias);

    private List<string>? _ancestorInterests = null;
    public List<string> AncestorInterests => _ancestorInterests ??= GetStringList(Tag.AncesInterest);

    private string? _ancestralFileNumber = null;
    public string AncestralFileNumber => _ancestralFileNumber ??= GetValue(Tag.AncestralFileNumber);

    private List<AssociationStructure>? _associationStructures = null;
    public List<AssociationStructure> AssociationStructures => _associationStructures ??= List<AssociationStructure>(Tag.Associates);

    private string? _automatedRecordId = null;
    public string AutomatedRecordId => _automatedRecordId ??= GetValue(Tag.RecordIdNumber);

    private ChangeDate? _changeDate = null;
    public ChangeDate ChangeDate => _changeDate ??= First<ChangeDate>(Tag.Change);

    private List<ChildToFamilyLink>? _childToFamilyLinks = null;
    public List<ChildToFamilyLink> ChildToFamilyLinks => _childToFamilyLinks ??= List<ChildToFamilyLink>(Tag.FamilyChild);

    private List<string>? _descendantInterests = null;
    public List<string> DescendantInterests => _descendantInterests ??= GetStringList(Tag.DescendantInterest);

    private List<EventStructure>? _individualEventStructures = null;
    public List<EventStructure> IndividualEventStructures => _individualEventStructures ??= [.. List(IndividualEventStructure.IsIndividualEventStructure).Select(r => new EventStructure(r))];

    private List<LdsIndividualOrdinance>? _ldsIndividualOrdinances = null;
    public List<LdsIndividualOrdinance> LdsIndividualOrdinances => _ldsIndividualOrdinances ??= List<LdsIndividualOrdinance>(Tag.Ordinance);

    private List<MultimediaLink>? _multimediaLinks = null;
    public List<MultimediaLink> MultimediaLinks => _multimediaLinks ??= List<MultimediaLink>(Tag.Object);

    private List<NoteStructure>? _noteStructures = null;
    public List<NoteStructure> NoteStructures => _noteStructures ??= List<NoteStructure>(Tag.Note);

    private string? _permanentRecordFileNumber = null;
    public string PermanentRecordFileNumber => _permanentRecordFileNumber ??= GetValue(Tag.RecordFileNumber);

    private List<PersonalNameStructure>? _personalNameStructures = null;
    public List<PersonalNameStructure> PersonalNameStructures => _personalNameStructures ??= List<PersonalNameStructure>(Tag.Name);

    private string? _restrictionNotice = null;
    public string RestrictionNotice => _restrictionNotice ??= GetValue(Tag.Restriction);

    private string? _sexValue = null;
    public string SexValue => _sexValue ??= GetValue(Tag.Sex);

    private List<SourceCitation>? _sourceCitations = null;
    public List<SourceCitation> SourceCitations => _sourceCitations ??= List<SourceCitation>(Tag.Source);

    private List<SpouseToFamilyLink>? _spouseToFamilyLinks = null;
    public List<SpouseToFamilyLink> SpouseToFamilyLinks => _spouseToFamilyLinks ??= List<SpouseToFamilyLink>(Tag.FamilySpouse);

    private string? _submitter = null;
    public string Submitter => _submitter ??= GetValue(Tag.Submission);

    private List<UserReferenceNumber>? _userReferenceNumbers = null;
    public List<UserReferenceNumber> UserReferenceNumbers => _userReferenceNumbers ??= List<UserReferenceNumber>(Tag.Reference);
   
    public string Xref => Record.Value;

    #region Convenience properties

    public string FullName => $"{Given}, {Surname}";
    
    public string Given => PersonalNameStructures.Count > 0 ? PersonalNameStructures[0].Given : "";
   
    public string NamePersonal => PersonalNameStructures.Count > 0 ? PersonalNameStructures[0].NamePersonal : "";
   
    public string Surname => PersonalNameStructures.Count > 0 ? PersonalNameStructures[0].Surname : "";

    #endregion

    #region Strongly-typed IndividualEventStructures

    private EventStructure? birth = null;
    public EventStructure Birth => birth ??= First<EventStructure>(Tag.Birth);

    private EventStructure? death = null;
    public EventStructure Death => death ??= First<EventStructure>(Tag.Death);

    #endregion

    private static bool IsWeaklyTyped(Record record)
    {
        // By weakly-typed, I mean there isn't a property that specifically
        // exposes the event. For example, there is a property named "Birth"
        // that exposes only the BIRT IndividualEventStructure.
        return !(new string[]
        {
            Tag.Birth, Tag.Death
        }.Contains(record.Tag));
    }

    public override string ToString() => $"{Record.Value}, {PersonalNameStructures.First().NamePersonal}, {SexValue}";
}

internal sealed class IndividualJsonConverter : JsonConverter<IndividualRecord>
{
    public override IndividualRecord? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, IndividualRecord value, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(value);
        JsonSerializer.Serialize(writer, new IndividualJson(value), options);
    }
}

public class IndividualJson : GedcomJson
{
    public IndividualJson(IndividualRecord individualRecord)
    {
        Aliases = JsonList(individualRecord.Aliases);
        AncestorInterests = JsonList(individualRecord.AncestorInterests);
        AncestralFileNumber = JsonString(individualRecord.AncestralFileNumber);
        Associations = JsonList(individualRecord.AssociationStructures.Select(_as => new AssociationJson(_as)).ToList());
        AutomatedRecordId = JsonString(individualRecord.AutomatedRecordId);
        Birth = JsonRecord(new EventJson(individualRecord.Birth));
        ChangeDate = JsonRecord(new ChangeDateJson(individualRecord.ChangeDate));
        ChildToFamilyLinks = JsonList(individualRecord.ChildToFamilyLinks.Select(ctfl => new ChildToFamilyLinkJson(ctfl)).ToList());
        Death = JsonRecord(new EventJson(individualRecord.Death));
        DescendantInterests = JsonList(individualRecord.DescendantInterests);
        Events = individualRecord.IndividualEventStructures.Select(ies => new EventJson(ies)).ToList();
        IsEmpty = individualRecord.IsEmpty;
        LdsIndividualOrdinances = JsonList(individualRecord.LdsIndividualOrdinances.Select(lio => new LdsIndividualOrdinanceJson(lio)).ToList());
        MultimediaLinks = JsonList(individualRecord.MultimediaLinks.Select(ml => new MultimediaLinkJson(ml)).ToList());
        Notes = JsonList(individualRecord.NoteStructures.Select(ns => ns.Text).ToList());
        PermanentRecordFileNumber = JsonString(individualRecord.PermanentRecordFileNumber);
        PersonalNames = JsonList(individualRecord.PersonalNameStructures.Select(pns => new PersonalNameJson(pns)).ToList());
        RestrictionNotice = JsonString(individualRecord.RestrictionNotice);
        Sex = JsonString(individualRecord.SexValue);
        SourceCitations = JsonList(individualRecord.SourceCitations.Select(sc => new SourceCitationJson(sc)).ToList());
        SpouseToFamilyLinks = JsonList(individualRecord.SpouseToFamilyLinks.Select(stfl => new SpouseToFamilyLinkJson(stfl)).ToList());
        Submitter = JsonString(individualRecord.Submitter);
        TreeId = "";
        UserReferenceNumbers = JsonList(individualRecord.UserReferenceNumbers.Select(urn => new UserReferenceNumberJson(urn)).ToList());
        Xref = individualRecord.Xref;
    }

    public IndividualJson(IndividualRecord individualRecord, string treeId) : this(individualRecord)
    {
        TreeId = treeId;
    }

    public List<string>? Aliases { get; set; } = [];
    public List<string>? AncestorInterests { get; set; } = [];
    public string? AncestralFileNumber { get; set; }

    public string AncestryLink
    {
        get
        {
            var xrefNumbersOnly = string.IsNullOrEmpty(Xref) ? "" : Xref.Replace("@", "").Replace("I", "");
            return $"https://www.ancestry.com/family-tree/person/tree/{TreeId}/person/{xrefNumbersOnly}/facts";
        }
    }

    public List<AssociationJson>? Associations { get; set; } = [];
    public string? AutomatedRecordId { get; set; }
    public EventJson? Birth { get; set; }
    public ChangeDateJson? ChangeDate { get; set; }
    public List<ChildToFamilyLinkJson>? ChildToFamilyLinks { get; set; } = [];
    public EventJson? Death { get; set; }
    public List<string>? DescendantInterests { get; set; } = [];
    public List<EventJson>? Events { get; set; } = [];
    public string FullName => $"{Given} {Surname}";
    public string? Given => PersonalNames?.Count > 0 ? PersonalNames[0].Given : "";
    public List<LdsIndividualOrdinanceJson>? LdsIndividualOrdinances { get; set; } = [];
    public List<MultimediaLinkJson>? MultimediaLinks { get; set; } = [];
    public List<string>? Notes { get; set; } = [];
    public string? PermanentRecordFileNumber { get; set; }
    public List<PersonalNameJson>? PersonalNames { get; set; } = [];
    public string? RestrictionNotice { get; set; }
    public string? Sex { get; set; }
    public List<SourceCitationJson>? SourceCitations { get; set; } = [];
    public List<SpouseToFamilyLinkJson>? SpouseToFamilyLinks { get; set; } = [];
    public string? Submitter { get; set; }
    public string? Surname => PersonalNames?.Count > 0 ? PersonalNames[0].Surname : "";
    public string? TreeId { get; set; }
    public List<UserReferenceNumberJson>? UserReferenceNumbers { get; set; } = [];
    public string? Xref { get; set; }

    public override string ToString() => $"{Given} {Surname}";
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