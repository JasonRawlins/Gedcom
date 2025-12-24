using GedcomTests.TestEntities;

namespace GedcomTests.Family;

[TestClass]
public class FamilyTests
{
    [TestMethod]
    public void FamilyTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);

        var jamesAndSaraFamily = familyManager.CreateFamily(TestFamilies.JamesSmithAndSaraDavis.Xref, 0, 0);

        // Husband and wife should be present.
        Assert.AreEqual(TestIndividuals.JamesSmith.Xref, jamesAndSaraFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.SaraDavis.Xref, jamesAndSaraFamily.Wife!.Xref);

        // Children should not be present.
        Assert.AreEqual(0, jamesAndSaraFamily.Children.Count);
    }

    [TestMethod]
    public void FamilyWithParentsTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var saraFamily = familyManager.CreateFamily(TestFamilies.JamesSmithAndSaraDavis.Xref, 1, 0);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.JamesSmith.Xref, saraFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.SaraDavis.Xref, saraFamily.Wife!.Xref);

        var fatherDylanDavis = saraFamily.Wife!.Parents!.Husband!;
        var motherFionaDouglas = saraFamily.Wife!.Parents!.Wife!;

        // Wife's parents should be present
        Assert.AreEqual(TestIndividuals.DylanDavis.Xref, fatherDylanDavis.Xref);
        Assert.AreEqual(TestIndividuals.FionaDouglas.Xref, motherFionaDouglas.Xref);

        // Children should not be present.
        Assert.AreEqual(0, saraFamily.Children.Count);
    }

    [TestMethod]
    public void FamilyWithGrandparentsTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var saraFamily = familyManager.CreateFamily(TestFamilies.JamesSmithAndSaraDavis.Xref, 2, 0);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.JamesSmith.Xref, saraFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.SaraDavis.Xref, saraFamily.Wife!.Xref);

        var fatherDylanDavis = saraFamily.Wife.Parents!.Husband!;
        var grandfatherOwenDavis = fatherDylanDavis.Parents!.Husband!;
        var grandMotherGwenJones = fatherDylanDavis.Parents!.Wife!;

        // Grandparents should be present.
        Assert.AreEqual(TestIndividuals.OwenDavis.Xref, grandfatherOwenDavis.Xref);
        Assert.AreEqual(TestIndividuals.GwenJones.Xref, grandMotherGwenJones.Xref);

        // Children should not be present.
        Assert.AreEqual(0, saraFamily.Children.Count);
    }

    [TestMethod]
    public void FamilyWithChildrenTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var owenAndGwenFamily = familyManager.CreateFamily(TestFamilies.OwenDavisAndGwenJones.Xref, 0, 1);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.OwenDavis.Xref, owenAndGwenFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.GwenJones.Xref, owenAndGwenFamily.Wife!.Xref);

        // Parents should not be present.
        Assert.IsNull(owenAndGwenFamily.Husband!.Parents);
        Assert.IsNull(owenAndGwenFamily.Wife!.Parents);

        // Child should be present.
        var sonDylanDavis = owenAndGwenFamily.Children.FirstOrDefault(c => c.Xref == TestIndividuals.DylanDavis.Xref);
        Assert.IsNotNull(sonDylanDavis);
    }

    [TestMethod]
    public void FamilyWithGrandChildrenTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var family = familyManager.CreateFamily(TestFamilies.OwenDavisAndGwenJones.Xref, 0, 2);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.OwenDavis.Xref, family.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.GwenJones.Xref, family.Wife!.Xref);

        // Parents should not be present.
        Assert.IsNull(family.Husband!.Parents);
        Assert.IsNull(family.Wife!.Parents);

        // Grandchild should be present.
        var dylanDavis = family.Children.First(c => c.Xref == TestIndividuals.DylanDavis.Xref);
        Assert.IsNotNull(dylanDavis.Children.FirstOrDefault(c => c.Xref == TestIndividuals.SaraDavis.Xref));
    }

    [TestMethod]
    public void FamilyWithAuntsAndUnclesTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var saraFamily = familyManager.CreateFamily(TestFamilies.JamesSmithAndSaraDavis.Xref, 1, 0);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.JamesSmith.Xref, saraFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.SaraDavis.Xref, saraFamily.Wife!.Xref);

        // Wife's parents should be present
        var fatherDylandDavis = saraFamily.Wife!.Parents!.Husband!;
        var motherFionaDouglas = saraFamily.Wife!.Parents!.Wife!;
        Assert.AreEqual(TestIndividuals.DylanDavis.Xref, fatherDylandDavis.Xref);
        Assert.AreEqual(TestIndividuals.FionaDouglas.Xref, motherFionaDouglas.Xref);

        familyManager.LoadSiblings(fatherDylandDavis);

        // Aunts and uncles should be present
        var auntMargaretDavis = fatherDylandDavis.Siblings.SingleOrDefault(s => s.Xref == TestIndividuals.MargaretDavis.Xref);
        var uncleGarethDavis = fatherDylandDavis.Siblings.SingleOrDefault(s => s.Xref == TestIndividuals.GarethDavis.Xref);

        Assert.IsNotNull(auntMargaretDavis);
        Assert.IsNotNull(uncleGarethDavis);
    }

    [TestMethod]
    public void FamilyWithCousinsTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var saraDavisFamily = familyManager.CreateFamily(TestFamilies.JamesSmithAndSaraDavis.Xref, 1, 0);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.JamesSmith.Xref, saraDavisFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.SaraDavis.Xref, saraDavisFamily.Wife!.Xref);

        // Father should be present.
        var fatherDylanDavis = saraDavisFamily.Wife!.Parents!.Husband!;
        familyManager.LoadSiblings(fatherDylanDavis);

        // Aunt should be present.
        var auntMargaretDavis = fatherDylanDavis.Siblings.Single(s => s.Xref == TestIndividuals.MargaretDavis.Xref);
        familyManager.LoadDescendants(auntMargaretDavis, 1);

        // First cousin should be present.
        var cousinXiaohuiZhou = auntMargaretDavis.Children.FirstOrDefault(c => c.Xref == TestIndividuals.XiaohuiZhou.Xref);

        Assert.IsNotNull(cousinXiaohuiZhou);
    }
}
