using Newtonsoft.Json;
using System.Text;

namespace Gedcom.CLI;

public class Exporter(Gedcom gedcom)
{
    public Gedcom Gedcom { get; set; } = gedcom;

    public string GetGedcomJson() => JsonConvert.SerializeObject(Gedcom, JsonSettings.DefaultOptions);

    // Family (FAM)
    public string GetFamilyRecordJson(string xref) => GetRecordJson(Gedcom.GetFamilyRecord(xref));
    public string GetFamilyRecordsJson(string query = "") => GetRecordsJson(Gedcom.GetFamilyRecords(query));

    public string GetIndividualJson(string xref) => GetRecordJson(Gedcom.GetIndividualRecord(xref));
    public string GetIndividualsJson(string query = "") => GetRecordsJson(Gedcom.GetIndividualRecords(query));

    public string GetIndividualsHtml(string query = "")
    {
        var individualListItems = Gedcom.GetIndividualRecords(query).Select(ir => new IndividualListItem(ir)).ToList();

        var htmlTemplate = Encoding.UTF8.GetString(Properties.Resources.IndividualsHtmlTemplate);
        var individualLis = HtmlWriter.CreateIndividualList(individualListItems, Gedcom.Header.Source.Tree.AutomatedRecordId);
        var finalHtml = htmlTemplate.Replace("{{INDIVIDUAL_LIST_ITEMS}}", string.Join(Environment.NewLine, individualLis));

        return finalHtml;
    }

    public byte[] IndividualsExcel()
    {
        var individualListItems = Gedcom.GetIndividualRecords().Select(ir => new IndividualListItem(ir)).ToList();
        var excelWriter = new ExcelWriter(Gedcom.Header.Source.Tree);
        return excelWriter.GetIndividuals(individualListItems);
    }

    // Repository (REPO)
    public string GetRepositoryRecordJson(string xref) => GetRecordJson(Gedcom.GetRepositoryRecord(xref));
    public string GetRepositoryRecordsJson(string query = "") => GetRecordsJson(Gedcom.GetRepositoryRecords(query));

    // Source (SOUR)
    public string GetSourceRecordJson(string xref) => GetRecordJson(Gedcom.GetSourceRecord(xref));
    public string GetSourceRecordsJson(string query = "") => GetRecordsJson(Gedcom.GetSourceRecords(query));

    private static string GetRecordJson(RecordStructureBase recordStructure)
    {
        if (recordStructure.IsEmpty) return "{}";
        return JsonConvert.SerializeObject(recordStructure, JsonSettings.DefaultOptions);
    }

    private static string GetRecordsJson(IEnumerable<RecordStructureBase> recordStructures)
    {
        if (!recordStructures.Any()) return "{}";
        return JsonConvert.SerializeObject(recordStructures, JsonSettings.DefaultOptions);
    }
}