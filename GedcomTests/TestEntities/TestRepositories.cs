namespace GedcomTests.TestEntities;

public class TestRepository(string xref, string name)
{
    public string Name { get; set; } = name;
    public string Xref { get; set; } = xref;
}

public class TestRepositories
{
    public static TestRepository VitalRecordsRepository
    {
        get
        {
            return new TestRepository("@R856097590@", "Vital records repository");
        }
    }
}
