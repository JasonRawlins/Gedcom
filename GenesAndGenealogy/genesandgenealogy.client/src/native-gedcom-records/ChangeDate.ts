import { GedcomDate } from "./GedcomDate";
import { NoteStructure } from "./NoteStructure";

export interface ChangeDate {
  gedcomDate: GedcomDate;
  noteStructures: NoteStructure[];
}
