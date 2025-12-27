import { HeaderCorporation } from "./HeaderCorporation";
import { HeaderData } from "./HeaderData";
import { HeaderTree } from "./HeaderTree";

export interface HeaderSource {
  corporation: HeaderCorporation;
  data: HeaderData;
  nameOfProduct: string;
  tree: HeaderTree;
  version: string;
  xref: string;
}
