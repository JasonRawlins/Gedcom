using Gedcom.Core;

namespace Gedcom.RecordStructures;

public class AddressStructure : RecordStructureBase
{
    public AddressStructure() { }
    public AddressStructure(Record record) : base(record) { }

    //public Address Address => FirstOrDefault<Address>(C.ADDR);
    public string AddressLine => Record.Value;
    public string AddressLine1 => _(C.ADR1);
    public string AddressLine2 => _(C.ADR2);
    public string AddressLine3 => _(C.ADR3);
    public string AddressCity => _(C.CITY);
    public string AddressState => _(C.STAE);
    public string AddressPostCode => _(C.POST);
    public string AddressCountry => _(C.CTRY);
}

public interface IAddressStructure
{
    List<string> PhoneNumber { get; }
    List<string> AddressEmail { get; }
    List<string> AddressFax { get; }
    List<string> AddressWebPage { get; }
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