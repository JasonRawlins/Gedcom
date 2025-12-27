import { NameVariation } from "./NameVariation";

export interface PersonalName {
  given: string;
  name: string;
  nickname: string;
  phoneticVariation: NameVariation;
  prefix: string;
  romanizedVariation: NameVariation;
  suffix: string;
  surname: string;
  surnamePrefix: string;
  type: string;
}
