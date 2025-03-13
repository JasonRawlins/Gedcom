namespace Gedcom.RecordStructures;

public interface IAddressStructure
{
    List<string> PhoneNumbers { get; }
    List<string> AddressEmails { get; }
    List<string> AddressFaxNumbers { get; }
    List<string> AddressWebPages { get; }
}

#region ADDRESS_STRUCTURE p. 
/* 

ADDRESS_STRUCTURE:=

n ADDR <ADDRESS_LINE> {1:1} p.41
n PHON <PHONE_NUMBER> {0:3} p.57
n EMAIL <ADDRESS_EMAIL> {0:3} p.41
n FAX <ADDRESS_FAX> {0:3} p.41
n WWW <ADDRESS_WEB_PAGE> {0:3} p.42

*/
#endregion