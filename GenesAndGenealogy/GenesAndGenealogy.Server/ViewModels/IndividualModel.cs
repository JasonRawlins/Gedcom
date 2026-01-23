using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class IndividualModel
{
    public IndividualModel(IndividualRecord individualRecord, TreeModel treeModel)
    {
        AutomatedRecordId = individualRecord.AutomatedRecordId;
        Birth = new EventModel(individualRecord.Birth);
        Death = new EventModel(individualRecord.Death);
        Events = individualRecord.IndividualEventStructures.Select(ies => new EventModel(ies)).ToList();
        Given = individualRecord.Given;
        IsEmpty = individualRecord.IsEmpty;
        PersonalName = individualRecord.PersonalName;
        Sex = individualRecord.SexValue;
        Submitter = individualRecord.Submitter;
        Surname = individualRecord.Surname;
        TreeId = treeModel.AutomatedRecordId;
        Xref = individualRecord.Xref;
    }

    public string AncestryLink
    {
        get
        {
            var xrefNumbersOnly = Xref.Replace("@", "").Replace("I", "");
            return $"https://www.ancestry.com/family-tree/person/tree/{TreeId}/person/{xrefNumbersOnly}/facts";
        }
    }
    public string AutomatedRecordId { get; set; }
    public EventModel Birth { get; set; }
    public EventModel Death { get; set; }
    public List<EventModel> Events { get; set; }
    public string FullName => $"{Given} {Surname}";
    public string Given { get; set; }
    public bool IsEmpty { get; set; }
    public string PersonalName { get; set; }
    public string Sex { get; set; }
    public string Submitter { get; set; }
    public string Surname { get; set; }
    private string TreeId { get; set; }
    public string Xref { get; set; }
    public string XrefNumber => string.IsNullOrEmpty(Xref) ? "" : Xref.Replace("@", "").Substring(1);

    public override string ToString() => $"{FullName} ({Birth.Date.DayMonthYear})-{Death.Date.DayMonthYear})";
}
