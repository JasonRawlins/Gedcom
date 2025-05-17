using Gedcom;
using System.Text;

namespace GedcomTests;

[TestClass]
public sealed class GedcomTests
{
    private static Gedcom.Gedcom Gedcom { get; set; }

    static GedcomTests()
    {
        var gedFileLines = Encoding.UTF8.GetString(Properties.Resources.DeveloperTree).Split(Environment.NewLine);
        var gedcomLines = gedFileLines.Select(GedcomLine.Parse).ToList();
        Gedcom = new Gedcom.Gedcom(gedcomLines);
    }

    [TestMethod]
    public void ExportGedcomAsJsonTest()
    {
        var options = new Options
        {
            RecordType = C.GEDC,
        };

        var exporter = new Exporter(Gedcom, options);
        var gedcomJson = exporter.GedcomJson();

        Assert.IsTrue(
            gedcomJson.Contains("@I1@")
            && gedcomJson.Contains("@I2@")
            && gedcomJson.Contains("@I3@")
            && gedcomJson.Contains("@S1@")
            && gedcomJson.Contains("@F1@")
            && gedcomJson.Contains("@R1@"));
    }

    [TestMethod]
    public void ExportIndividualsJsonTest()
    {
        var options = new Options
        {
            RecordType = C.INDI
        };

        var exporter = new Exporter(Gedcom, options);
        var individualsJson = exporter.IndividualRecordsJson();

        Assert.IsTrue(
            individualsJson.Contains("@I1@")
            && individualsJson.Contains("@I2@")
            && individualsJson.Contains("@I3@"));
    }
}
