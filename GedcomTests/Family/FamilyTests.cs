using Gedcom;
using GedcomTests.TestEntities;

namespace GedcomTests.Family;

// See Gedcom/ProjectResources/NGS-Family-Relationship-Chart.pdf for an explanation of family reltionships.
[TestClass]
public class FamilyTests
{
    [TestMethod]
    public void FamilyCoupleTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var jamesAndSaraFamily = familyManager.CreateFamily(TestFamilies.JamesSmithAndSaraDavis.Xref, Generation.Current, Generation.Current);

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
        var saraFamily = familyManager.CreateFamily(TestFamilies.JamesSmithAndSaraDavis.Xref, Generation.Parent, Generation.Current);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.JamesSmith.Xref, saraFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.SaraDavis.Xref, saraFamily.Wife!.Xref);

        var fatherDylan = saraFamily.Wife!.Parents!.Husband!;
        var motherFiona = saraFamily.Wife!.Parents!.Wife!;

        // Wife's parents should be present
        Assert.AreEqual(TestIndividuals.DylanDavis.Xref, fatherDylan.Xref);
        Assert.AreEqual(TestIndividuals.FionaDouglas.Xref, motherFiona.Xref);

        // Children should not be present.
        Assert.AreEqual(0, saraFamily.Children.Count);
    }

    [TestMethod]
    public void FamilyWithGrandparentsTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var saraFamily = familyManager.CreateFamily(TestFamilies.JamesSmithAndSaraDavis.Xref, Generation.Grandparent, Generation.Current);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.JamesSmith.Xref, saraFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.SaraDavis.Xref, saraFamily.Wife!.Xref);

        var fatherDylan = saraFamily.Wife.Parents!.Husband!;
        var grandfatherOwen = fatherDylan.Parents!.Husband!;
        var grandMotherGwen = fatherDylan.Parents!.Wife!;

        // Grandparents should be present.
        Assert.AreEqual(TestIndividuals.OwenDavis.Xref, grandfatherOwen.Xref);
        Assert.AreEqual(TestIndividuals.GwenJones.Xref, grandMotherGwen.Xref);

        // Children should not be present.
        Assert.AreEqual(0, saraFamily.Children.Count);
    }

    [TestMethod]
    public void FamilyWithChildrenTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var owenAndGwenFamily = familyManager.CreateFamily(TestFamilies.OwenDavisAndGwenJones.Xref, Generation.Current, Generation.Child);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.OwenDavis.Xref, owenAndGwenFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.GwenJones.Xref, owenAndGwenFamily.Wife!.Xref);

        // Parents should not be present.
        Assert.IsNull(owenAndGwenFamily.Husband!.Parents);
        Assert.IsNull(owenAndGwenFamily.Wife!.Parents);

        var sonDylan = owenAndGwenFamily.Children.Single(c => c.Xref == TestIndividuals.DylanDavis.Xref);
        
        // Child should be present.
        Assert.IsNotNull(sonDylan);
    }

    [TestMethod]
    public void FamilyWithGrandChildrenTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var owenAndGwenFamily = familyManager.CreateFamily(TestFamilies.OwenDavisAndGwenJones.Xref, Generation.Current, Generation.Grandchild);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.OwenDavis.Xref, owenAndGwenFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.GwenJones.Xref, owenAndGwenFamily.Wife!.Xref);

        // Parents should not be present.
        Assert.IsNull(owenAndGwenFamily.Husband!.Parents);
        Assert.IsNull(owenAndGwenFamily.Wife!.Parents);

        var sonDylan = owenAndGwenFamily.Children.Single(c => c.Xref == TestIndividuals.DylanDavis.Xref);
        var granddaughterSara = sonDylan.Children.Single(c => c.Xref == TestIndividuals.SaraDavis.Xref);
        
        // Grandchild should be present.
        Assert.IsNotNull(granddaughterSara);
    }

    [TestMethod]
    public void FamilyWithAuntsAndUnclesTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var saraFamily = familyManager.CreateFamily(TestFamilies.JamesSmithAndSaraDavis.Xref, Generation.Parent, Generation.Current);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.JamesSmith.Xref, saraFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.SaraDavis.Xref, saraFamily.Wife!.Xref);

        var fatherDylan = saraFamily.Wife!.Parents!.Husband!;
        var motherFiona = saraFamily.Wife!.Parents!.Wife!;

        // Wife's parents should be present
        Assert.AreEqual(TestIndividuals.DylanDavis.Xref, fatherDylan.Xref);
        Assert.AreEqual(TestIndividuals.FionaDouglas.Xref, motherFiona.Xref);

        familyManager.LoadSiblings(fatherDylan);

        var auntMargaret = fatherDylan.Siblings.Single(s => s.Xref == TestIndividuals.MargaretDavis.Xref);
        var uncleGareth = fatherDylan.Siblings.Single(s => s.Xref == TestIndividuals.GarethDavis.Xref);
        
        // Aunts and uncles should be present
        Assert.IsNotNull(auntMargaret);
        Assert.IsNotNull(uncleGareth);
    }

    [TestMethod]
    public void FamilyWithFirstCousinsTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var saraFamily = familyManager.CreateFamily(TestFamilies.JamesSmithAndSaraDavis.Xref, Generation.Parent, Generation.Current);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.JamesSmith.Xref, saraFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.SaraDavis.Xref, saraFamily.Wife!.Xref);

        // Father should be present.
        var fatherDylan = saraFamily.Wife!.Parents!.Husband!;
        familyManager.LoadSiblings(fatherDylan);

        // Aunt should be present.
        var auntMargaret = fatherDylan.Siblings.Single(s => s.Xref == TestIndividuals.MargaretDavis.Xref);
        familyManager.LoadDescendants(auntMargaret, Generation.Child);

        var firstCousinXiaohui = auntMargaret.Children.FirstOrDefault(c => c.Xref == TestIndividuals.XiaohuiZhou.Xref);

        // First cousin should be present.
        Assert.IsNotNull(firstCousinXiaohui);
    }

    [TestMethod]
    public void FamilyWithSecondCousinsTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);
        var saraFamily = familyManager.CreateFamily(TestFamilies.JamesSmithAndSaraDavis.Xref, Generation.G_Grandparent, Generation.Current);

        // Couple should be present.
        Assert.AreEqual(TestIndividuals.JamesSmith.Xref, saraFamily.Husband!.Xref);
        Assert.AreEqual(TestIndividuals.SaraDavis.Xref, saraFamily.Wife!.Xref);

        var sara = saraFamily.Wife!;
        var fatherDylan = sara.Parents!.Husband!;
        var grandfatherOwen = fatherDylan.Parents!.Husband!;
        var greatGrandfatherCarwyn = grandfatherOwen.Parents!.Husband!;
        var greatGrandmotherElizabeth = grandfatherOwen.Parents!.Wife!;

        // Great grandparents should be present.
        Assert.AreEqual(TestIndividuals.CarwynDavis.Xref, greatGrandfatherCarwyn.Xref);
        Assert.AreEqual(TestIndividuals.ElizabethRhys.Xref, greatGrandmotherElizabeth.Xref);

        familyManager.LoadDescendants(grandfatherOwen.Parents, Generation.G_Grandchild);

        var greatGrandparentsFamily = grandfatherOwen.Parents;
        var daughterAnwen = greatGrandparentsFamily.Children.Single(c => c.Xref == TestIndividuals.AnwenDavis.Xref);
        var grandsonCelyn = daughterAnwen.Children.Single(c => c.Xref == TestIndividuals.CelynVaughn.Xref);
        var secondCousinJared = grandsonCelyn.Children.Single(c => c.Xref == TestIndividuals.JaredVaughn.Xref);

        // Second cousin should be present.
        Assert.IsNotNull(secondCousinJared);
    }
}
