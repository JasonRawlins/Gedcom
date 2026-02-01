import { FamilyModel } from "./FamilyModel";
import { IndividualModel } from "./IndividualModel";
import { MultimediaModel } from "./MultimediaModel";
import { SourceModel } from './SourceModel';
import { TreeModel } from "./TreeModel";

export interface ProfileModel {
  families: FamilyModel[];
  individual: IndividualModel;
  multimediaItems: MultimediaModel[];
  parents: FamilyModel;
  portraitUrl: string;
  sources: SourceModel[];
  tree: TreeModel;
}
