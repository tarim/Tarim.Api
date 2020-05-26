// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tips.cs" company="Tarim Lab">
//   Copyright (c) Tarim Lab. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Tarim.Api.Infrastructure.Common.Enums;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Tarim.Api.Infrastructure.Model.Tips
{
    public class Tip
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TipsType Category { get; set; }

        public string Content { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StatusType Status { get; set; }

        public string Source { get; set; }

        public bool Private { get; set; }

        public int UserRecid { get; set; }

        public string UserName {get;set;}
    }
}
