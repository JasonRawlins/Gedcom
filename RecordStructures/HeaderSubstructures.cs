using Gedcom.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Gedcom.RecordStructures;

[JsonConverter(typeof(HeaderSOURJsonConverter))]
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

internal class HeaderSOURJsonConverter : JsonConverter<HeaderSOUR>
{
    public override HeaderSOUR? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, HeaderSOUR headerSOUR, JsonSerializerOptions options)
    {
        var headerSOURJson = new HeaderSOURJson(headerSOUR);
        JsonSerializer.Serialize(writer, headerSOURJson, headerSOURJson.GetType(), options);
    }
}

internal class HeaderSOURJson : GedcomJson
{
    public HeaderSOURJson(HeaderSOUR headerSOUR)
    {
        Xref = JsonString(headerSOUR.Xref);
        Version = JsonString(headerSOUR.Version);
        NameOfProduct = JsonString(headerSOUR.NameOfProduct);
        HeaderCORP = JsonRecord(headerSOUR.HeaderCORP);
        HeaderDATA = JsonRecord(headerSOUR.HeaderDATA);
    }

    public string? Xref { get; set; }
    public string? Version { get; set; }
    public string? NameOfProduct { get; set; }
    public HeaderCORP? HeaderCORP { get; set; }
    public HeaderDATA? HeaderDATA { get; set; }
}

[JsonConverter(typeof(HeaderCORPJsonConverter))]
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

internal class HeaderCORPJsonConverter : JsonConverter<HeaderCORP>
{
    public override HeaderCORP? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, HeaderCORP headerCORP, JsonSerializerOptions options)
    {
        var headerCORPJson = new HeaderCORPJson(headerCORP);
        JsonSerializer.Serialize(writer, headerCORPJson, headerCORPJson.GetType(), options);
    }
}

internal class HeaderCORPJson : GedcomJson
{
    public HeaderCORPJson(HeaderCORP headerCORP)
    {
        AddressStructure = JsonRecord(headerCORP.AddressStructure);
        PhoneNumbers = JsonList(headerCORP.PhoneNumbers);
        AddressEmails = JsonList(headerCORP.AddressEmails);
        AddressFaxNumbers = JsonList(headerCORP.AddressFaxNumbers);
        AddressWebPages = JsonList(headerCORP.AddressWebPages);
    }

    public AddressStructure? AddressStructure { get; set; }

    #region IAddressStructure

    public List<string>? PhoneNumbers { get; set; }
    public List<string>? AddressEmails { get; set; }
    public List<string>? AddressFaxNumbers { get; set; }
    public List<string>? AddressWebPages { get; set; }

    #endregion
}

[JsonConverter(typeof(HeaderDATAJsonConverter))]
public class HeaderDATA : RecordStructureBase
{
    public HeaderDATA() : base() { }
    public HeaderDATA(Record record) : base(record) { }

    public string PublicationDate => _(C.DATE);
    public NoteStructure CopyrightSourceData => First<NoteStructure>(C.COPR);
}

internal class HeaderDATAJsonConverter : JsonConverter<HeaderDATA>
{
    public override HeaderDATA? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
    public override void Write(Utf8JsonWriter writer, HeaderDATA headerDATA, JsonSerializerOptions options)
    {
        var headerDATAJson = new HeaderDATAJson(headerDATA);
        JsonSerializer.Serialize(writer, headerDATAJson, headerDATAJson.GetType(), options);
    }
}

internal class HeaderDATAJson : GedcomJson
{
    public HeaderDATAJson(HeaderDATA headerDATA)
    {
        PublicationDate = JsonString(headerDATA.PublicationDate);
        CopyrightSourceData = JsonRecord(headerDATA.CopyrightSourceData);
    }

    public string? PublicationDate { get; set; }
    public NoteStructure? CopyrightSourceData { get; set; }
}

#region HeaderSOUR p. 23
/* 

n HEAD
    +1 SOUR Approved system id
        +2 CORP Name of business
            +3 <<ADDRESS_STRUCTURE>> {0:1} p.31

*/
#endregion