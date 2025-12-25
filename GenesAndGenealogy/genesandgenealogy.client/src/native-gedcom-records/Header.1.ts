import { CharacterSet } from "./CharacterSet";
import { GedcomDate } from "./GedcomDate";
import { HeaderGedcom } from "./HeaderGedcom";
import { HeaderSource } from "./HeaderSource";
import { NoteStructure } from "./NoteStructure";
import { SubmissionRecord } from "./SubmissionRecord";

export interface Header {
    characterSet: CharacterSet;
    copyrightGedcomFile: string;
    fileName: string;
    gedcom: HeaderGedcom;
    gedcomContentDescription: NoteStructure;
    languageOfText: string;
    placeHierarchy: string;
    receivingSystemName: string;
    source: HeaderSource;
    submissionRecord: SubmissionRecord;
    submitter: string;
    transmissionDate: GedcomDate;
}
