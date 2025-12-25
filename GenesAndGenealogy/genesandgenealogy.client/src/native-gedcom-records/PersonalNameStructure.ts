import { NameVariation } from "./NameVariation";

export interface PersonalNameStructure {
  given: string;
  namePersonal: string;
  namePhoneticVariation: NameVariation;
  namePrefix: string;
  nameSuffix: string;
  nameRomanizedVariation: NameVariation;
  nameType: string;
  nickname: string;
  surname: string;
  surnamePrefix: string;
}
