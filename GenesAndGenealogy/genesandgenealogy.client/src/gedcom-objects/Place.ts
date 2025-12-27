
import { Map } from "./Map";
import { NameVariation } from "./NameVariation";
import { Note } from "./Note";

export interface Place {
    hierarchy: string;
    map: Map;
    name: string;
    notes: Note[];
    phoneticVariations: NameVariation[];
    romanizedVariations: NameVariation[];
}
