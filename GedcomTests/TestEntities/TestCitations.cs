namespace GedcomTests.TestEntities;

public class TestCitation(string sourceXref, string page)
{
    public string SourceXref { get; set; } = sourceXref;
    public string Page { get; set; } = page;
}