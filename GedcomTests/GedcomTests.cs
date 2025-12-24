//using Gedcom;
//using Gedcom.CLI;
//using GedcomTests.TestData;

//namespace GedcomTests;

//[TestClass]
//public class GedcomTests
//{
//    private Exporter? Exporter;
//    [TestInitialize]
//    public void BeforeEach()
//    {
//        Exporter = new Exporter(TestUtilities.CreateGedcom());
//    }

//    [TestMethod]
//    public void ExportGedcomAsJsonTest()
//    {
//        var gedcJson = Exporter!.GetGedcomJson();

//        Assert.IsTrue(gedcJson.Contains(TestIndividuals.DylanDavis.Xref)
//            && gedcJson.Contains(TestIndividuals.FionaDouglas.Xref)
//            && gedcJson.Contains(TestIndividuals.GwenJones.Xref)
//            && gedcJson.Contains(TestIndividuals.JamesSmith.Xref)
//            && gedcJson.Contains(TestIndividuals.MarySmith.Xref)
//            && gedcJson.Contains(TestIndividuals.OwenDavis.Xref)
//            && gedcJson.Contains(TestIndividuals.SaraDavis.Xref)
//            && gedcJson.Contains(TestFamilies.DylanDavisAndFionaDouglas.Xref)
//            && gedcJson.Contains(TestFamilies.JamesSmithAndSaraDavis.Xref)
//            && gedcJson.Contains(TestFamilies.OwenDavisAndGwenJones.Xref)
//            && gedcJson.Contains(TestRepositories.VitalRecordsRepository.Xref)
//            && gedcJson.Contains(TestSources.VitalRecords.Xref));
//    }

//    //[TestMethod]
//    // Dummy test method used for generating a Base64 strings of images.
//    //public void GetImageBase64String()
//    //{
//    //    var imageBase64String = TestUtilities.GetImageBase64String();
//    //}
//}
