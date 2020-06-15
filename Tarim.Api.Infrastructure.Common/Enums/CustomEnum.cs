using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tarim.Api.Infrastructure.Common.Enums
{
    // [JsonConverter(typeof(StringEnumConverter))]
    public enum GenderType
    {
        Male = 0,
        Female,
        Unisex
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProfileType
    {
        Basic = 0,
        Advanced,
        Admin,
        Owner
    }

    // [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusType
    {
        Registered = 0,
        Active,
        Hold,
        Disabled,
        Deleted
    }

    //[JsonConverter(typeof(StringEnumConverter))]
    public enum OriginType
    {
        Uyghur = 0,
        Arabic,
        Persian,
        Other
    }

    //   [JsonConverter(typeof(StringEnumConverter))]
    public enum NameActionType
    {
        LIKE = 0,
        LOVE,
        MYNAME
    }

    public enum TipsType
    {
        General=0,
        DevOps,
        Go,
        CSharp,
        Api,
        React,
        CSS,
        Oracle,
        MySql,
        RFM
    }

    public enum ProverbType
    {
        Proverb=0,
        Idiom,
        Wisdom,
        Other
    }

}
