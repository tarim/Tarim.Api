// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UyghurName.cs" company="Tarim Lab">
//   Copyright (c) Tarim Lab. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Tarim.Api.Infrastructure.Common.Enums;

namespace Tarim.Api.Infrastructure.Model.names
{
    public class UyghurName
    {
        public int Id { get; set; }

        public string NameUg { get; set; }

        public string NameLatin { get; set; }

        public OriginationType Origination { get; set; }

        public GenderType Gender { get; set; }

        public StatusType Status { get; set; }

        public string RelatedName { get; set; }

        public bool IsFamilyName { get; set; }

        public string Description { get; set; }
    }
}
