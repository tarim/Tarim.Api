// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteStatus.cs" company="Karluks">
//   Copyright (c) Karluks. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tarim.Api.Infrastructure.Common.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ExecuteStatus
    {
        Success,
        Failed,
        Hold,
        Error

    }
}
