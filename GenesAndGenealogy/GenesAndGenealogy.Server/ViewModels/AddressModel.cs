using Gedcom.RecordStructures;

namespace GenesAndGenealogy.Server.ViewModels;

public class AddressModel
{
    public AddressModel(AddressStructure addressStructure)
    {
        City = addressStructure.AddressCity;
        Country = addressStructure.AddressCountry;
        Line = addressStructure.AddressLine.Replace("<line>", "").Replace("</line>", "");
        Line1 = addressStructure.AddressLine1;
        Line2 = addressStructure.AddressLine2;
        Line3 = addressStructure.AddressLine3;
        PostalCode = addressStructure.AddressPostCode;
        State = addressStructure.AddressState;
    }

    public string City { get; set; }
    public string Country { get; set; }
    public string Line { get; set; }
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string Line3 { get; set; }
    public string PostalCode { get; set; }
    public string State { get; set; }

    public override string ToString() => $"{Line}, {City}, {State}, {PostalCode}";
}