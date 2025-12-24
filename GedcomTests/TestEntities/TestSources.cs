namespace GedcomTests.TestEntities;

public class TestSource(string xref, string title)
{
    public string Xref { get; set; } = xref;
    public string Title { get; set; } = title;
}

public static class TestSources
{
    public static TestSource VitalRecords = new("@S976697667@", "Vital records");
}

