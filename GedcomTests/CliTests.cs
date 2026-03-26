using System.Diagnostics;
using CommandLine;
using Gedcom.CLI;

namespace GedcomTests;

[TestClass]
public class CliTests
{
    [TestMethod]
    public void ParseValidArguments()
    {
        var args = new[] { 
            "--format", "text",
            "--input", @"input-path\input.ged", 
            "--output", @"output-path\output.txt", 
            "--record-type", "indi", 
            "--xref", "@I123@" };

        var result = Parser.Default.ParseArguments<CliOptions>(args);

        result.WithParsed(options =>
        {
            Assert.AreEqual("TEXT", options.Format);
            Assert.AreEqual(@"input-path\input.ged", options.InputFilePath);
            Assert.AreEqual(@"output-path\output.txt", options.OutputFilePath);
            Assert.AreEqual("INDI", options.RecordType);
            Assert.AreEqual("@I123@", options.Xref);
        });
    }

    [TestMethod]
    public void MixedCaseArgumentsAreValid()
    {
        var args = new[] { 
            "--format", "TeXt", 
            "--input", @"input-path\input.ged", 
            "--output", @"output-path\output.txt", 
            "--record-type", "InDi", 
            "--xref", "@I123@" };

        var result = Parser.Default.ParseArguments<CliOptions>(args);

        result.WithParsed(options =>
        {
            Assert.AreEqual("TEXT", options.Format);
            Assert.AreEqual(@"input-path\input.ged", options.InputFilePath);
            Assert.AreEqual(@"output-path\output.txt", options.OutputFilePath);
            Assert.AreEqual("INDI", options.RecordType);
            Assert.AreEqual("@I123@", options.Xref);
        });
    }

    [TestMethod]
    public void MissingInputPathWithoutParamsFilePathResultsInError()
    {
        var args = new[] { 
            "--format", "text",
            "--output", @"output-path\output.txt", 
            "--record-type", "indi", 
            "--xref", "@I123@" };

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

    [TestMethod]
    public void MissingOutputPathWithoutParamsFilePathResultsInError()
    {
        var args = new[] { 
            "--format", "text",
            "--input", @"input-path\input.ged", 
            "--record-type", "indi", 
            "--xref", "@I123@" };

        var result = Parser.Default.ParseArguments<CliOptions>(args);

        result.WithParsed(options =>
        {
            var outputFilePathIsMissing = string.IsNullOrEmpty(options.OutputFilePath);
            var paramsFilePathIsPresent = !string.IsNullOrEmpty(options.ParamsFilePath);
            if (outputFilePathIsMissing && !paramsFilePathIsPresent)
            {
                Assert.IsTrue(options.Errors.Contains("Output file path is required."));
            }
        });
    }

    [TestMethod]
    public void MissingFormatWithoutParamsFilePathResultsInError()
    {
        var args = new[] { 
            "--input", @"input-path\input.ged", 
            "--output", @"output-path\output.txt", 
            "--record-type", "indi", 
            "--xref", "@I123@" };

        var result = Parser.Default.ParseArguments<CliOptions>(args);

        result.WithParsed(options =>
        {
            var formatIsMissing = string.IsNullOrEmpty(options.Format);
            var paramsFilePathIsPresent = !string.IsNullOrEmpty(options.ParamsFilePath);
            if (formatIsMissing && !paramsFilePathIsPresent)
            {
                Assert.IsTrue(options.Errors.Contains("Format is required."));
            }
        });
    }

    [TestMethod]
    public void MissingRecordTypeWithoutParamsFilePathResultsInError()
    {
        var args = new[] { 
            "--format", "text",
            "--input", @"input-path\input.ged", 
            "--output", @"output-path\output.txt", 
            "--xref", "@I123@" };

        var result = Parser.Default.ParseArguments<CliOptions>(args);

        result.WithParsed(options =>
        {
            var recordTypeIsMissing = string.IsNullOrEmpty(options.RecordType);
            var paramsFilePathIsPresent = !string.IsNullOrEmpty(options.ParamsFilePath);
            if (recordTypeIsMissing && !paramsFilePathIsPresent)
            {
                Assert.IsTrue(options.Errors.Contains(CliErrorMessages.RecordTypeIsRequired));
            }
        });
    }

    [TestMethod]
    public void RecordTypeIsValid()
    {
        var args = new[] {
            "--format", "text",
            "--input", @"input-path\input.ged",
            "--output", @"output-path\output.txt",
            "--record-type", "FAIL", 
            "--xref", "@I123@" };

        var result = Parser.Default.ParseArguments<CliOptions>(args);

        result.WithParsed(options =>
        {
            Assert.IsTrue(options.Errors.Contains($"'{options.RecordType}' {CliErrorMessages.RecordTypeIsInvalid}"));
        });
    }

    [TestMethod]
    public void FormatIsValid()
    {
        var args = new[] {
            "--format", "FAIL",
            "--input", @"input-path\input.ged",
            "--output", @"output-path\output.txt",
            "--record-type", "indi",
            "--xref", "@I123@" };

        var result = Parser.Default.ParseArguments<CliOptions>(args);

        result.WithParsed(options =>
        {
            Assert.IsTrue(options.Errors.Contains($"'{options.Format}' {CliErrorMessages.FormatIsInvalid}"));
        });
    }

    [TestMethod]
    public void ParamsFileOverridesAllOtherParameters()
    {
        var testOuputFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestOutput\GedcomNetTestTree.ged");

        var args = new[] { 
            "--format", "original-text", 
            "--input", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestFiles\GedcomNetTestTree.ged"),
            "--output", @"original-output.txt", 
            "--record-type", "original-indi", 
            "--xref", "original-@I123@",
            "--params", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestFiles\TestGedcomCliParamsThatReplaceArguments.json")
        };

        var result = Parser.Default.ParseArguments<CliOptions>(args);

        result.WithParsed(options =>
        {
            options.ApplyParamsFile();

            Assert.AreEqual("REPLACED-TEXT", options.Format);
            Assert.AreEqual("replaced-input.ged", options.InputFilePath);
            Assert.AreEqual("replaced-output.txt", options.OutputFilePath);
            Assert.AreEqual("REPLACED-INDI", options.RecordType);
            Assert.AreEqual("replaced-@I123@", options.Xref);
        });
    }

    [TestMethod]
    public void XrefParameterIsInValidFormat()
    {
        var args = new[] {
            "--format", "text",
            "--input", @"input-path\input.ged",
            "--output", @"output-path\output.txt",
            "--record-type", "indi", 
            "--xref", "FAIL" };

        var result = Parser.Default.ParseArguments<CliOptions>(args);

        result.WithParsed(options =>
        {
            if (!string.IsNullOrEmpty(options.Xref))
            {
                Assert.IsTrue(options.Errors.Contains($"'{options.Xref}' {CliErrorMessages.XrefIsInvalid}"));
            }
        });
    }

    [TestMethod]
    public void WriteGedcomWriterFiles()
    {
        var gedcomExecutableFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Gedcom.exe");
        var gedcomNetTestTreeFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestFiles\GedcomNetTestTree.ged");
        var testOutputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"TestOutput");

        Directory.CreateDirectory(testOutputDirectory);

        List<string> allCliTestRecordTypeArguments =
            [.. TestCliArguments.AllIndividualRecordTypes, 
            .. TestCliArguments.AllRepositoryRecordTypes,
            .. TestCliArguments.AllSourceRecordTypes];

        foreach (var cliTestArguments in allCliTestRecordTypeArguments)
        {
            var cliTestArgumentsWithLocalPaths = cliTestArguments
                .Replace(@"input-path", gedcomNetTestTreeFilePath)
                .Replace(@"output-path", testOutputDirectory)
                .Replace("@I123@", TestEntities.TestIndividuals.DylanDavis.Xref)
                .Replace("@R123@", TestEntities.TestRepositories.VitalRecordsRepository.Xref)
                .Replace("@S123@", TestEntities.TestSources.DylanDavisBiography.Xref);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = gedcomExecutableFilePath,
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
