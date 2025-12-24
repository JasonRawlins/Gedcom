using Gedcom;

namespace GedcomTests.TestEntities;

public class TestIndividual(string xref, string given, string surname)
{
    public List<TestEvent> Events { get; set; } = [];

    public string Given { get; set; } = given;
    public string Surname { get; set; } = surname;
    public string Xref { get; set; } = xref;

    // The xref without the "@" and "I" characters.
    public string XrefId => Xref.Replace("@", "").Replace("I", "");
}

public class TestIndividuals
{
    public static TestIndividual AnxinZhou
    {
        get
        {
            var individual = new TestIndividual("@I272721746853@", "安信 (Ānxìn)", "周 (Zhōu)");
            individual.Events.Add(new(Tag.Birth, "7 Jul 1932", "China"));
            individual.Events.Add(new(Tag.Death, "7 Jul 1992", "China"));
            return individual;
        }
    }

    public static TestIndividual DylanDavis
    {
        get
        {
            var individual = new TestIndividual("@I272718948910@", "Dylan", "Davis");
            individual.Events.Add(new(Tag.Birth, "7 Jul 1930", "Wales"));
            individual.Events.Add(new(Tag.Death, "7 Jul 1990", "Salt Lake City, Salt Lake, Utah, USA"));
            return individual;
        }
    }

    public static TestIndividual FionaDouglas
    {
        get
        {
            var individual = new TestIndividual("@I272718947188@", "Fiona", "Douglas");
            individual.Events.Add(new(Tag.Birth, "7 Jul 1930", "Scotland"));
            individual.Events.Add(new(Tag.Death, "7 Jul 1990", "Salt Lake City, Salt Lake, Utah, USA"));
            return individual;
        }
    }

    public static TestIndividual GarethDavis
    {
        get
        {
            var individual = new TestIndividual("@I272721732639@", "Garet", "Davis");
            individual.Events.Add(new(Tag.Birth, "7 Jul 1934", "Wales"));
            individual.Events.Add(new(Tag.Death, "7 Jul 1994", "Salt Lake City, Salt Lake, Utah, USA"));
            return individual;
        }
    }

    public static TestIndividual GwenJones
    {
        get
        {
            var individual = new TestIndividual("@I272718949767@", "Gwen", "Jones");
            individual.Events.Add(new(Tag.Birth, "6 Jun 1900", "Wales"));
            individual.Events.Add(new(Tag.Death, "6 Jun 1960", ""));
            return individual;
        }
    }

    public static TestIndividual JamesSmith
    {
        get
        {
            var individual = new TestIndividual("@I272718953593@", "James", "Smith");
            individual.Events.Add(new(Tag.Birth, "8 Aug 1960", "England"));
            individual.Events.Add(new(Tag.Death, "8 Aug 2020", "Salt Lake City, Salt Lake, Utah, USA"));
            return individual;
        }
    }

    public static TestIndividual MargaretDavis
    {
        get
        {
            var individual = new TestIndividual("@I272721726882@", "Margaret", "Davis");
            individual.Events.Add(new(Tag.Birth, "7 Jul 1932", "Wales"));
            individual.Events.Add(new(Tag.Death, "7 Jul 1992", "China"));
            return individual;
        }
    }

    public static TestIndividual MarySmith
    {
        get
        {
            var individual = new TestIndividual("@I272718954607@", "Mary", "Smith");
            individual.Events.Add(new(Tag.Birth, "9 Sep 1990", "Utah, USA"));
            return individual;
        }
    }

    public static TestIndividual OwenDavis
    {
        get
        {
            var individual = new TestIndividual("@I272718952211@", "Owen", "Davis");
            individual.Events.Add(new(Tag.Birth, "6 Jun 1900", "Wales"));
            individual.Events.Add(new(Tag.Death, "6 Jun 1960", "Utah, USA"));
            return individual;
        }
    }

    public static TestIndividual SaraDavis
    {
        get
        {
            var individual = new TestIndividual("@I272718947187@", "Sara", "Davis");
            individual.Events.Add(new(Tag.Birth, "8 Aug 1960", "Salt Lake City, Salt Lake, Utah, USA"));
            individual.Events.Add(new(Tag.Death, "8 Aug 2020", "Salt Lake City, Salt Lake, Utah, USA"));
            return individual;
        }
    }

    public static TestIndividual XiaohuiZhou
    {
        get
        {
            var individual = new TestIndividual("@I272721748631@", "晓慧 (Xiǎo huì)", "周 (Zhōu)");
            individual.Events.Add(new(Tag.Birth, "8 Aug 1960", "China"));
            individual.Events.Add(new(Tag.Death, "8 Aug 2012", "China"));
            return individual;
        }
    }
}