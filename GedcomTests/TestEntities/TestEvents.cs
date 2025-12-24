namespace GedcomTests.TestEntities;

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