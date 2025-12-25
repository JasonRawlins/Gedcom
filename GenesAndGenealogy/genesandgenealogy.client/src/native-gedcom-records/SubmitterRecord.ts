import { AddressStructure } from "./AddressStructure";
import { GedcomDate } from "./GedcomDate";
import { MultimediaLink } from "./MultimediaLink";
import { NoteStructure } from "./NoteStructure";

export interface Template {
  addressStructure: AddressStructure;
  automatedRecordId: string;
  changeDate: GedcomDate;
  languagePreferences: string[];
  multimediaLinks: MultimediaLink[];
  noteStructures: NoteStructure[];
  submitterName: string;
  submitterRegisteredRfn: string;
}
