import { NoteStructure } from "./NoteStructure";
export interface ChildToFamilyLink {
  adoptedByWhichParent: string;
  childLinkageStatus: string;
  noteStructures: NoteStructure[];
  pedigreeLinkageType: string;
  xref: string;
}
