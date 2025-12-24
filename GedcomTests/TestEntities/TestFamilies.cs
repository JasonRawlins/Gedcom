using Gedcom;
using Gedcom.RecordStructures;

namespace GedcomTests.TestEntities;

public class TestFamily(string xref, TestIndividual husband, TestIndividual wife)
{
    public TestFamily(string xref, TestIndividual husband, TestIndividual wife, List<TestIndividual> children) : this(xref, husband, wife)
    {
        Children = children;
    }

    public List<TestIndividual> Children { get; set; } = [];
    public TestIndividual Husband { get; set; } = husband;
    public TestEvent? Marriage { get; set; }
    public TestIndividual Wife { get; set; } = wife;
    public string Xref { get; set; } = xref;
}

public class TestFamilies(Gedcom.Gedcom gedcom)
{
    private readonly Gedcom.Gedcom Gedcom = gedcom;

    // Unfortunately, family records exported from Ancestry.com do not have deterministic xrefs.
    // They will have to be retrieved at runtime. This may create concurrency issues when 
    // running tests. Those issues will have to be resolved when they pop up.
    public FamilyRecord GetFamilyRecord(TestIndividual husband, TestIndividual wife) => Gedcom.GetFamilyRecordByHusbandAndWife(husband.Xref, wife.Xref);

    public static TestFamily AnxinZhouAndMargaretDavis
    {
        get
        {
            var testFamilies = new TestFamilies(TestUtilities.CreateGedcom());
            var familyRecord = testFamilies.GetFamilyRecord(TestIndividuals.AnxinZhou, TestIndividuals.MargaretDavis);
            var family = new TestFamily(familyRecord.Xref, TestIndividuals.AnxinZhou, TestIndividuals.MargaretDavis, [TestIndividuals.XiaohuiZhou])
            {
                Marriage = new(Tag.Marriage, "7 Jul 1957", "")
            };
            return family;
        }
    }

    public static TestFamily CarwynDavisAndElizabethRhys
    {
        get
        {
            var testFamilies = new TestFamilies(TestUtilities.CreateGedcom());
            var familyRecord = testFamilies.GetFamilyRecord(TestIndividuals.CarwynDavis, TestIndividuals.ElizabethRhys);
            var family = new TestFamily(familyRecord.Xref, TestIndividuals.CarwynDavis, TestIndividuals.ElizabethRhys, [TestIndividuals.AnwenDavis])
            {
                Marriage = new(Tag.Marriage, "5 May 1895", "")
            };
            return family;
        }
    }

    public static TestFamily CelynDavisAndAbigailBrown
    {
        get
        {
            var testFamilies = new TestFamilies(TestUtilities.CreateGedcom());
            var familyRecord = testFamilies.GetFamilyRecord(TestIndividuals.CelynVaughn, TestIndividuals.AbigailBrown);
            var family = new TestFamily(familyRecord.Xref, TestIndividuals.CelynVaughn, TestIndividuals.AbigailBrown, [TestIndividuals.JaredVaughn])
            {
                Marriage = new(Tag.Marriage, "7 Jul 1955", "")
            };
            return family;
        }
    }

    public static TestFamily DylanDavisAndFionaDouglas
    {
        get
        {
            var testFamilies = new TestFamilies(TestUtilities.CreateGedcom());
            var familyRecord = testFamilies.GetFamilyRecord(TestIndividuals.DylanDavis, TestIndividuals.FionaDouglas);
            var family = new TestFamily(familyRecord.Xref, TestIndividuals.DylanDavis, TestIndividuals.FionaDouglas, [TestIndividuals.SaraDavis])
            {
                Marriage = new(Tag.Marriage, "7 Jul 1955", "")
            };
            return family;
        }
    }

    public static TestFamily JamesSmithAndSaraDavis
    {
        get
        {
            var testFamilies = new TestFamilies(TestUtilities.CreateGedcom());
            var familyRecord = testFamilies.GetFamilyRecord(TestIndividuals.JamesSmith, TestIndividuals.SaraDavis);
            var family = new TestFamily(familyRecord.Xref, TestIndividuals.JamesSmith, TestIndividuals.SaraDavis, [TestIndividuals.MarySmith])
            {
                Marriage = new(Tag.Marriage, "8 Aug 1985", "")
            };
            return family;
        }
    }

    public static TestFamily LlewelynVaughnAndAnwenDavis
    {
        get
        {
            var testFamilies = new TestFamilies(TestUtilities.CreateGedcom());
            var familyRecord = testFamilies.GetFamilyRecord(TestIndividuals.LlewelynVaughn, TestIndividuals.AnwenDavis);
            var family = new TestFamily(familyRecord.Xref, TestIndividuals.LlewelynVaughn, TestIndividuals.AnwenDavis, [TestIndividuals.CelynVaughn])
            {
                Marriage = new(Tag.Marriage, "8 Aug 1985", "")
            };
            return family;
        }
    }

    public static TestFamily OwenDavisAndGwenJones
    {
        get
        {
            var testFamilies = new TestFamilies(TestUtilities.CreateGedcom());
            var familyRecord = testFamilies.GetFamilyRecord(TestIndividuals.OwenDavis, TestIndividuals.GwenJones);
            var family = new TestFamily(familyRecord.Xref, TestIndividuals.OwenDavis, TestIndividuals.GwenJones, [TestIndividuals.DylanDavis])
            {
                Marriage = new(Tag.Marriage, "6 Jun 1925", "")
            };
            return family;
        }
    }
}