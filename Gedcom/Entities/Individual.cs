using Gedcom.RecordStructures;

namespace Gedcom.Entities
{
    public class Individual(IndividualRecord individualRecord)
    {
        private IndividualRecord IndividualRecord { get; } = individualRecord;

        public List<Individual> Children { get; set; } = [];
        public string Given => IndividualRecord.Given;
        public Family? Parents { get; set; }
        public string Surname => IndividualRecord.Surname;
        public string Xref => IndividualRecord.Xref;

        public override string ToString() => $"{Given} {Surname} ({Xref})";
    }
}
