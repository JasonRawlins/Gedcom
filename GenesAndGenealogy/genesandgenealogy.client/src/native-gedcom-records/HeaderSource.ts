import { HeaderData } from "./HeaderData";
import { HeaderTree } from "./HeaderTree";

export interface HeaderSource {
  data: HeaderData;
  nameOfProduct: string;
  tree: HeaderTree;
  version: string;
  xref: string;
}
