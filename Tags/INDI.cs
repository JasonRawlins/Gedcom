namespace Gedcom.Tags;

public class INDI : TagBase
{
    public INDI(Record record) : base(record) { }

    private Record? BIRTRecord => FirstOrDefault(Tags.Tag.BIRT);
    public string Birthdate => RecordValue(BIRTRecord, Tags.Tag.DATE);
    public string Birthplace => RecordValue(BIRTRecord, Tags.Tag.PLAC);

    private Record? DEATRecord => FirstOrDefault(Tags.Tag.DEAT);
    public string Deathdate => RecordValue(DEATRecord, Tags.Tag.DATE);

    public string Deathplace => RecordValue(DEATRecord, Tags.Tag.PLAC);
    public string EXTID => Record.Value;
    public List<Record> FAMSs => List(Tags.Tag.FAMS);
    public string GIVN => RecordValue(NAMERecord, Tags.Tag.GIVN);
    private Record? NAMERecord => FirstOrDefault(Tags.Tag.NAME);
    public string NAME => NAMERecord?.Value ?? "";
    public string SEX => Value(Tags.Tag.SEX);
    public string SURN => Value(Tags.Tag.SURN);

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
