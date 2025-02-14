namespace Gedcom.RecordStructures;

public class NameVariation : RecordStructureBase, IPersonalNamePieces
{
    public NameVariation() { }
    public NameVariation(Record record) : base(record) { }

    public string Type => _(C.TYPE); // PHONETIC_TYPE or ROMANIZED_TYPE

    #region IPersonalNamePieces

    public string Given => _(C.GIVN);
    public string NamePrefix => _(C.NPFX);
    public string NameSuffix => _(C.NSFX);
    public string Nickname => _(C.NICK);
    public string Surname => _(C.SURN);
    public string SurnamePrefix => _(C.SPFX);

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