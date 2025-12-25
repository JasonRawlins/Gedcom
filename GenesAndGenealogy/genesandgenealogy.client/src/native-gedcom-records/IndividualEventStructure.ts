import { AddressStructure } from "./AddressStructure";
import { ChildToFamilyLink } from "./ChildToFamilyLink";
import { GedcomDate } from "./GedcomDate";
import { MultimediaLink } from "./MultimediaLink";

export interface IndividualEventStructure {
  childToFamilyLink: ChildToFamilyLink;
  addressStructure: AddressStructure;
  ageAtEvent: string;
  causeOfEvent: string;
  dateValue: string;
  eventOrFactClassification: string;
  gedcomDate: GedcomDate;
  multimediaLinks: MultimediaLink[];
}
