namespace Gedcom.Tags;

public class FAM
{
    private Record Record { get; }
    public FAM(Record record)
    {
        Record = record;
    }

    public List<Record> Partners => Record.Records.Where(r => r.Tag.Equals(Tag.WIFE) || r.Tag.Equals(Tag.HUSB)).ToList();

    public override string ToString()
    {
        var famsIdList = "(";
        foreach (var partner in Partners)
        {
            famsIdList += $"{partner.Value}, ";
        }
        return famsIdList.Trim(',') + ")";
    }
}
