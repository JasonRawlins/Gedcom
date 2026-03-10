using Gedcom;
using Gedcom.GedcomWriters;
using GedcomTests.TestEntities;
using System.Text;

namespace GedcomTests.Repositories;

// The use of the word "Repository" in this class refers to a Gedcom "Repository" (REPO) record,
// not its normal meaning related to source control.
[TestClass]
public class RepositoryJsonTests
{
    [TestMethod]
    public void ExportRepositoryJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Json);
        var repositoryJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetRepositories(TestRepositories.VitalRecordsRepository.Xref));

       //Assert.IsTrue(repositoryJson.Contains(TestRepositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void ExportRepositoriesJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Json);
        var repositoriesJson = Encoding.UTF8.GetString(jsonGedcomWriter.GetRepositories());

        //Assert.IsTrue(repositoriesJson.Contains(TestRepositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void ExportNonExistingRepositoryJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Json);
        var repositoriesJson = jsonGedcomWriter.GetRepositories(TestConstants.InvalidXref);

        //Assert.IsTrue(repositoriesJson.Equals("{}"));
    }

    //[TestMethod]
    public void WriteRepositoriesJsonTest()
    {
        // This is an integration test. Figure that out later
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Constants.Json);
        var jsonRepositoriesBytes = Encoding.UTF8.GetString(jsonGedcomWriter.GetRepositories());

        File.WriteAllText(Path.Combine(TestUtilities.OutputFilesDirectory, "Repositories.json"), jsonRepositoriesBytes);
    }
}

