namespace Gedcom.RecordStructures;

public interface IPersonalNamePieces
{
    string Given { get; } // GIVN
    string NamePrefix { get; } // NPFX
    string NameSuffix { get; } // NSFX
    string Nickname { get; } // NICK
    string Surname { get; } // SURN
    string SurnamePrefix { get; } // SPFX
}