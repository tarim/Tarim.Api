// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Proverb.cs" company="Tarim Lab">
//   Copyright (c) Tarim Lab. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Tarim.Api.Infrastructure.Common.Enums;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Tarim.Api.Infrastructure.Model.Proverbs
{
    public class Proverb
    {

        public int Id { get; set; }

        public string Content { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProverbType Category { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StatusType Status { get; set; }

        public int UserRecid { get; set; }

        public string UserName {get;set;}
    }
}
