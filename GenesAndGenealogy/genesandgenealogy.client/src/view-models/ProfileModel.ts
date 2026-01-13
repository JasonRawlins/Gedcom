import { FamilyModel } from "./FamilyModel";
import { IndividualModel } from "./IndividualModel";
import { SourceModel } from './SourceModel';
import { TreeModel } from "./TreeModel";

export interface ProfileModel {
  families: FamilyModel[];
  individual: IndividualModel;
  parents: FamilyModel;
  sources: SourceModel[];
  tree: TreeModel;
}
