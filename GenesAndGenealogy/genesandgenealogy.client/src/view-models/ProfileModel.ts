import { FamilyModel } from "./FamilyModel";
import { IndividualModel } from "./IndividualModel";
import { TreeModel } from "./TreeModel";

export interface ProfileModel {
  families: FamilyModel[];
  individual: IndividualModel;
  tree: TreeModel;
}
