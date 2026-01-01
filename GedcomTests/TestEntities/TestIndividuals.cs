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
    public static TestIndividual AbigailBrown
    {
        get
        {
            var individual = new TestIndividual("@I272721828928@", "Abigail", "Brown");
            individual.Events.Add(new(Tag.Birth, "7 JUL 1930", "England"));
            individual.Events.Add(new(Tag.Death, "7 JUL 1990", "England"));
            return individual;
        }
    }

    public static TestIndividual AnwenDavis
    {
        get
        {
            var individual = new TestIndividual("@I272721827508@", "Anwen", "Davis");
            individual.Events.Add(new(Tag.Birth, "6 JUN 1900", "Wales"));
            individual.Events.Add(new(Tag.Death, "6 JUN 1960", "Wales"));
            return individual;
        }
    }

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

    public static TestIndividual CarwynDavis
    {
        get
        {
            var individual = new TestIndividual("@I272721827103@", "Carwyn", "Davis");
            individual.Events.Add(new(Tag.Birth, "5 May 1870", "Wales"));
            individual.Events.Add(new(Tag.Death, "5 May 1870", "Wales"));
            return individual;
        }
    }

    public static TestIndividual CelynVaughn
    {
        get
        {
            var individual = new TestIndividual("@I272721828893@", "Celyn", "Vaughn");
            individual.Events.Add(new(Tag.Birth, "7 JUL 1930", "Wales"));
            individual.Events.Add(new(Tag.Death, "7 JUL 1990", "Wales"));
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

    public static TestIndividual EithneLynch
    {
        get
        {
            var individual = new TestIndividual("@I272723846975@", "Eithne", "Lynch");
            individual.Events.Add(new(Tag.Birth, "7 Jul 1930", "Ireland"));
            individual.Events.Add(new(Tag.Death, "7 Jul 1990", "Ireland"));
            return individual;
        }
    }

    public static TestIndividual ElizabethRhys
    {
        get
        {
            var individual = new TestIndividual("@I272721827126@", "Elizabeth", "Rhys");
            individual.Events.Add(new(Tag.Birth, "5 May 1870", "Wales"));
            individual.Events.Add(new(Tag.Death, "5 May 1930", "Wales"));
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

    public static TestIndividual JaredVaughn
    {
        get
        {
            var individual = new TestIndividual("@I272721829058@", "Jared", "Vaughn");
            individual.Events.Add(new(Tag.Birth, "8 Aug 1960", "England"));
            individual.Events.Add(new(Tag.Death, "8 Aug 2020", "England"));
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


    public static TestIndividual LlewelynVaughn
    {
        get
        {
            var individual = new TestIndividual("@I272721827546@", "Llewelyn", "Vaughn");
            individual.Events.Add(new(Tag.Birth, "6 JUN 1900", "Wales"));
            individual.Events.Add(new(Tag.Death, "6 JUN 1960", "Wales"));
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