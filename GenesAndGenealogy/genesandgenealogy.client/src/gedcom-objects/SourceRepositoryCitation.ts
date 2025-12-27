import { CallNumber } from "./CallNumber";
import { Note } from "./Note";

export interface SourceRepositoryCitation {
  callNumbers: CallNumber[];
  notes: Note[];
}
