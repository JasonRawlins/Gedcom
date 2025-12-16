using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

[TestClass]
public class RepositoryJsonTests
{
    [TestMethod]
    public void ExportRepositoryJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var repositoryJson = jsonGedcomWriter.GetRepository(TestTree.Repositories.VitalRecordsRepository.Xref);

        Assert.IsTrue(repositoryJson.Contains(TestTree.Repositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void ExportRepositoriesJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var repositoriesJson = jsonGedcomWriter.GetRepositories();

        Assert.IsTrue(repositoriesJson.Contains(TestTree.Repositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void ExportNonExistingRepositoryJsonTest()
    {
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);
        var repositoriesJson = jsonGedcomWriter.GetRepository(TestTree.InvalidXref);

        Assert.IsTrue(repositoriesJson.Equals("{}"));
    }

    [TestMethod]
    public void WriteRepositoriesJsonTest()
    {
        // This is an integration test. Figure that out later
        var jsonGedcomWriter = GedcomWriter.Create(TestUtilities.CreateGedcom(), Gedcom.C.JSON);

        File.WriteAllText(TestUtilities.JsonFullName, jsonGedcomWriter.GetRepositories());
    }
}

