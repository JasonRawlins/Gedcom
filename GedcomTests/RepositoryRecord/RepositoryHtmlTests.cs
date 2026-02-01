using Gedcom.Core;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;

namespace GedcomTests.Repository;

// The use of the word "Repository" in this class refers to a Gedcom "Repository" (REPO) record,
// not its normal meaning related to source control.
[TestClass]
public class RepositoryHtmlTests
{
    [TestMethod]
    public void ExportRepositoryHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var repositoryHtml = htmlGedcomWriter.GetRepository(TestRepositories.VitalRecordsRepository.Xref);

        Assert.IsTrue(repositoryHtml.Contains(TestRepositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void ExportRepositoriesHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var repositoriesHtml = htmlGedcomWriter.GetRepositories();

        Assert.IsTrue(repositoriesHtml.Contains(TestRepositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void NonExistingRepositoryHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var repositoryHtml = htmlGedcomWriter.GetRepository(TestConstants.InvalidXref);

        Assert.IsTrue(repositoryHtml.Equals(""));
    }

    [TestMethod]
    public void QueryRepositoriesHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var repositoriesHtml = htmlGedcomWriter.GetRepositories(TestRepositories.VitalRecordsRepository.Xref);

        Assert.IsTrue(repositoriesHtml.Contains(TestRepositories.VitalRecordsRepository.Xref));
    }

    //[TestMethod]
    public void WriteRepositoriesHtmlTest()
    {
        // This is an integration test. Figure that out later
        var gedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);

        File.WriteAllText(TestUtilities.HtmlFullName, gedcomWriter.GetRepositories());
    }
}