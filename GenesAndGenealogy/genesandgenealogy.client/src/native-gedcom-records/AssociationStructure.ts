import { NoteStructure } from "./NoteStructure";
import { SourceCitation } from "./SourceCitation";

export interface AssociationStructure {
  noteStructures: NoteStructure[];
  relationIsDescriptor: string;
  sourceCitations: SourceCitation[];
}
