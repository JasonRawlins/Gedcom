using CommandLine;
using System.Diagnostics;
using Gedcom.CLI;

namespace GedcomTests;

[TestClass]
public class CliTests
{
    [TestMethod]
    public void ParseValidArguments()
    {
        var args = new[] { "--input", @"input-path\input.ged", "--output", @"output-path\output.txt", "--format", "text", "--record-type", "indi", "--xref", "@I123@" };

        var result = Parser.Default.ParseArguments<CliOptions>(args);

        result.WithParsed(options =>
        {
            Assert.AreEqual(@"input-path\input.ged", options.InputFilePath);
            Assert.AreEqual(@"output-path\output.txt", options.OutputFilePath);
            Assert.AreEqual("TEXT", options.Format);
            Assert.AreEqual("INDI", options.RecordType);
            Assert.AreEqual("@I123@", options.Xref);
        });
    }

    [TestMethod]
    public void MixedCaseArgumentsAreValid()
    {
        var args = new[] { "--input", @"input-path\input.ged", "--output", @"output-path\output.txt", "--format", "TeXt", "--record-type", "InDi", "--xref", "@I123@" };

        var result = Parser.Default.ParseArguments<CliOptions>(args);

        result.WithParsed(options =>
        {
            Assert.AreEqual(@"input-path\input.ged", options.InputFilePath);
            Assert.AreEqual(@"output-path\output.txt", options.OutputFilePath);
            Assert.AreEqual("TEXT", options.Format);
            Assert.AreEqual("INDI", options.RecordType);
            Assert.AreEqual("@I123@", options.Xref);
        });
    }

    [TestMethod]
    public void MissingInputPathWithoutParamsFilePathResultsInError()
    {
        var args = new[] { "--output", @"output-path\output.txt", "--format", "text", "--record-type", "indi", "--xref", "@I123@" };

        var result = Parser.Default.ParseArguments<CliOptions>(args);

        result.WithParsed(options =>
        {
            var inputFilePathIsMissing = string.IsNullOrEmpty(options.InputFilePath);
            var paramsFilePathIsPresent = !string.IsNullOrEmpty(options.ParamsFilePath);
            if (inputFilePathIsMissing && !paramsFilePathIsPresent)
            {
                Assert.IsTrue(options.Errors.Contains("Input file path is required."));
            }
        });
    }

    // HACK: (terrible one) This runs the cli against all possible record types. Obviously not
    // a valid test outside of local environment due to different paths to the executable. The
    // executable path and the input/output argument values assume a local file structure
    // that won't exist on most machines. Uncomment to use.
    //[TestMethod]
    public void WriteGedcomWriterFiles()
    {
        List<string> allCliTestRecordTypeArguments =
            [.. TestCliArguments.AllIndividualRecordTypes, 
            .. TestCliArguments.AllRepositoryRecordTypes,
            .. TestCliArguments.AllSourceRecordTypes];

        foreach (var cliTestArguments in allCliTestRecordTypeArguments)
        {
            var cliTestArgumentsWithLocalPaths = cliTestArguments
                .Replace(@"input-path\", @"C:\temp\GedcomNET\Resources\\")
                .Replace(@"output-path\", @"C:\temp\GedcomNET\OutputFiles\\")
                .Replace("@I123@", TestEntities.TestIndividuals.DylanDavis.Xref)
                .Replace("@R123@", TestEntities.TestRepositories.VitalRecordsRepository.Xref)
                .Replace("@S123@", TestEntities.TestSources.DylanDavisBiography.Xref);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "C:\\Users\\amorm\\source\\repos\\GedcomProjects\\Gedcom\\bin\\Debug\\net8.0\\Gedcom.exe",
                    Arguments = cliTestArgumentsWithLocalPaths,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
            Console.WriteLine($"Exit code: {process.ExitCode}");
        }
    }
}
