using Gedcom;

namespace GedcomTests.TestData;

public static class TestTree
{
    public static class Individuals
    {
        public static TestIndividual DylanDavis
        {
            get
            {
                var individual = new TestIndividual("@I272718948910@", "Dylan", "Davis");
                individual.Events.Add(new(Tag.BIRT, "7 Jul 1930", "Wales"));
                individual.Events.Add(new(Tag.DEAT, "7 Jul 1990", "Salt Lake City, Salt Lake, Utah, USA"));
                return individual;
            }
        }

        public static TestIndividual FionaDouglas
        {
            get
            {
                var individual = new TestIndividual("@I272718947188@", "Fiona", "Douglas");
                individual.Events.Add(new(Tag.BIRT, "7 Jul 1930", "Wales"));
                individual.Events.Add(new(Tag.DEAT, "7 Jul 1990", "Salt Lake City, Salt Lake, Utah, USA"));
                return individual;
            }
        }

        public static TestIndividual GwenJones
        {
            get
            {
                var individual = new TestIndividual("@I272718949767@", "Gwen", "Jones");
                individual.Events.Add(new(Tag.BIRT, "6 Jun 1900", "Wales"));
                individual.Events.Add(new(Tag.DEAT, "6 Jun 1960", ""));
                return individual;
            }
        }

        public static TestIndividual JamesSmith
        {
            get
            {
                var individual = new TestIndividual("@I272718953593@", "James", "Smith");
                individual.Events.Add(new(Tag.BIRT, "8 Aug 1960", "England"));
                individual.Events.Add(new(Tag.DEAT, "8 Aug 2020", "Salt Lake City, Salt Lake, Utah, USA"));
                return individual;
            }
        }

        public static TestIndividual MarySmith
        {
            get
            {
                var individual = new TestIndividual("@I272718954607@", "Mary", "Smith");
                individual.Events.Add(new(Tag.BIRT, "9 Sep 1990", "Utah, USA"));
                return individual;
            }
        }

        public static TestIndividual OwenDavis
        {
            get
            {
                var individual = new TestIndividual("@I272718952211@", "Owen", "Davis");
                individual.Events.Add(new(Tag.BIRT, "6 Jun 1900", "Wales"));
                individual.Events.Add(new(Tag.DEAT, "6 Jun 1960", "Utah, USA"));
                return individual;
            }
        }

        public static TestIndividual SaraDavis
        {
            get
            {
                var individual = new TestIndividual("@I272718947187@", "Sara", "Davis");
                individual.Events.Add(new(Tag.BIRT, "8 Aug 1960", "Salt Lake City, Salt Lake, Utah, USA"));
                individual.Events.Add(new(Tag.DEAT, "8 Aug 2020", "Salt Lake City, Salt Lake, Utah, USA"));
                return individual;
            }
        }
    }

    public static class Families
    {
        public static TestFamily DylanDavisAndFionaDouglas
        {
            get
            {
                var family = new TestFamily("@F1@", Individuals.DylanDavis, Individuals.FionaDouglas, [Individuals.SaraDavis])
                {
                    Marriage = new(Tag.MARR, "7 Jul 1955", "")
                };
                return family;
            }
        }
           

        public static TestFamily JamesSmithAndSaraDavis
        {
            get
            {
                var family = new TestFamily("@F2@", Individuals.JamesSmith, Individuals.SaraDavis, [Individuals.MarySmith])
                {
                    Marriage = new(Tag.MARR, "8 Aug 1985", "")
                };
                return family;
            }
        }

        public static TestFamily OwenDavisAndGwenJones
        {
            get
            {
                var family = new TestFamily("@F3@", Individuals.OwenDavis, Individuals.GwenJones, [Individuals.DylanDavis])
                {
                    Marriage = new(Tag.MARR, "6 Jun 1925", "")
                };
                return family;
            }
        }
    }

    public static class Repositories
    {
        public static readonly TestRepository VitalRecordsRepository = new("@R856097590@", "Vital records repository");
    }

    public static class Sources
    {
        public static readonly TestSource VitalRecords = new("@S976697667@", "Vital records");
    }
}

public class TestIndividual(string xref, string given, string surname)
{
    public List<TestEvent> Events { get; set; } = [];

    public string Given { get; set; } = given;
    public string Surname { get; set; } = surname;
    public string Xref { get; set; } = xref;
    public string XrefId => Xref.Replace("@", "").Replace("I", "");
}

public class TestFamily(string xref, TestIndividual husband, TestIndividual wife)
{
    public TestFamily(string xref, TestIndividual husband, TestIndividual wife, List<TestIndividual> children) : this(xref, husband, wife)
    {
        Children = children;
    }

    public List<TestIndividual> Children { get; set; } = [];
    public TestIndividual Husband { get; set; } = husband;
    public TestEvent? Marriage { get; set; }
    public TestIndividual Wife { get; set; } = wife;
    public string Xref { get; set; } = xref;
}

public class TestRepository(string xref, string name)
{
    public string Name { get; set; } = name;
    public string Xref { get; set; } = xref;
}

public class TestSource(string xref, string title)
{
    public string Xref { get; set; } = xref;
    public string Title { get; set; } = title;
}

public class TestCitation(string sourceXref, string page)
{
    public string SourceXref { get; set; } = sourceXref;
    public string Page { get; set; } = page;
}

public class TestEvent(string eventType, string date, string place)
{
    public TestEvent(string eventType, string date, string place, string note) : this(eventType, date, place)
    {
        Note = note;
    }

    public TestEvent(string eventType, string date, string place, string note, List<TestCitation> citations) : this(eventType, date, place, note)
    {
        Citations = citations;
    }

    public List<TestCitation> Citations { get; set; } = [];
    public string Date { get; set; } = date;
    public string EventType { get; set; } = eventType;
    public string Note { get; set; } = "";
    public string Place { get; set; } = place;
}