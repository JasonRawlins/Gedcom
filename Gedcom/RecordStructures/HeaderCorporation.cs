namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class HeaderCorporation : RecordStructureBase, IAddressStructure
{
    public HeaderCorporation() : base() { }
    public HeaderCorporation(Record record) : base(record) { }

    private List<string>? _addressEmails = null;
    public List<string> AddressEmails => _addressEmails ??= ListValues(Tag.Email);

    private List<string>? _addressFaxNumbers = null;
    public List<string> AddressFaxNumbers => _addressFaxNumbers ??= ListValues(Tag.Facimilie);

    private AddressStructure? _addressStructure = null;
    public AddressStructure AddressStructure => _addressStructure ??= First<AddressStructure>(Tag.Address);

    private List<string>? _addressWebPages = null;
    public List<string> AddressWebPages => _addressWebPages ??= ListValues(Tag.Web);

    private List<string>? _phoneNumbers = null;
    public List<string> PhoneNumbers => _phoneNumbers ??= ListValues(Tag.Phone);

    public override string ToString() => $"{Record.Value}, {AddressStructure.AddressLine}";
}

#region HeaderCorporation p. 23
/* 

n HEAD {1:1}
    +1 SOUR <APPROVED_SYSTEM_ID> {1:1} p.42
        +2 CORP <NAME_OF_BUSINESS> {0:1} p.54
            +3 <<ADDRESS_STRUCTURE>> {0:1} p.31

*/
#endregion