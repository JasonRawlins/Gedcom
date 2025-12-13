using Gedcom.CLI;
using GedcomTests.TestData;

namespace GedcomTests;

[TestClass]
public class RepositoryTests
{
    private Exporter? Exporter;
    [TestInitialize]
    public void BeforeEach()
    {
        Exporter = new Exporter(TestUtilities.CreateGedcom());
    }

    [TestMethod]
    public void ExportRepositoryJsonTest()
    {
        var repositoryJson = Exporter!.GetRepositoryRecordJson(TestTree.Repositories.VitalRecordsRepository.Xref);

        Assert.IsTrue(repositoryJson.Contains(TestTree.Repositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void ExportRepositoriesJsonTest()
    {
        var repositoriesJson = Exporter!.GetRepositoryRecordsJson();

        Assert.IsTrue(repositoriesJson.Contains(TestTree.Repositories.VitalRecordsRepository.Xref));
    }

    [TestMethod]
    public void ExportNonExistingRepositoryJsonTest()
    {
        var repositoryJson = Exporter!.GetRepositoryRecordJson("INVALID_XREF");

        Assert.IsTrue(repositoryJson.Equals("{}"));
    }

    [TestMethod]
    public void QueryRepositoriesJsonTest()
    {
        var repositoryJson = Exporter!.GetRepositoryRecordsJson("Vital records");

        Assert.IsTrue(repositoryJson.Contains(TestTree.Repositories.VitalRecordsRepository.Xref));
    }
}