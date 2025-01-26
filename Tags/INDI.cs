namespace Gedcom.Tags;

public class INDI : TagBase
{
    public INDI(Record record) : base(record) { }

    private Record? BIRTRecord => SingleOrDefault(Tag.BIRT);
    public string Birthdate => RecordValue(BIRTRecord, Tag.DATE);
    public string Birthplace => RecordValue(BIRTRecord, Tag.PLAC);

    private Record? DEATRecord => SingleOrDefault(Tag.DEAT);
    public string Deathdate => RecordValue(DEATRecord, Tag.DATE);

    public string Deathplace => RecordValue(DEATRecord, Tag.PLAC);
    public string EXTID => Record.Value;
    public List<Record> FAMSs => GetList(Tag.FAMS);
    public string GIVN => RecordValue(NAMERecord, Tag.GIVN);
    private Record? NAMERecord => SingleOrDefault(Tag.NAME);
    public string NAME => NAMERecord?.Value ?? "";
    public string SEX => SingleValue(Tag.SEX);
    public string SURN => SingleValue(Tag.SURN);

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

        return $"{NAME} ({birthdateText} - {deathdateText}) {SEX} {EXTID}";
    }
}
