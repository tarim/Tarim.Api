using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Tarim.Api.Infrastructure.Common.Enums;

namespace Tarim.Api.Infrastructure.Model
{
    public sealed class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [JsonProperty("status")]
        [EnumDataType(typeof(StatusType))]
        public StatusType Status { get; set; }

        [JsonProperty("profile")]
        [EnumDataType(typeof(ProfileType))]
        public ProfileType Profile { get; set; }
        public string[] Roles { get; set; }
        public string Description { get; set; }
    }
}
