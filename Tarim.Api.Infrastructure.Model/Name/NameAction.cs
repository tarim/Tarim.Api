using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Tarim.Api.Infrastructure.Common.Enums;

namespace Tarim.Api.Infrastructure.Model.Name
{
    public class NameAction
    {
        /// <summary>
        /// Get or sets ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Get or sets name id
        /// </summary>
        public int NameId { get; set; }

        /// <summary>
        /// Get or sets name action [LIKE, LOVE,MYNAME]
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public NameActionType ActionType { get; set; }

        /// <summary>
        /// Get or sets User ip address
        /// </summary>
        public string UserIp { get; set; }
    }
}
