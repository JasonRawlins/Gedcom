using Gedcom;

namespace GedcomTests;

[TestClass]
public class GedcomLineTests
{

    [TestMethod]
    public void ParseEmptyLine()
    {
        var exception = Assert.ThrowsExactly<GedcomLineParseException>(() =>
        {
            GedcomLine.Parse("");
        });

        Assert.AreEqual("The Gedcom line is empty.", exception.Message);
    }
}
