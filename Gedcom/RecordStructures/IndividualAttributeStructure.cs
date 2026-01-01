//using Newtonsoft.Json;

//namespace Gedcom.RecordStructures;

//// The Gedcom Standard 5.5.1 documentation is at the end of this file.
//[JsonConverter(typeof(IndividualAttributeStructureJsonConverter))]
//public class IndividualAttributeStructure : RecordStructureBase
//{
//    public IndividualAttributeStructure() : base() { }
//    public IndividualAttributeStructure(Record record) : base(record) { }
    
//    public IndividualEventDetail IndividualEventDetail => First<IndividualEventDetail>(Record.Tag);

//    public override string ToString() => $"{Record.Value}";
//}

//internal class IndividualAttributeStructureJsonConverter : JsonConverter<IndividualAttributeStructure>
//{
//    public override IndividualAttributeStructure? ReadJson(JsonReader reader, Type objectType, IndividualAttributeStructure? existingValue, bool hasExistingValue, JsonSerializer serializer) => throw new NotImplementedException();

//    public override void WriteJson(JsonWriter writer, IndividualAttributeStructure? individualAttributeStructure, JsonSerializer serializer)
//    {
//        if (individualAttributeStructure == null) throw new ArgumentNullException(nameof(individualAttributeStructure));

//        serializer.Serialize(writer, new IndividualAttributeStructureJson(individualAttributeStructure));
//    }
//}

//internal class IndividualAttributeStructureJson : GedcomJson
//{
//    public IndividualAttributeStructureJson(IndividualAttributeStructure individualAttributeStructure)
//    {
//        IndividualEventDetail = JsonRecord(individualAttributeStructure.IndividualEventDetail);
//    }

//    public IndividualEventDetail? IndividualEventDetail { get; set; }
//}

//#region INDIVIDUAL_ATTRIBUTE_STRUCTURE p. 33-34
///* 

//INDIVIDUAL_ATTRIBUTE_STRUCTURE:=

//[
//n CAST <CASTE_NAME> {1:1} p.43
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//|
//n DSCR <PHYSICAL_DESCRIPTION> {1:1} p.58
//    +1 [CONC | CONT ] <PHYSICAL_DESCRIPTION> {0:M} p.58
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//|
//n EDUC <SCHOLASTIC_ACHIEVEMENT> {1:1} p.61
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//|
//n IDNO <NATIONAL_ID_NUMBER> {1:1} p.56
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//|
//n NATI <NATIONAL_OR_TRIBAL_ORIGIN> {1:1} p.56
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//|
//n NCHI <COUNT_OF_CHILDREN> {1:1} p.44
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//|
//n NMR <COUNT_OF_MARRIAGES> {1:1} p.44
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//|
//n OCCU <OCCUPATION> {1:1} p.57
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//|
//n PROP <POSSESSIONS> {1:1} p.59
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//|
//n RELI <RELIGIOUS_AFFILIATION> {1:1} p.60
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//|
//n RESI {1:1} // Resides at
//    +1 << INDIVIDUAL_EVENT_DETAIL >> { 0:1}
//|
//n SSN<SOCIAL_SECURITY_NUMBER> { 1:1} p.61
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//|
//n TITL<NOBILITY_TYPE_TITLE> { 1:1} p.57
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//|
//n FACT<ATTRIBUTE_DESCRIPTOR> { 1:1} p.43
//    +1 <<INDIVIDUAL_EVENT_DETAIL>> {0:1}* p.34
//]

//* Note: The usage of IDNO or the FACT tag require that a subordinate TYPE tag be used to define
//what kind of identification number or fact classification is being defined. The TYPE tag can be used
//with each of the above tags used in this structure.

//*/
//#endregion