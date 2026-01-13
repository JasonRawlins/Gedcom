using Gedcom;
using Gedcom.RecordStructures;
using GenesAndGenealogy.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace GenesAndGenealogy.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GedcomController : ControllerBase
    {
        private readonly ILogger<GedcomController> _logger;
        private Gedcom.Gedcom Gedcom { get; }
        private TreeModel TreeModel => new(Gedcom.Header.Source.Tree);

        public GedcomController(ILogger<GedcomController> logger)
        {
            _logger = logger;

            var gedFileLines = Encoding.UTF8.GetString(Properties.Resources.GedcomNetTestTree).Split("\r\n");
            var gedcomLines = gedFileLines.Where(l => !string.IsNullOrEmpty(l)).Select(GedcomLine.Parse).ToList();
            Gedcom = new Gedcom.Gedcom(gedcomLines);
        }

        [HttpGet(Name = "GetGedcom")]
        public IEnumerable<IndividualModel> Get()
        {
            return Gedcom.GetIndividualRecords().Select(ir => new IndividualModel(ir, TreeModel));
        }

        [HttpGet("individuals")]
        public List<IndividualModel> GetIndividuals() => Gedcom.GetIndividualRecords().Select(ir => new IndividualModel(ir, TreeModel)).ToList();

        [HttpGet("profile/{individualXref}")]
        public ProfileModel GetProfile(string individualXref)
        {
            var individualRecord = Gedcom.GetIndividualRecord(individualXref);
            var individualModel = new IndividualModel(individualRecord, TreeModel);

            var parentsFamilyRecord = Gedcom.GetFamilyRecordOfParents(individualRecord.Xref);
            var father = new IndividualModel(Gedcom.GetIndividualRecord(parentsFamilyRecord.Husband), TreeModel);
            var mother = new IndividualModel(Gedcom.GetIndividualRecord(parentsFamilyRecord.Wife), TreeModel);
            var parents = new FamilyModel(father, mother);

            var familyModels = GetFamilyModels(individualRecord);

            foreach (var familyModel in familyModels)
            {
                individualModel.Events.AddRange(familyModel.Events);
            }

            individualModel.Events.Sort();

            //var groupedEvents = individualModel.Events
            //    .GroupBy(e => e.Date.Year)
            //    .Select(g => new { Year = g.Key, Events = g.ToList() });

            var repositories = Gedcom.GetRepositoryRecords().Select(r => new RepositoryModel(r)).ToList();

            var sources = new List<SourceModel>();
            foreach (var sourceCitation in individualRecord.SourceCitations)
            {
                var sourceRecord = Gedcom.GetSourceRecord(sourceCitation.Xref);
                sources.Add(new SourceModel(sourceRecord));
            }

            var sortedSources = sources.OrderBy(s => s.Title).ToList();

            var profileModel = new ProfileModel(TreeModel, individualModel, parents, familyModels, repositories, sortedSources);

            return profileModel;
        }

        [HttpGet("individual/{individualXref}/families/")]
        public List<FamilyModel> GetIndividualFamilies(string individualXref)
        {
            return GetFamilyModels(Gedcom.GetIndividualRecord(individualXref));
        }

        private List<FamilyModel> GetFamilyModels(IndividualRecord individualRecord)
        {
            var familyRecords = new List<FamilyRecord>();

            foreach (var spouseToFamilyLink in individualRecord.SpouseToFamilyLinks)
            {
                var familyRecord = Gedcom.GetFamilyRecord(spouseToFamilyLink.Xref);
                familyRecords.Add(familyRecord);
            }

            var familyModels = new List<FamilyModel>();

            foreach (var familyRecord in familyRecords)
            {
                var husband = new IndividualModel(Gedcom.GetIndividualRecord(familyRecord.Husband), TreeModel);
                var wife = new IndividualModel(Gedcom.GetIndividualRecord(familyRecord.Wife), TreeModel);
                var children = new List<IndividualModel>();

                foreach (var childXref in familyRecord.Children)
                {
                    var childIndividualRecord = Gedcom.GetIndividualRecord(childXref);
                    children.Add(new IndividualModel(childIndividualRecord, TreeModel));
                }

                var events = new List<EventModel>();
                foreach (var familyEventStructure in familyRecord.FamilyEventStructures)
                {
                    events.Add(new EventModel(familyEventStructure));
                }

                var familyModel = new FamilyModel(husband, wife)
                {
                    Children = children,
                    Events = events
                };

                familyModels.Add(familyModel);
            }

            return familyModels;
        }
    }
}
