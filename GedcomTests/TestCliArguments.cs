namespace GedcomTests;

public static class TestCliArguments
{
    #region Excel

    // Excel
    public static readonly string[] ExcelIndividual = ["--input", @"input-path", "--output", @"output-path\cli-individual.xlsx", "--format", "excel", "--record-type", "indi", "--xref", "@I123@"];
    public static readonly string[] ExcelIndividuals = ["--input", @"input-path", "--output", @"output-path\cli-individuals.xlsx", "--format", "excel", "--record-type", "indi"];

    // Html
    public static readonly string[] HtmlIndividual = ["--input", @"input-path", "--output", @"output-path\cli-individual.html", "--format", "html", "--record-type", "indi", "--xref", "@I123@"];
    public static readonly string[] HtmlIndividuals = ["--input", @"input-path", "--output", @"output-path\cli-individuals.html", "--format", "html", "--record-type", "indi"];

    // Json
    public static readonly string[] JsonIndividual = ["--input", @"input-path", "--output", @"output-path\cli-individual.json", "--format", "json", "--record-type", "indi", "--xref", "@I123@"];
    public static readonly string[] JsonIndividuals = ["--input", @"input-path", "--output", @"output-path\cli-individuals.json", "--format", "json", "--record-type", "indi"];

    // Text
    public static readonly string[] TextIndividual = ["--input", @"input-path", "--output", @"output-path\cli-individual.txt", "--format", "text", "--record-type", "indi", "--xref", "@I123@"];
    public static readonly string[] TextIndividuals = ["--input", @"input-path", "--output", @"output-path\cli-individuals.txt", "--format", "text", "--record-type", "indi"];

    public static readonly List<string> AllIndividualRecordTypes =
        [
            string.Join(" ", ExcelIndividual),
            string.Join(" ", ExcelIndividuals),
            string.Join(" ", HtmlIndividual),
            string.Join(" ", HtmlIndividuals),
            string.Join(" ", JsonIndividual),
            string.Join(" ", JsonIndividuals),
            string.Join(" ", TextIndividual),
            string.Join(" ", TextIndividuals)
        ];

    //, .. ExcelIndividuals, .. HtmlIndividual, .. JsonIndividual, .. JsonIndividuals, .. TextIndividual, .. TextIndividuals];

    #endregion

    #region Repositories

    // Excel
    public static readonly string[] ExcelRepository = ["--input", @"input-path", "--output", @"output-path\cli-repository.xlsx", "--format", "excel", "--record-type", "repo", "--xref", "@R123@"];
    public static readonly string[] ExcelRepositories = ["--input", @"input-path", "--output", @"output-path\cli-repositories.xlsx", "--format", "excel", "--record-type", "repo"];

    // Html
    public static readonly string[] HtmlRepository = ["--input", @"input-path", "--output", @"output-path\cli-repository.html", "--format", "html", "--record-type", "repo", "--xref", "@R123@"];
    public static readonly string[] HtmlRepositories = ["--input", @"input-path", "--output", @"output-path\cli-repositories.html", "--format", "html", "--record-type", "repo"];

    // Json
    public static readonly string[] JsonRepository = ["--input", @"input-path", "--output", @"output-path\cli-repository.json", "--format", "json", "--record-type", "repo", "--xref", "@R123@"];
    public static readonly string[] JsonRepositories = ["--input", @"input-path", "--output", @"output-path\cli-repositories.json", "--format", "json", "--record-type", "repo"];

    // Text
    public static readonly string[] TextRepository = ["--input", @"input-path", "--output", @"output-path\cli-repository.txt", "--format", "text", "--record-type", "repo", "--xref", "@R123@"];
    public static readonly string[] TextRepositories = ["--input", @"input-path", "--output", @"output-path\cli-repositories.txt", "--format", "text", "--record-type", "repo"];

    public static readonly List<string> AllRepositoryRecordTypes =
    [
        string.Join(" ", ExcelRepository),
            string.Join(" ", ExcelRepositories),
            string.Join(" ", HtmlRepository),
            string.Join(" ", HtmlRepositories),
            string.Join(" ", JsonRepository),
            string.Join(" ", JsonRepositories),
            string.Join(" ", TextRepository),
            string.Join(" ", TextRepositories)
    ];

    #endregion

    #region Sources

    // Excel
    public static readonly string[] ExcelSource = ["--input", @"input-path", "--output", @"output-path\cli-source.xlsx", "--format", "excel", "--record-type", "sour", "--xref", " @S977470769@"];
    public static readonly string[] ExcelSources = ["--input", @"input-path", "--output", @"output-path\cli-sources.xlsx", "--format", "excel", "--record-type", "sour"];

    // Html
    public static readonly string[] HtmlSource = ["--input", @"input-path", "--output", @"output-path\cli-source.html", "--format", "html", "--record-type", "sour", "--xref", " @S977470769@"];
    public static readonly string[] HtmlSources = ["--input", @"input-path", "--output", @"output-path\cli-sources.html", "--format", "html", "--record-type", "sour"];

    // Json
    public static readonly string[] JsonSource = ["--input", @"input-path", "--output", @"output-path\cli-source.json", "--format", "json", "--record-type", "sour", "--xref", " @S977470769@"];
    public static readonly string[] JsonSources = ["--input", @"input-path", "--output", @"output-path\cli-sources.json", "--format", "json", "--record-type", "sour"];

    // Text
    public static readonly string[] TextSource = ["--input", @"input-path", "--output", @"output-path\cli-source.txt", "--format", "text", "--record-type", "sour", "--xref", " @S977470769@"];
    public static readonly string[] TextSources = ["--input", @"input-path", "--output", @"output-path\cli-sources.txt", "--format", "text", "--record-type", "sour"];

    public static readonly List<string> AllSourceRecordTypes =
    [
        string.Join(" ", ExcelSource),
            string.Join(" ", ExcelSources),
            string.Join(" ", HtmlSource),
            string.Join(" ", HtmlSources),
            string.Join(" ", JsonSource),
            string.Join(" ", JsonSources),
            string.Join(" ", TextSource),
            string.Join(" ", TextSources)
    ];

    #endregion
}
