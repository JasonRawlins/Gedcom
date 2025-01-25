using System.Xml;

namespace Gedcom;

public class INDI
{
    private Record Record { get; }
    public INDI(Record record)
    {
        Record = record;
    }

    private Record BIRTRecord => Record.Records.Single(r => r.Tag.Equals(Tag.BIRT));
    public string Birthdate => BIRTRecord.Records.Single(r => r.Tag.Equals(Tag.DATE)).Value;
    public string Birthplace => BIRTRecord.Records.Single(r => r.Tag.Equals(Tag.PLAC)).Value;

    private Record DEATRecord => Record.Records.Single(r => r.Tag.Equals(Tag.DEAT));
    public string Deathdate => DEATRecord.Records.Single(r => r.Tag.Equals(Tag.DATE)).Value;
    public string Deathplace => DEATRecord.Records.Single(r => r.Tag.Equals(Tag.PLAC)).Value;
    public string EXTID => Record.Value;
    public string FAMSId => Record.Records.Single(r => r.Tag.Equals(Tag.FAMS)).Value;
    public string GIVN => NAMERecord.Records.Single(r => r.Tag.Equals(Tag.GIVN)).Value;
    private Record NAMERecord => Record.Records.Single(r => r.Tag.Equals(Tag.NAME));
    public string NAME => NAMERecord.Value;
    public string SEX => Record.Records.Single(r => r.Tag.Equals(Tag.SEX)).Value;
    public string SURN => NAMERecord.Records.Single(r => r.Tag == Tag.SURN).Value;

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
