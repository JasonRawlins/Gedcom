using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Gedcom;

public static class JsonSettings
{
    public static readonly JsonSerializerSettings DefaultOptions = new JsonSerializerSettings
    {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        },
        Formatting = Formatting.None
    };
}

