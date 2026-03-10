using GedcomTests.TestEntities;

namespace GedcomTests.Sources;

[TestClass]
public class SourceCitationsTests
{
    [TestMethod]
    public void MarriageAndDivorceTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var dylanDavis = gedcom.GetIndividualRecords().Single(r => r.Xref == TestIndividuals.DylanDavis.Xref);

        var fionaDouglasMarriageCertificate = dylanDavis.SourceCitations.SingleOrDefault(sc => sc.Xref == "@S976697667@");
        var eithneLynchMarriageCertificate = dylanDavis.SourceCitations.SingleOrDefault(sc => sc.Xref == "@S977046020@");
        var eithneLynchDivorceCertificate = dylanDavis.SourceCitations.SingleOrDefault(sc => sc.Xref == "@S977046024@");
        var eithneLynchWeddingAnnouncement = dylanDavis.SourceCitations.SingleOrDefault(sc => sc.Xref == "@S977046068@");
        var residenceOnCarolineStreet = dylanDavis.SourceCitations.SingleOrDefault(sc => sc.Xref == "@S977151969@");
        var historyDegree = dylanDavis.SourceCitations.SingleOrDefault(sc => sc.Xref == "@S977151981@");
        var deathAnnouncement = dylanDavis.SourceCitations.SingleOrDefault(sc => sc.Xref == "@S977151991@");
        var residenceAtDeath = dylanDavis.SourceCitations.SingleOrDefault(sc => sc.Xref == "@S977152005@");
        var electionResults = dylanDavis.SourceCitations.SingleOrDefault(sc => sc.Xref == "@S977152012@");

        Assert.IsNotNull(fionaDouglasMarriageCertificate);
        Assert.IsNotNull(eithneLynchMarriageCertificate);
        Assert.IsNotNull(eithneLynchDivorceCertificate);
        Assert.IsNotNull(eithneLynchWeddingAnnouncement);
        Assert.IsNotNull(residenceOnCarolineStreet);
        Assert.IsNotNull(historyDegree);
        Assert.IsNotNull(deathAnnouncement);
        Assert.IsNotNull(residenceAtDeath);
        Assert.IsNotNull(electionResults);

    }
}

