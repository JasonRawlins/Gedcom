using Gedcom;

namespace GedcomTests;

[TestClass]
public class GedcomFormatTests
{
    [TestMethod]
    public void CreateGedcomWithMissingHeader()
    {
        var gedcomLines = new List<GedcomLine>
        {
            new(0, Tag.Individual, "@I1@"),
            new(0, Tag.Individual, "@I2@"),
            new(0, Tag.Trailer),
        };

        var exception = Assert.ThrowsExactly<GedcomFormatException>(() =>
        {
            var gedcom = new Gedcom.Core.Gedcom(gedcomLines);
        });

        Assert.AreEqual($"The first tag in a GEDCOM file must be HEAD. The actual tag was {exception.GedcomLine.Tag}.", exception.Message);
    }

    [TestMethod]
    public void CreateGedcomWithMissingTrailer()
    {
        var gedcomLines = new List<GedcomLine>
        {
            new(0, Tag.Header),
            new(0, Tag.Individual, "@I1@"),
            new(0, Tag.Individual, "@I2@")
        };

        var exception = Assert.ThrowsExactly<GedcomFormatException>(() =>
        {
            var gedcom = new Gedcom.Core.Gedcom(gedcomLines);
        });

        Assert.AreEqual($"The last tag in a GEDCOM file must be TRLR. The actual tag was {exception.GedcomLine.Tag}.", exception.Message);
    }

    [TestMethod]
    public void CreateGedcomWithInsufficientLines()
    {
        var gedcomLines = new List<GedcomLine>
        {
            new(0, Tag.Header),
            new(0, Tag.Trailer)
        };

        var exception = Assert.ThrowsExactly<GedcomFormatException>(() =>
        {
            var gedcom = new Gedcom.Core.Gedcom(gedcomLines);
        });

        Assert.AreEqual("A valid GEDCOM file must have at least three tags: HEAD, any other tags, and a TRLR.", exception.Message);
    }
}

