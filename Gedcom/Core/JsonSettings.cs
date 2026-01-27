using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Gedcom.Core;

public static class JsonSettings
{
    public static readonly JsonSerializerSettings DefaultOptions = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        },
        Formatting = Formatting.Indented
    };
}

