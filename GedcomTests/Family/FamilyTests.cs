namespace GedcomTests.Family;

[TestClass]
public class FamilyTests
{
    [TestMethod]
    public void FamilyTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var relationshipManager = new Gedcom.ReltionshipManager(gedcom);

        var family = relationshipManager.GetFamily(TestTree.Families.JamesSmithAndSaraDavis.Xref);

        Assert.AreEqual(TestTree.Individuals.JamesSmith.Xref, family.Husband!.Xref);
        Assert.AreEqual(TestTree.Individuals.SaraDavis.Xref, family.Wife!.Xref);
        Assert.AreEqual(TestTree.Individuals.MarySmith.Xref, family.Children.First().Xref);
    }

    [TestMethod]
    public void FamilyWithParentsTest()
    {
        var gedcom = TestUtilities.CreateGedcom();
        var relationshipManager = new Gedcom.ReltionshipManager(gedcom);

        var family = relationshipManager.GetFamily(TestTree.Families.JamesSmithAndSaraDavis.Xref);

        Assert.AreEqual(TestTree.Individuals.JamesSmith.Xref, family.Husband!.Xref);
        Assert.AreEqual(TestTree.Individuals.SaraDavis.Xref, family.Wife!.Xref);
        Assert.AreEqual(TestTree.Individuals.MarySmith.Xref, family.Children.First().Xref);

        // TODO: Find parents
    }
}
