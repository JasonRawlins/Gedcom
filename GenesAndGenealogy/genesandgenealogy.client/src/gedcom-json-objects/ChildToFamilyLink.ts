import { Note } from "./Note";
export interface ChildToFamilyLink {
  adoptedByWhichParent: string;
  childLinkageStatus: string;
  notes: Note[];
  pedigreeLinkageType: string;
  xref: string;
}
