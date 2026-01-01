using GedcomTests.TestEntities;

namespace GedcomTests.Family;

[TestClass]
public class FamilyRecordTests
{
    [TestMethod]
    public void MarriageAndDivorceTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var dylanAndEithneFamily = TestFamilies.DylanDavisAndEithneLynch;
        
        var familyRecord = gedcom.GetFamilyRecord(dylanAndEithneFamily.Xref);

        // The marriage and divorce dates should be correct.
        Assert.AreEqual("7 Jul 1950", familyRecord.Marriage.GedcomDate.DayMonthYear);
        Assert.AreEqual("7 Jul 1953", familyRecord.Divorce.GedcomDate.DayMonthYear);

        // The marriage and divorce should also be in the EventDetails list.
        Assert.IsNotNull(familyRecord.EventDetails.Single(ed => ed.GedcomDate.DayMonthYear.Equals("7 Jul 1950")));
        Assert.IsNotNull(familyRecord.EventDetails.Single(ed => ed.GedcomDate.DayMonthYear.Equals("7 Jul 1953")));
    }
}

