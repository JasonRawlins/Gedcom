namespace GedcomTests.TestEntities;

// TODO: 2026-03-24. Do we need to test for citations?
public class TestCitation(string sourceXref, string page)
{
    public string SourceXref { get; set; } = sourceXref;
    public string Page { get; set; } = page;
}