using Gedcom;
using Gedcom.Core;
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
        private FamilyManager FamilyManager { get; }
        private Gedcom.Gedcom Gedcom { get; }
        private HeaderTreeJson HeaderTree => new(Gedcom.Header.Source.Tree);

        public GedcomController(ILogger<GedcomController> logger)
        {
            _logger = logger;

            var gedFileLines = Encoding.UTF8.GetString(Properties.Resources.GedcomNetTestTree).Split("\r\n");
            var gedcomLines = gedFileLines.Where(l => !string.IsNullOrEmpty(l)).Select(GedcomLine.Parse).ToList();
            Gedcom = new Gedcom.Gedcom(gedcomLines);
            FamilyManager = new FamilyManager(Gedcom);
        }

        [HttpGet(Name = "GetGedcom")]
        public IEnumerable<IndividualJson> Get()
        {
            return Gedcom.GetIndividualRecords().Select(ir => new IndividualJson(ir, HeaderTree.AutomatedRecordId));
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

                var husband = new IndividualJson(Gedcom.GetIndividualRecord(familyRecord.Husband), HeaderTree.AutomatedRecordId);
                var wife = new IndividualJson(Gedcom.GetIndividualRecord(familyRecord.Wife), HeaderTree.AutomatedRecordId);
                var children = new List<IndividualJson>();

                foreach (var childXref in familyRecord.Children)
                {
                    var childIndividualRecord = Gedcom.GetIndividualRecord(childXref);
                    children.Add(new IndividualJson(childIndividualRecord, HeaderTree.AutomatedRecordId));
                }

                var events = new List<EventJson>();
                foreach (var familyEventStructure in familyRecord.FamilyEventStructures.Select(fes => new EventJson(fes)).ToList())
                {
                    events.Add(familyEventStructure);
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

        [HttpGet("individual/{individualXref}")]
        public IndividualJson GetIndividual(string individualXref)
        {
            var individualRecord = Gedcom.GetIndividualRecord(individualXref);
            return new IndividualJson(individualRecord, HeaderTree.AutomatedRecordId);
        }

        [HttpGet("individual/{individualXref}/families/")]
        public List<FamilyModel> GetIndividualFamilies(string individualXref)
        {
            return GetFamilyModels(Gedcom.GetIndividualRecord(individualXref));
        }

        [HttpGet("individuals")]
        public List<IndividualJson> GetIndividuals()
        {
            var individuals = Gedcom.GetIndividualRecords().Select(ir => new IndividualJson(ir, HeaderTree.AutomatedRecordId)).ToList();
            return [.. individuals.OrderBy(i => i.Birth).ThenBy(i => i.Surname)];
        }

        [HttpGet("profile/{individualXref}")]
        public ProfileModel GetProfile(string individualXref)
        {
            var individualRecord = Gedcom.GetIndividualRecord(individualXref);
            var individualJson = new IndividualJson(individualRecord, HeaderTree.AutomatedRecordId);

            var familyModels = GetFamilyModels(individualRecord);

            foreach (var familyModel in familyModels)
            {
                individualJson.Events!.AddRange(familyModel.Events);
            }

            individualJson.Events!.Sort();

            //var groupedEvents = individualModel.Events
            //    .GroupBy(e => e.Date.Year)
            //    .Select(g => new { Year = g.Key, Events = g.ToList() });

            var repositories = Gedcom.GetRepositoryRecords().Select(r => new RepositoryJson(r)).ToList();

            var sources = new List<SourceJson>();
            foreach (var sourceCitation in individualRecord.SourceCitations)
            {
                var sourceRecord = Gedcom.GetSourceRecord(sourceCitation.Xref);
                sources.Add(new SourceJson(sourceRecord));
            }
            var sortedSources = sources.OrderBy(s => s.DescriptiveTitle).ToList();

            var multimedias = new List<MultimediaJson>();
            foreach (var multimediaLink in individualRecord.MultimediaLinks)
            {
                var multimediaRecord = Gedcom.GetObjectRecord(multimediaLink.Xref);
                multimedias.Add(new MultimediaJson(multimediaRecord));
            }

            var profileModel = new ProfileModel(HeaderTree, individualJson, familyModels, repositories, sortedSources);
            
            var portraitMultimedia = multimedias.FirstOrDefault(m => m.File?.Form?.MediaType?.Equals("portrait") ?? false);
            if (portraitMultimedia != null)
            {
                profileModel.PortraitMultiMedia = portraitMultimedia;
            }

            var parentsFamilyRecord = Gedcom.GetFamilyRecordOfParents(individualRecord.Xref);
            if (!parentsFamilyRecord.IsEmpty)
            {
                var familyManager = new FamilyManager(Gedcom);
                var family = familyManager.CreateFamily(parentsFamilyRecord.Xref, Generation.Current, Generation.Child);

                var fatherIndividualRecord = Gedcom.GetIndividualRecord(parentsFamilyRecord.Husband);
                IndividualJson? father = null;
                if (!fatherIndividualRecord.IsEmpty)
                {
                    father = new IndividualJson(fatherIndividualRecord, HeaderTree.AutomatedRecordId);
                }

                var motherIndividualRecord = Gedcom.GetIndividualRecord(parentsFamilyRecord.Wife);
                IndividualJson? mother = null;
                if (!motherIndividualRecord.IsEmpty)
                {
                    mother = new IndividualJson(motherIndividualRecord, HeaderTree.AutomatedRecordId);
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
        public RepositoryJson GetRepository(string repositoryXref)
        {
            return new RepositoryJson(Gedcom.GetRepositoryRecord(repositoryXref));
        }

        [HttpGet("source/{sourceXref}")]
        public SourceJson GetSource(string sourceXref)
        {
            return new SourceJson(Gedcom.GetSourceRecord(sourceXref));
        }

        [HttpGet("tree")]
        public HeaderTreeJson GetTree()
        {
            return HeaderTree;
        }
    }
}
