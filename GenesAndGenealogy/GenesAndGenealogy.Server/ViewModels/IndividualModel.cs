using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class IndividualModel
{
    public IndividualModel(IndividualRecord individualRecord, TreeModel treeModel)
    {
        AutomatedRecordId = individualRecord.AutomatedRecordId;
        Birth = new EventModel(individualRecord.Birth);
        Death = new EventModel(individualRecord.Death);
        FullName = $"{Given} {Surname}";
        Given = individualRecord.PersonalNameStructures[0].Given;
        PersonalName = individualRecord.PersonalNameStructures[0].NamePersonal;
        SexValue = individualRecord.SexValue;
        Submitter = individualRecord.Submitter;
        Surname = individualRecord.PersonalNameStructures[0].Surname;
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
    public string FullName { get; set; }
    public string Given { get; set; }
    public string PersonalName { get; set; }
    public string SexValue { get; set; }
    public string Submitter { get; set; }
    public string Surname { get; set; }
    private string TreeId { get; set; }
    public string Xref { get; set; }

    public override string ToString() => $"{FullName} ({Birth.Date.DayMonthYear})-{Death.Date.DayMonthYear})";
}
