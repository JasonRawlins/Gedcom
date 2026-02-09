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
        private FamilyManager FamilyManager { get; }
        private GedcomDocument Gedcom { get; }
        private HeaderTreeDto HeaderTree => new(Gedcom.Header.Source.Tree);

        public GedcomController(ILogger<GedcomController> logger)
        {
            _logger = logger;

            var gedFileLines = Encoding.UTF8.GetString(Properties.Resources.GedcomNetTestTree).Split("\r\n");
            var gedcomLines = gedFileLines.Where(l => !string.IsNullOrEmpty(l)).Select(GedcomLine.Parse).ToList();
            Gedcom = new GedcomDocument(gedcomLines);
            FamilyManager = new FamilyManager(Gedcom);
        }

        [HttpGet(Name = "GetGedcom")]
        public IEnumerable<IndividualDto> Get()
        {
            return Gedcom.GetIndividualRecords().Select(ir => new IndividualDto(ir, HeaderTree.AutomatedRecordId));
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

                var husband = new IndividualDto(Gedcom.GetIndividualRecord(familyRecord.Husband), HeaderTree.AutomatedRecordId);
                var wife = new IndividualDto(Gedcom.GetIndividualRecord(familyRecord.Wife), HeaderTree.AutomatedRecordId);
                var children = new List<IndividualDto>();

                foreach (var childXref in familyRecord.Children)
                {
                    var childIndividualRecord = Gedcom.GetIndividualRecord(childXref);
                    children.Add(new IndividualDto(childIndividualRecord, HeaderTree.AutomatedRecordId));
                }

                var events = new List<EventDto>();
                foreach (var familyEventStructure in familyRecord.FamilyEventStructures.Select(fes => new EventDto(fes)).ToList())
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
        public IndividualDto GetIndividual(string individualXref)
        {
            var individualRecord = Gedcom.GetIndividualRecord(individualXref);
            return new IndividualDto(individualRecord, HeaderTree.AutomatedRecordId);
        }

        [HttpGet("individual/{individualXref}/families/")]
        public List<FamilyModel> GetIndividualFamilies(string individualXref)
        {
            return GetFamilyModels(Gedcom.GetIndividualRecord(individualXref));
        }

        [HttpGet("individuals")]
        public List<IndividualDto> GetIndividuals()
        {
            var individuals = Gedcom.GetIndividualRecords().Select(ir => new IndividualDto(ir, HeaderTree.AutomatedRecordId)).ToList();
            return [.. individuals.OrderBy(i => i.Birth).ThenBy(i => i.Surname)];
        }

        [HttpGet("multimedia-items")]
        public List<MultimediaDto> GetMultimediaItems()
        {
            var multimediaItems = Gedcom.GetObjectRecords().Select(or => new MultimediaDto(or)).ToList();
            return multimediaItems;
        }

        [HttpGet("profile/{individualXref}")]
        public ProfileModel GetProfile(string individualXref)
        {
            var individualRecord = Gedcom.GetIndividualRecord(individualXref);
            var individualJson = new IndividualDto(individualRecord, HeaderTree.AutomatedRecordId);

            var familyModels = GetFamilyModels(individualRecord);

            foreach (var familyModel in familyModels)
            {
                individualJson.Events!.AddRange(familyModel.Events);
            }

            individualJson.Events!.Sort();

            //var groupedEvents = individualModel.Events
            //    .GroupBy(e => e.Date.Year)
            //    .Select(g => new { Year = g.Key, Events = g.ToList() });

            var repositories = Gedcom.GetRepositoryRecords().Select(r => new RepositoryDto(r)).ToList();

            var sources = new List<SourceDto>();
            foreach (var sourceCitation in individualRecord.SourceCitations)
            {
                var sourceRecord = Gedcom.GetSourceRecord(sourceCitation.Xref);
                sources.Add(new SourceDto(sourceRecord));
            }
            var sortedSources = sources.OrderBy(s => s.DescriptiveTitle).ToList();

            var multimediaItems = new List<MultimediaDto>();
            foreach (var multimediaLink in individualRecord.MultimediaLinks)
            {
                var multimediaRecord = Gedcom.GetObjectRecord(multimediaLink.Xref);
                multimediaItems.Add(new MultimediaDto(multimediaRecord));
            }

            var profileModel = new ProfileModel(HeaderTree, individualJson, familyModels, repositories, sortedSources, multimediaItems);
            
            var portraitMultimedia = multimediaItems.FirstOrDefault(m => m.File?.Form?.MediaType?.Equals("portrait") ?? false);
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
                IndividualDto? father = null;
                if (!fatherIndividualRecord.IsEmpty)
                {
                    father = new IndividualDto(fatherIndividualRecord, HeaderTree.AutomatedRecordId);
                }

                var motherIndividualRecord = Gedcom.GetIndividualRecord(parentsFamilyRecord.Wife);
                IndividualDto? mother = null;
                if (!motherIndividualRecord.IsEmpty)
                {
                    mother = new IndividualDto(motherIndividualRecord, HeaderTree.AutomatedRecordId);
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
        public RepositoryDto GetRepository(string repositoryXref)
        {
            return new RepositoryDto(Gedcom.GetRepositoryRecord(repositoryXref));
        }

        [HttpGet("source/{sourceXref}")]
        public SourceDto GetSource(string sourceXref)
        {
            return new SourceDto(Gedcom.GetSourceRecord(sourceXref));
        }

        [HttpGet("tree")]
        public HeaderTreeDto GetTree()
        {
            return HeaderTree;
        }
    }
}
