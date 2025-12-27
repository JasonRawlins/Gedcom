import { IndividualModel } from "./IndividualModel";

export interface FamilyModel {
  children: IndividualModel[];
  partner1: IndividualModel;
  partner2: IndividualModel;
}
