import { Address } from "./Address";
import { GedcomDate } from "./GedcomDate";
import { MultimediaLink } from "./MultimediaLink";
import { Note } from "./Note";
import { Place } from "./Place";
import { SourceCitation } from "./SourceCitation";

export interface IndividualEvent {
  address: Address;
  ageAtEvent: string;
  causeOfEvent: string;
  date: string;
  eventOrFactClassification: string;
  gedcomDate: GedcomDate;
  multimediaLinks: MultimediaLink[];
  notes: Note[];
  place: Place;
  religiousAffiliation: string;
  responsibleAgency: string;
  restrictionNotice: string;
  sourceCitations: SourceCitation[]
  tag: string;
}
