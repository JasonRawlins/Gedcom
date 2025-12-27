import { GedcomDate } from "./GedcomDate";
import { Note } from "./Note";

export interface ChangeDate {
  gedcomDate: GedcomDate;
  noteStructures: Note[];
}
