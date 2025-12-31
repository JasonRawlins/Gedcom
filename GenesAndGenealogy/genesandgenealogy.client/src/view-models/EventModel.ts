import { DateModel } from '../view-models/DateModel';
import { PlaceModel } from '../view-models/PlaceModel';

export interface EventModel {
  //address: Address;
  ageAtEvent: string;
  causeOfEvent: string;
  eventOrFactClassification: string;
  date: DateModel;
  name: string;
  notes: string[];
  //multimediaLinks: MultimediaLinkModel[];
  place: PlaceModel;
  religiousAffiliation: string;
  responsibleAgency: string;
  restrictionNotice: string;
  //sourceCitations: SourceCitation[];
  tag: string;
}

