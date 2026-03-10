using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Repositories;

// The use of the word "Repository" in this class refers to a Gedcom "Repository" (REPO) record,
// not its normal meaning related to source control.
[TestClass]
public class RepositoryHtmlTests
{
    [TestMethod]
    public void ExportRepositoryHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var repositoryHtml = Encoding.UTF8.GetString(htmlGedcomWriter.GetRepositories(TestRepositories.VitalRecordsRepository.Xref));

        //Assert.IsTrue(repositoryHtml.Contains(TestRepositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void ExportRepositoriesHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var repositoriesHtml = Encoding.UTF8.GetString(htmlGedcomWriter.GetRepositories());

        //Assert.IsTrue(repositoriesHtml.Contains(TestRepositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void NonExistingRepositoryHtmlTest()
    {
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var repositoryHtml = Encoding.UTF8.GetString(htmlGedcomWriter.GetRepositories(TestConstants.InvalidXref));

        //Assert.IsTrue(repositoryHtml.Equals(""));
    }

    //[TestMethod]
    public void WriteRepositoriesHtmlTest()
    {
        // This is an integration test. Figure that out later
        var htmlGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.HTML);
        var repositoriesHtml = Encoding.UTF8.GetString(htmlGedcomWriter.GetRepositories(TestRepositories.VitalRecordsRepository.Xref));

        File.WriteAllText(Path.Combine(TestUtilities.OutputFilesDirectory, "Repositories.html"), repositoriesHtml);
    }
}