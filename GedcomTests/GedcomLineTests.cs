using Gedcom;

namespace GedcomTests;

[TestClass]
public class GedcomLineTests
{

    [TestMethod]
    public void ParseValidHeaderLine()
    {
        var gedcomLine = GedcomLine.Parse("0 HEAD");

        Assert.AreEqual(0, gedcomLine.Level);
        Assert.AreEqual(Tag.Header, gedcomLine.Tag);
    }

    [TestMethod]
    public void ParseValidLevel0RecordLine()
    {
        var gedcomLine = GedcomLine.Parse("0 @I1@ INDI");

        Assert.AreEqual(0, gedcomLine.Level);
        Assert.AreEqual(Tag.Individual, gedcomLine.Tag);
        Assert.AreEqual("@I1@", gedcomLine.Xref);
    }

    [TestMethod]
    public void ParseValidNonLevel0RecordWithoutValue()
    {
        var gedcomLine = GedcomLine.Parse("1 BIRT");

        Assert.AreEqual(1, gedcomLine.Level);
        Assert.AreEqual(Tag.Birth, gedcomLine.Tag);
        Assert.AreEqual("", gedcomLine.Value);
    }

    [TestMethod]
    public void ParseValidNonLevel0RecordWithValue()
    {
        var gedcomLine = GedcomLine.Parse("1 GIVN Jane");

        Assert.AreEqual(1, gedcomLine.Level);
        Assert.AreEqual(Tag.GivenName, gedcomLine.Tag);
        Assert.AreEqual("Jane", gedcomLine.Value);
    }

    [TestMethod]
    public void ParseValidNonLevel0RecordWithValueThatHasSpacesInIt()
    {
        var gedcomLine = GedcomLine.Parse("1 GIVN Jane /Miller/");

        Assert.AreEqual(1, gedcomLine.Level);
        Assert.AreEqual(Tag.GivenName, gedcomLine.Tag);
        Assert.AreEqual("Jane", gedcomLine.Value);
    }

    [TestMethod]
    public void ParseEmptyLine()
    {
        var exception = Assert.ThrowsExactly<GedcomLineParseException>(() =>
        {
            GedcomLine.Parse("");
        });

        Assert.AreEqual("The line is empty.", exception.Message);
    }

    [TestMethod]
    public void ParseLineLongerThan256Characters()
    {
        var exception = Assert.ThrowsExactly<GedcomLineParseException>(() =>
        {
            GedcomLine.Parse("1 NOTE Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus non volutpat sem. Vestibulum pulvinar erat ut dolor hendrerit, aliquam pharetra magna pulvinar. Integer in orci volutpat libero elementum lobortis. Etiam ut elit sem. Donec ut eros purus. Vestibulum iaculis velit placerat.");
        });

        Assert.AreEqual("The line is longer than 256 characters. Actual length was 294.", exception.Message);
    }

    [TestMethod]
    public void ParseLineWithLeadingWhitespace()
    {
        var exception = Assert.ThrowsExactly<GedcomLineParseException>(() =>
        {
            GedcomLine.Parse(" 0 @I1@ INDI");
        });

        Assert.AreEqual("The line has leading whitespace.", exception.Message);
    }

    [TestMethod]
    public void ParseLineWithNoDataAfterLevel()
    {
        var exception = Assert.ThrowsExactly<GedcomLineParseException>(() =>
        {
            GedcomLine.Parse("0");
        });

        Assert.AreEqual("The line is missing information after the level.", exception.Message);
    }

    [TestMethod]
    public void ParseLineWithNonNumberLevel()
    {
        var exception = Assert.ThrowsExactly<GedcomLineParseException>(() =>
        {
            GedcomLine.Parse("FAIL GIVN Jane");
        });

        Assert.AreEqual("Level must be an integer. Actual value was FAIL.", exception.Message);
    }

    [TestMethod]
    public void ParseLineWithANegativeLevel()
    {
        var exception = Assert.ThrowsExactly<GedcomLineParseException>(() =>
        {
            GedcomLine.Parse("-1 GIVN Jane");
        });

        Assert.AreEqual("Level must be between 0 and 99. Actual value was -1.", exception.Message);
    }

    [TestMethod]
    public void ParseLineWithALevelHigherThan99()
    {
        var exception = Assert.ThrowsExactly<GedcomLineParseException>(() =>
        {
            GedcomLine.Parse("100 GIVN Jane");
        });

        Assert.AreEqual("Level must be between 0 and 99. Actual value was 100.", exception.Message);
    }

    [TestMethod]
    public void ParseLineWithALevel0RecordWithoutAValueThatIsNotAHeaderOrTrailer()
    {
        var exception = Assert.ThrowsExactly<GedcomLineParseException>(() =>
        {
            GedcomLine.Parse("0 @I1@");
        });

        Assert.AreEqual("A level 0 record with only two parts must be the HEAD or TRLR tag.", exception.Message);
    }
}

