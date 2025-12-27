import { CharacterSet } from "./CharacterSet";
import { GedcomDate } from "./GedcomDate";
import { HeaderGedcom } from "./HeaderGedcom";
import { HeaderSource } from "./HeaderSource";
import { Note } from "./Note";
import { Submission } from "./Submission";

export interface Header {
  characterSet: CharacterSet;
  copyrightGedcomFile: string;
  fileName: string;
  gedcom: HeaderGedcom;
  gedcomContentDescription: Note;
  languageOfText: string;
  placeHierarchy: string;
  receivingSystemName: string;
  source: HeaderSource;
  submissionRecord: Submission;
  submitter: string;
  transmissionDate: GedcomDate;
}
