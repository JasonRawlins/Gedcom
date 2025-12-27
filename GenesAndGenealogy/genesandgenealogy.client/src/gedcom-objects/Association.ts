import { Note } from "./Note";
import { SourceCitation } from "./SourceCitation";

export interface Association {
  notes: Note[];
  relationIsDescriptor: string;
  sourceCitations: SourceCitation[];
}
