import { MultimediaFileReferenceNumber } from "./MultimediaFileReferenceNumber";

export interface MultimediaLink {
  descriptiveTitle: string;
  multimediaFileReferenceNumber: MultimediaFileReferenceNumber[]
  sourceMediaType: string;
  xref: string;
}
