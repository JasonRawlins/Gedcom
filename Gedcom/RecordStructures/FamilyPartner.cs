namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class FamilyPartner : RecordStructureBase
{
    public FamilyPartner() { }
    public FamilyPartner(Record record) { }

    private string? _ageAtEvent = null;
    public string AgeAtEvent => _ageAtEvent ??= Record.Records.FirstOrDefault(r => r.Tag == Tag.Age)?.Value ?? "";
   
    public string Name => Record.Value;

    public override string ToString() => $"{Record.Value}, {Name}";
}