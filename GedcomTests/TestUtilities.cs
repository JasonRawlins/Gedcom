using Gedcom;

namespace GedcomTests;

public class TestUtilities
{
    public static string TestTreeFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "TestTree.ged");
    public static string TestTreeDirectory = Path.GetDirectoryName(TestTreeFullName) ?? "";
    public static string TestTreeOutputFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "TestTree.json");

    public static Options CreateOptionsWithInputAndOutput()
    {
        return new Options
        {
            InputFilePath = TestTreeFullName,
            OutputFilePath = TestTreeOutputFullName,
        };
    }
}

public class GedcomAssert
{
    public static void ExporterIsValid(Exporter exporter)
    {
        if (exporter.Errors.Count > 0)
        {
            Assert.Fail("There were errors in the Exporter: ", string.Join(Environment.NewLine, exporter.Errors));
        }
    }
}