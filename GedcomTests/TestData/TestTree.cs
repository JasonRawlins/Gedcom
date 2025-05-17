namespace GedcomTests.TestData;

internal static class TestTree
{
    public static class Individuals
    {
        public static TestIndividual RobertDavis = new TestIndividual("@I412679332995@", "Robert", "Davis");
        public static TestIndividual RosaGarcia = new TestIndividual("@I412679333070@", "Rosa", "Garcia");
        public static TestIndividual MariaDavis = new TestIndividual("@I412679332996@", "Maria", "Davis");
        public static TestIndividual DylanLewis = new TestIndividual("@I412679333361@", "Dylan", "Lewis");
        public static TestIndividual GwenLewis = new TestIndividual("@I412679333479@", "Gwen", "Lewis");
        public static TestIndividual MateoDavis = new TestIndividual("@I412679333252@", "Mateo", "Davis");
    }

    public static class Families
    {
        public static TestFamily RobertAndRosaDavis = new TestFamily("@F1@", Individuals.RobertDavis, Individuals.RosaGarcia, new List<TestIndividual> { Individuals.MariaDavis, Individuals.MateoDavis });
        public static TestFamily DylanAndMariaLewis = new TestFamily("@F2@", Individuals.DylanLewis, Individuals.MariaDavis, new List<TestIndividual> { Individuals.GwenLewis });
    }

    public static class Repositories
    {
        public static TestRepository GarciaFamilyBookOfRemembrance = new TestRepository("@R1555932347@", "Garcia family book of remembrance");
        public static TestRepository FamilySearchLibrary = new TestRepository("@R1555932401@", "FamilySearch Library");
    }

    public static class Sources
    {
        public static TestSource GarciaFamilyBirths = new TestSource("@S1670740985@", "Garcia family births");
        public static TestSource AutobiographyOfRobertDavis = new TestSource("@S1670741627@", "Autobiography of Robert Davis");
    }
}

internal class TestIndividual
{
    public TestIndividual(string xref, string given, string surname)
    {
        Xref = xref;
        Given = given;
        Surname = surname;
    }

    public string Xref { get; set; }
    public string Given { get; set; }
    public string Surname { get; set; }
}

internal class TestFamily
{
    public TestFamily(string xref, TestIndividual husband, TestIndividual wife) : this(xref, husband, wife, new List<TestIndividual>())
    {
    }

    public TestFamily(string xref, TestIndividual husband, TestIndividual wife, List<TestIndividual> children)
    {
        Xref = xref;
        Husband = husband;
        Wife = wife;
        Children = children;
    }
    
    public string Xref { get; set; }
    public TestIndividual Husband { get; set; }
    public TestIndividual Wife { get; set; }
    public List<TestIndividual> Children { get; set; }
}

public class TestRepository
{
    public TestRepository(string xref, string name)
    {
        Xref = xref;
        Name = name;
    }

    public string Xref { get; set; }
    public string Name { get; set; }
}

public class TestSource
{
    public TestSource(string xref, string title)
    {
        Xref = xref;
        Title = title;
    }

    public string Xref { get; set; }
    public string Title { get; set; }
}