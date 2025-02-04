namespace Gedcom.RecordStructures;

public class NameVariation : RecordStructureBase, IPersonalNamePieces
{
    public NameVariation(Record record) : base(record) { }

    public string Type => V(C.TYPE);

    #region IPersonalNamePieces

    public string Given => V(C.GIVN);
    public string NamePrefix => V(C.NPFX);
    public string NameSuffix => V(C.NSFX);
    public string Nickname => V(C.NICK);
    public string Surname => V(C.SURN);
    public string SurnamePrefix => V(C.SPFX);

    #endregion
}

#region NAME_PHONETIC_VARIATION (FONE) and NAME_ROMANIZED_VARIATION (ROMN) p. 38
/* 

n NAME
    +1 FONE <NAME_PHONETIC_VARIATION> {0:M} p.55
        +2 TYPE <PHONETIC_TYPE> {1:1} p.57
        +2 <<PERSONAL_NAME_PIECES>> {0:1} p.37
    +1 ROMN <NAME_ROMANIZED_VARIATION> {0:M} p.56
        +2 TYPE <ROMANIZED_TYPE> {1:1} p.61
        +2 <<PERSONAL_NAME_PIECES>> {0:1} p.37

*/
#endregion