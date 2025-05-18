using Gedcom.RecordStructures;

namespace Gedcom.CLI;

public class IndividualListItem
{
    public IndividualListItem(IndividualRecord individualRecord)
    {
        Birth = individualRecord.Birth.GedcomDate;
        Death = individualRecord.Death.GedcomDate;
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