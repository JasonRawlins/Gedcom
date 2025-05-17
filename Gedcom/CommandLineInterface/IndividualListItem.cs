using Gedcom.RecordStructures;

namespace Gedcom.CLI;

public class IndividualListItem
{
    public IndividualListItem(IndividualRecord individualRecord)
    {
        Birth = new GedcomDate(individualRecord.Birth.Record);
        Death = new GedcomDate(individualRecord.Death.Record);
        FullName = individualRecord.FullName;
        Given = individualRecord.Given;
        Surname = individualRecord.Surname;
        Xref = individualRecord.Xref;
    }

    public GedcomDate Birth { get; set; }
    public GedcomDate Death { get; set; }
    public string FullName { get; set; }
    public string Given { get; set; }
    public string Surname { get; set; }
    public string Xref { get; set; }

    public override string ToString()
    {
        return $"{Surname}, {Given} ({Birth.DayMonthYear} — {Death.DayMonthYear}). {Xref}";
    }
}