// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UyghurName.cs" company="Tarim Lab">
//   Copyright (c) Tarim Lab. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Tarim.Api.Infrastructure.Common.Enums;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Tarim.Api.Infrastructure.Model.Name
{
    public class UyghurName
    {

        public int Id { get; set; }

        public string NameUg { get; set; }

        public string NameLatin { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public OriginType Origin { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GenderType Gender { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StatusType Status { get; set; }

        public string RelatedName { get; set; }

        public bool IsFamilyName { get; set; }

        public string Description { get; set; }
    }
}
