using Gedcom.RecordStructures;

namespace Gedcom.Entities;

public class Individual(IndividualRecord individualRecord)
{
    private IndividualRecord IndividualRecord { get; } = individualRecord;

    public List<Individual> Children { get; set; } = [];
    public string Given => IndividualRecord.Given;
    public List<MultimediaRecord> MultimediaRecords { get; set; } = [];
    public Family? Parents { get; set; }
    public List<Individual> Siblings { get; set; } = [];
    public string Sex => IndividualRecord.SexValue;
    public string Surname => IndividualRecord.Surname;
    public string Xref => IndividualRecord.Xref;

    public void AddChild(Individual individual)
    {
        if (!Children.Contains(individual))
        {
            Children.Add(individual);
        }
    }

    public override string ToString() => $"{Given} {Surname} ({Xref})";
}
