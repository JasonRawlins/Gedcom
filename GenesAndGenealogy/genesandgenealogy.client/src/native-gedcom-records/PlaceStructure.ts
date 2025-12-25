
import { Map } from "./Map";
import { NameVariation } from "./NameVariation";
import { NoteStructure } from "./NoteStructure";

export interface PlaceStructure {
    map: Map;
    noteStructures: NoteStructure[];
    placeHierarchy: string;
    placeName: string;
    placePhoneticVariations: NameVariation[];
    placeRomanizedVariations: NameVariation[];
}
