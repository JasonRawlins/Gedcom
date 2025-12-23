namespace GedcomTests.Family;

[TestClass]
public class FamilyTests
{
    [TestMethod]
    public void FamilyTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);

        var family = familyManager.CreateFamily(TestTree.Families.JamesSmithAndSaraDavis.Xref, 0, 0);

        // Husband and wife should be present.
        Assert.AreEqual(TestTree.Individuals.JamesSmith.Xref, family.Husband!.Xref);
        Assert.AreEqual(TestTree.Individuals.SaraDavis.Xref, family.Wife!.Xref);

        // Children should not be present.
        Assert.IsTrue(family.Children.Count == 0);
    }

    [TestMethod]
    public void FamilyWithParentsTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);

        var family = familyManager.CreateFamily(TestTree.Families.JamesSmithAndSaraDavis.Xref, 1, 0);

        // Couple should be present.
        Assert.AreEqual(TestTree.Individuals.JamesSmith.Xref, family.Husband!.Xref);
        Assert.AreEqual(TestTree.Individuals.SaraDavis.Xref, family.Wife!.Xref);

        // Wife's parents should be present
        Assert.AreEqual(TestTree.Individuals.DylanDavis.Xref, family.Wife!.Parents!.Husband!.Xref);
        Assert.AreEqual(TestTree.Individuals.FionaDouglas.Xref, family.Wife!.Parents!.Wife!.Xref);

        // Children should not be present.
        Assert.AreEqual(family.Children.Count, 0);
    }

    [TestMethod]
    public void FamilyWithGrandparentsTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);

        var family = familyManager.CreateFamily(TestTree.Families.JamesSmithAndSaraDavis.Xref, 2, 0);

        // Couple should be present.
        Assert.AreEqual(TestTree.Individuals.JamesSmith.Xref, family.Husband!.Xref);
        Assert.AreEqual(TestTree.Individuals.SaraDavis.Xref, family.Wife!.Xref);

        // Grandparents should be present.
        var dylanDavis = family.Wife.Parents!.Husband;
        Assert.AreEqual(TestTree.Individuals.OwenDavis.Xref, dylanDavis!.Parents!.Husband!.Xref);
        Assert.AreEqual(TestTree.Individuals.GwenJones.Xref, dylanDavis!.Parents!.Wife!.Xref);

        // Children should not be present.
        Assert.AreEqual(family.Children.Count, 0);
    }

    [TestMethod]
    public void FamilyWithChildrenTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);

        var family = familyManager.CreateFamily(TestTree.Families.OwenDavisAndGwenJones.Xref, 0, 1);

        // Couple should be present.
        Assert.AreEqual(TestTree.Individuals.OwenDavis.Xref, family.Husband!.Xref);
        Assert.AreEqual(TestTree.Individuals.GwenJones.Xref, family.Wife!.Xref);

        // Parents should not be present.
        Assert.IsNull(family.Husband!.Parents);
        Assert.IsNull(family.Wife!.Parents);

        // Child should be present.
        var dylanDavis = family.Children.FirstOrDefault(c => c.Xref == TestTree.Individuals.DylanDavis.Xref);
        Assert.IsNotNull(dylanDavis);
    }

    [TestMethod]
    public void FamilyWithGrandChildrenTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);

        var family = familyManager.CreateFamily(TestTree.Families.OwenDavisAndGwenJones.Xref, 0, 2);

        // Couple should be present.
        Assert.AreEqual(TestTree.Individuals.OwenDavis.Xref, family.Husband!.Xref);
        Assert.AreEqual(TestTree.Individuals.GwenJones.Xref, family.Wife!.Xref);

        // Parents should not be present.
        Assert.IsNull(family.Husband!.Parents);
        Assert.IsNull(family.Wife!.Parents);

        // Grandchild should be present.
        var dylanDavis = family.Children.First(c => c.Xref == TestTree.Individuals.DylanDavis.Xref);
        Assert.IsNotNull(dylanDavis.Children.FirstOrDefault(c => c.Xref == TestTree.Individuals.SaraDavis.Xref));
    }

    [TestMethod]
    public void FamilyWithAuntsAndUnclesTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var familyManager = new Gedcom.FamilyManager(gedcom);

        var family = familyManager.CreateFamily(TestTree.Families.JamesSmithAndSaraDavis.Xref, 1, 0);

        // Couple should be present.
        Assert.AreEqual(TestTree.Individuals.JamesSmith.Xref, family.Husband!.Xref);
        Assert.AreEqual(TestTree.Individuals.SaraDavis.Xref, family.Wife!.Xref);

        // Wife's parents should be present
        var parentsFamily = family.Wife!.Parents;
        Assert.AreEqual(TestTree.Individuals.DylanDavis.Xref, parentsFamily!.Husband!.Xref);
        Assert.AreEqual(TestTree.Individuals.FionaDouglas.Xref, parentsFamily!.Wife!.Xref);

        familyManager.LoadSiblings(parentsFamily.Husband);

        // Aunts and uncles should be present
        var auntMargaretDavis = parentsFamily.Husband.Siblings.SingleOrDefault(s => s.Xref == TestTree.Individuals.MargaretDavis.Xref);
        var uncleGarethDavis = parentsFamily.Husband.Siblings.SingleOrDefault(s => s.Xref == TestTree.Individuals.GarethDavis.Xref);

        Assert.IsNotNull(auntMargaretDavis);
        Assert.IsNotNull(uncleGarethDavis);
    }
}
