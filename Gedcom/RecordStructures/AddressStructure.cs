namespace Gedcom.RecordStructures;

// The Gedcom Standard 5.5.1 documentation is at the end of this file.
public class AddressStructure : RecordStructureBase
{
    public AddressStructure() { }
    public AddressStructure(Record record) : base(record) { }

    private string? _addressCity = null;
    public string AddressCity => _addressCity ??= GetValue(Tag.City);

    private string? _addressCountry = null;
    public string AddressCountry => _addressCountry ??= GetValue(Tag.Country);

    public string AddressLine => Record.Value;

    private string? _addressLine1 = null;
    public string AddressLine1 => _addressLine1 ??= GetValue(Tag.Address1);

    private string? _addressLine2 = null;
    public string AddressLine2 => _addressLine2 ??= GetValue(Tag.Address2);

    private string? _addressLine3 = null;
    public string AddressLine3 => _addressLine3 ??= GetValue(Tag.Address3);

    private string? _addressPostCode = null;
    public string AddressPostCode => _addressPostCode ??= GetValue(Tag.PostalCode);

    private string? _addressState = null;
    public string AddressState => _addressState ??= GetValue(Tag.State);

    public override string ToString() => $"{Record.Value}, {AddressLine}";
}

#region ADDRESS_STRUCTURE p. 31
/* 

ADDRESS_STRUCTURE:=

n ADDR <ADDRESS_LINE> {1:1} p.41
    +1 CONT <ADDRESS_LINE> {0:3} p.41
    +1 ADR1 <ADDRESS_LINE1> {0:1} p.41
    +1 ADR2 <ADDRESS_LINE2> {0:1} p.41
    +1 ADR3 <ADDRESS_LINE3> {0:1} p.41
    +1 CITY <ADDRESS_CITY> {0:1} p.41
    +1 STAE <ADDRESS_STATE> {0:1} p.42
    +1 POST <ADDRESS_POSTAL_CODE> {0:1} p.41
    +1 CTRY <ADDRESS_COUNTRY> {0:1} p.41
n PHON <PHONE_NUMBER> {0:3} p.57
n EMAIL <ADDRESS_EMAIL> {0:3} p.41
n FAX <ADDRESS_FAX> {0:3} p.41
n WWW <ADDRESS_WEB_PAGE> {0:3} p.42

The address structure should be formed as it would appear on a mailing label using the ADDR and
the CONT lines to form the address structure. The ADDR and CONT lines are required for any
address. The additional subordinate address tags such as STAE and CTRY are provided to be used
by systems that have structured their addresses for indexing and sorting. For backward compatibility
these lines are not to be used in lieu of the required ADDR.and CONT line structure.

*/
#endregion