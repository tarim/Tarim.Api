﻿// --------------------------------------------------------------------------------------------------------------------
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

        public string Name { get; set; }

        public string Origin { get; set; }

        public SexType Sex { get; set; }

        public bool IsFamilyName { get; set; }

        public string Description { get; set; }
    }
}
