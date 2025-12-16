using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

[TestClass]
public class RepositoryHtmlTests
{
    [TestMethod]
    public void ExportRepositoryHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var repositoryHtml = htmlGedcomWriter.GetRepository(TestTree.Repositories.VitalRecordsRepository.Xref);

        Assert.IsTrue(repositoryHtml.Contains(TestTree.Repositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void ExportRepositoriesHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var repositoriesHtml = htmlGedcomWriter.GetRepositories();

        Assert.IsTrue(repositoriesHtml.Contains(TestTree.Repositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void NonExistingRepositoryHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var repositoryHtml = htmlGedcomWriter.GetRepository(TestTree.InvalidXref);

        Assert.IsTrue(repositoryHtml.Equals(""));
    }

    [TestMethod]
    public void QueryRepositoriesHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);
        var repositoriesHtml = htmlGedcomWriter.GetRepositories(TestTree.Repositories.VitalRecordsRepository.Xref);

        Assert.IsTrue(repositoriesHtml.Contains(TestTree.Repositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void WriteRepositoriesHtmlTest()
    {
        // This is an integration test. Figure that out later
        var gedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.HTML);

        File.WriteAllText(TestUtilities.HtmlFullName, gedcomWriter.GetRepositories());
    }
}