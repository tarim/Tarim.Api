using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tarim.Api.Infrastructure.Common.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GenderType
    {
        Oghul=0,
        Qiz=1,
        Ortaq=2
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProfileType
    {
        Basic=0,
        Advanced=1,
        Admin=2,
        Owner=3
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusType
    {
        Registered=0,
        Active=1,
        Hold=2,
        Disabled=3,
        Deleted=4
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OriginationType
    {
        Uyghurche = 0,
        Erepche = 1,
        Parische = 2,
        Bashqa = 3
    }
}
