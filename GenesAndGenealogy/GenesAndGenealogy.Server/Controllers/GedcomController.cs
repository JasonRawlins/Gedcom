using Gedcom;
using Gedcom.RecordStructures;
using GenesAndGenealogy.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Text;

namespace GenesAndGenealogy.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GedcomController : ControllerBase
    {
        private readonly ILogger<GedcomController> _logger;
        private FamilyManager FamilyManager { get; }
        private Gedcom.Gedcom Gedcom { get; }
        private TreeModel TreeModel => new(Gedcom.Header.Source.Tree);

        public GedcomController(ILogger<GedcomController> logger)
        {
            _logger = logger;

            var gedFileLines = Encoding.UTF8.GetString(Properties.Resources.GedcomNetTestTree).Split("\r\n");
            var gedcomLines = gedFileLines.Where(l => !string.IsNullOrEmpty(l)).Select(GedcomLine.Parse).ToList();
            Gedcom = new Gedcom.Gedcom(gedcomLines);
            FamilyManager = new FamilyManager(Gedcom);
        }

        [HttpGet(Name = "GetGedcom")]
        public IEnumerable<IndividualModel> Get()
        {
            return Gedcom.GetIndividualRecords().Select(ir => new IndividualModel(ir, TreeModel));
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
                var family = FamilyManager.CreateFamily(familyRecord.Xref, 1, 1);



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

        [HttpGet("individual/{individualXref}/families/")]
        public List<FamilyModel> GetIndividualFamilies(string individualXref)
        {
            return GetFamilyModels(Gedcom.GetIndividualRecord(individualXref));
        }

        [HttpGet("individuals")]
        public List<IndividualModel> GetIndividuals() => Gedcom.GetIndividualRecords().Select(ir => new IndividualModel(ir, TreeModel)).ToList();

        [HttpGet("profile/{individualXref}")]
        public ProfileModel GetProfile(string individualXref)
        {
            var individualRecord = Gedcom.GetIndividualRecord(individualXref);
            var individualModel = new IndividualModel(individualRecord, TreeModel);

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

            var profileModel = new ProfileModel(TreeModel, individualModel, familyModels, repositories, sortedSources);

            var parentsFamilyRecord = Gedcom.GetFamilyRecordOfParents(individualRecord.Xref);
            if (!parentsFamilyRecord.IsEmpty)
            {
                var familyManager = new FamilyManager(Gedcom);
                var family = familyManager.CreateFamily(parentsFamilyRecord.Xref, 0, 1);

                var fatherIndividualRecord = Gedcom.GetIndividualRecord(parentsFamilyRecord.Husband);
                IndividualModel? father = null;
                if (!fatherIndividualRecord.IsEmpty)
                {
                    father = new IndividualModel(fatherIndividualRecord, TreeModel);
                }

                var motherIndividualRecord = Gedcom.GetIndividualRecord(parentsFamilyRecord.Wife);
                IndividualModel? mother = null;
                if (!motherIndividualRecord.IsEmpty)
                {
                    mother = new IndividualModel(motherIndividualRecord, TreeModel);
                }

                profileModel.Parents = new FamilyModel(father, mother);
            }
            else
            {
                profileModel.Parents = new FamilyModel(null, null);
            }

            return profileModel;
        }

        [HttpGet("repository/{repositoryXref}")]
        public RepositoryModel GetRepository(string repositoryXref)
        {
            return new RepositoryModel(Gedcom.GetRepositoryRecord(repositoryXref));
        }

        [HttpGet("source/{sourceXref}")]
        public SourceModel GetSource(string sourceXref)
        {
            return new SourceModel(Gedcom.GetSourceRecord(sourceXref));
        }

        [HttpGet("tree")]
        public TreeModel GetTree()
        {
            return TreeModel;
        }
    }
}
