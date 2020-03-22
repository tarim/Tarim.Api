using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tarim.Api.Infrastructure.Common.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GenderType
    {
        Male=0,
        Female,
        Unisex
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProfileType
    {
        Basic=0,
        Advanced,
        Admin,
        Owner
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusType
    {
        Registered=0,
        Active,
        Hold,
        Disabled,
        Deleted
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum OriginationType
    {
        Uyghur = 0,
        Arabic,
        Persian,
        Other
    }
}
