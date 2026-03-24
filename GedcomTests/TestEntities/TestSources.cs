namespace GedcomTests.TestEntities;

public class TestSource(string xref, string title)
{
    public string Xref { get; set; } = xref;
    public string Title { get; set; } = title;
}

public class TestSources
{
    public static TestSource VitalRecords
    {
        get
        {
            return new("@S976697667@", "Vital records");
        }
    }

    public static List<TestSource> All
    {
        get
        {
            return [VitalRecords];
        }
    }
}

