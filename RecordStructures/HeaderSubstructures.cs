using Gedcom.Core;

namespace Gedcom.RecordStructures;

// The SOUR tag in the HEAD doesn't quite match the SOUR tags elsewhere. 
// HeaderSOUR is the temp class name. I'll merge the two classes later
// if they turn out to represent the same entity.
public class HeaderSOUR : RecordStructureBase
{
    public HeaderSOUR() : base() { }
    public HeaderSOUR(Record record) : base(record) { }

    public string Xref => Record.Value;
    public string Version => _(C.VERS);
    public string NameOfProduct => _(C.NAME);
    public HeaderCORP HeaderCORP => First<HeaderCORP>(C.CORP);
    public HeaderDATA HeaderDATA => First<HeaderDATA>(C.DATA);
}

public class HeaderCORP : RecordStructureBase, IAddressStructure
{
    public HeaderCORP() : base() { }
    public HeaderCORP(Record record) : base(record) { }

    public AddressStructure AddressStructure => First<AddressStructure>(C.ADDR);

    #region IAddressStructure
    public List<string> PhoneNumbers => ListValues(C.PHON);
    public List<string> AddressEmails => ListValues(C.EMAIL);
    public List<string> AddressFaxNumbers => ListValues(C.FAX);
    public List<string> AddressWebPages => ListValues(C.WWW);
    #endregion
}

public class HeaderDATA : RecordStructureBase
{
    public HeaderDATA() : base() { }
    public HeaderDATA(Record record) : base(record) { }

    public string PublicationDate => _(C.DATE);
    public NoteStructure CopyrightSourceData => First<NoteStructure>(C.COPR);
    //public KeyValuePair<string, string> CopyrightSourceData => new KeyValuePair<string, string>("key", "value"); // ""; //new NoteStructure(Record); // Default<NoteStructure>(); // FirstOrDefault<NoteStructure>(C.COPR);
}

#region HeaderSOUR p. 23
/* 

n HEAD
    +1 SOUR Approved system id
        +2 CORP Name of business
            +3 <<ADDRESS_STRUCTURE>> {0:1} p.31

*/
#endregion